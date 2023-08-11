using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListDataSource.Mapping;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Crossbones.ALPR.Api.HotListNumberPlates;
using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.JobScheduler.SharedLibrary.MappingDataSourceALPR;
using Crossbones.JobScheduler.SharedLibrary.MappingDataSourceALPR.RelatedBody;
using Crossbones.Modules.Business;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Common.ServiceDiscovery;
using Hangfire;
using System.Diagnostics;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping.Service
{
    public class HotListDataSourceMappingService : ServiceBase, IHotListDataSourceMappingService
    {
        private const int MAXIMUM_BLOCK_SIZE_FOR_INGESTION = 5000;
        public static string RECURRING_JOB_NAME = "ALPR_MappingForAllDataSources";

        private readonly INumberPlateService numberPlateService;
        private readonly IHotListNumberPlateService hotListNumberPlateService;
        private readonly IHotListDataSourceItemService hotListDataSourceItemService;
        private readonly IServiceDiscoveryConfiguration _configuration;

        public HotListDataSourceMappingService(ServiceArguments args,
            INumberPlateService plateService,
            IHotListNumberPlateService hotListNPService,
            IHotListDataSourceItemService sourceItemService,
            IServiceDiscoveryConfiguration configuration
            ) : base(args)
        {
            numberPlateService = plateService;
            hotListNumberPlateService = hotListNPService;
            hotListDataSourceItemService = sourceItemService;
            _configuration = configuration;
        }

        public async Task EnqueJobAllDataSources()
        {
            using (var _mappingForAllDataSources = new RequestMappingAllDataSources(_configuration))
            {
                var _details = new MappingRequiredDetails()
                {
                    TenantId = _httpContextAccessor.TenantId.Equals(0) ? 1 : _httpContextAccessor.TenantId,
                };

                RecurringJob.AddOrUpdate<RequestMappingAllDataSources>(RECURRING_JOB_NAME, x => x.Execute(_details), Cron.Daily);
            }
        }

        public async Task<object> EnqueJobSingleDataSource(long recId)
        {
            var jobId = string.Empty;

            using (var _mappingForAllDataSources = new RequestMappingSingleDataSource(_configuration))
            {
                var _details = new MappingRequiredDetails()
                {
                    TenantId = _httpContextAccessor.TenantId,
                    RecId = recId
                };

                jobId = BackgroundJob.Enqueue<RequestMappingSingleDataSource>(x => x.Execute(_details));
            }

            return jobId;
        }

        public async Task AddNumberPlatesToDataBase(IEnumerable<DTO.NumberPlateDTO> numberPlates, long hotListId)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            //numberPlates.ForEach(async licensePlate =>
            for (int _count = 0; _count < numberPlates.Count() && _count < MAXIMUM_BLOCK_SIZE_FOR_INGESTION; _count++)
            {
                var licensePlate = numberPlates.ToArr()[_count];
                var chainCommand = new ChainCommand();

                RecId _numberPlateId;

                if ((bool)licensePlate.NeedFullInsertion)
                {
                    var numerPlateCommand = await numberPlateService.GetAddCommand(licensePlate);
                    _numberPlateId = numerPlateCommand.Id;
                    chainCommand += numerPlateCommand;
                }
                else { _numberPlateId = new RecId(licensePlate.RecId); }

                chainCommand += await hotListNumberPlateService.GetAddCommand(
                    new DTO.HotListNumberPlateDTO() { HotListId = hotListId, NumberPlateId = _numberPlateId });

                if (chainCommand.Commands.Any())
                    _ = await Execute(chainCommand);
            }
            stopwatch.Stop();
            Console.WriteLine("Time Elapsed : " + TimeSpan.FromTicks(stopwatch.ElapsedTicks).TotalSeconds);
        }

        private async Task GetHotListDataSourceInfo(DTO.HotListDataSourceDTO hotListDataSourceItem)
        {
            var query = new GetHotListDataSourceMappingDetails(hotListDataSourceItem);
            var res = await Inquire<IEnumerable<DTO.NumberPlateDTO>>(query);

            //hotListDataSourceItem.Hotlists.ForEach(async _hotListItem =>
            foreach (var _hotListItem in hotListDataSourceItem.Hotlists)
            {
                await AddNumberPlatesToDataBase(res, _hotListItem.RecId);
            }
        }

        public async Task<RecId> ExecuteMappingForSingleDataSource(long _hotListDataSourceItemId)
        {
            var hotListDataSourceItem = await hotListDataSourceItemService.Get(new RecId(_hotListDataSourceItemId));

            await GetHotListDataSourceInfo(hotListDataSourceItem);

            return new RecId(_hotListDataSourceItemId);
        }

        public async Task<List<DTO.HotListDataSourceDTO>> ExecuteMappingForAllDataSources()
        {
            var dataQuery = new GetHotListDataSource(RecId.Empty, GetQueryFilter.All);
            var response = await Inquire<List<DTO.HotListDataSourceDTO>>(dataQuery);

            foreach (var hotListDataSourceItem in response)
            {
                await GetHotListDataSourceInfo(hotListDataSourceItem);
            }

            return response;
        }
    }
}
