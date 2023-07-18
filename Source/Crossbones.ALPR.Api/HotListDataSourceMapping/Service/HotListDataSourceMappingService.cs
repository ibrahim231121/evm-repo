using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListDataSource.Mapping;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Crossbones.ALPR.Api.HotListNumberPlates;
using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business;
using Crossbones.Modules.Common.Queryables;
using System.Diagnostics;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping.Service
{
    public class HotListDataSourceMappingService : ServiceBase, IHotListDataSourceMappingService
    {
        private const int MAXIMUM_BLOCK_SIZE_FOR_INGESTION = 50000;
        readonly INumberPlateService numberPlateService;
        readonly IHotListNumberPlateService hotListNumberPlateService;
        readonly IHotListDataSourceItemService hotListDataSourceItemService;

        public HotListDataSourceMappingService(ServiceArguments args,
            INumberPlateService plateService,
            IHotListNumberPlateService hotListNPService,
            IHotListDataSourceItemService sourceItemService
            ) : base(args)
        {
            numberPlateService = plateService;
            hotListNumberPlateService = hotListNPService;
            hotListDataSourceItemService = sourceItemService;
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
                    new DTO.HotListNumberPlateDTO() { HotListId = hotListId, NumberPlatesId = _numberPlateId });

                if (chainCommand.Commands.Any())
                    _ = await Execute(chainCommand);
            }
            stopwatch.Stop();
            Console.WriteLine("Time Elapsed : " + TimeSpan.FromTicks(stopwatch.ElapsedTicks));
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
