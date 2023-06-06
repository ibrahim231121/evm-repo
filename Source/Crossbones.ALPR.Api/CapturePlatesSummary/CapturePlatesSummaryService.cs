using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Business.CapturedPlate.Get;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.CapturePlatesSummary
{
    public class CapturePlatesSummaryService: ServiceBase, ICapturePlatesSummaryService
    {
        readonly ISequenceProxy _capturedPlateSummarySequenceProxy;

        public CapturePlatesSummaryService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => (_capturedPlateSummarySequenceProxy) = (sequenceProxyFactory.GetProxy(ALPRResources.CapturePlateSummary));

        public async Task<SysSerial> Add(CapturePlatesSummaryItem capturedPlateSummaryItem)
        {
            var capturedPlateSummaryId = new SysSerial(await _capturedPlateSummarySequenceProxy.Next(CancellationToken.None));

            var addCapturePlateSummaryCommand = new AddCapturePlatesSummaryItem(capturedPlateSummaryId, capturedPlateSummaryItem);

            _ = await Execute(addCapturePlateSummaryCommand);

            return capturedPlateSummaryId;
        }

        public async Task Change(CapturePlatesSummaryItem capturedPlateSummaryItem)
        {
            var changeCapturePlateSummaryCommand = new ChangeCapturePlatesSummaryItem(new SysSerial(0),
                                                                                                 capturedPlateSummaryItem);

            _ = await Execute(changeCapturePlateSummaryCommand);
        }

        public async Task Delete(long userId, long capturedPlateId)
        {
            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new SysSerial(0),
                                                                                                 DeleteCommandFilter.All,
                                                                                                 userId,
                                                                                                 capturedPlateId);

            _ = await Execute(deleteCapturePlateSummaryCommand);
        }

        public async Task DeleteAll(long userId)
        {
            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new SysSerial(0),
                                                                                                 userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                                 userId);

            _= await Execute(deleteCapturePlateSummaryCommand);
        }

        public async Task<CapturePlatesSummaryItem> Get(long userId, long capturedPlateId)
        {
            var getCapturePlateSummaryQuery = new GetCapturePlatesSummaryItem(userId,
                                                                                       new SysSerial(0),
                                                                                       capturedPlateId,
                                                                                       GetQueryFilter.Single);

            var resp = await Inquire<CapturePlatesSummaryItem>(getCapturePlateSummaryQuery);

            return resp;
        }

        public async Task<PagedResponse<CapturePlatesSummaryItem>> GetAll(Pager paging, GridFilter filter, GridSort sort, long userId = 0)
        {
            var getCapturePlateSummaryQuery = new GetCapturePlatesSummaryItem(userId,
                                                                                      new SysSerial(0),
                                                                                      0,
                                                                                      userId > 0 ? GetQueryFilter.AllByUser : GetQueryFilter.All,
                                                                                      null,
                                                                                      filter,
                                                                                      paging,
                                                                                      sort);

            var resp = await Inquire<PagedResponse<CapturePlatesSummaryItem>>(getCapturePlateSummaryQuery);

            return resp;
        }

        public async Task<List<CapturePlatesSummaryItem>> GetAllWithOutPaging(GridFilter filter, GridSort sort, long userId = 0)
        {
            var getCapturePlateSummaryQuery = new GetCapturePlatesSummaryItem(userId,
                                                                                      new SysSerial(0),
                                                                                      0,
                                                                                      userId > 0 ? GetQueryFilter.AllByUserWithOutPaging : GetQueryFilter.AllWithoutPaging,
                                                                                      null,
                                                                                      filter,
                                                                                      null,
                                                                                      sort);

            var resp = await Inquire<List<Corssbones.ALPR.Database.Entities.CapturePlatesSummary>>(getCapturePlateSummaryQuery);

            return resp.Select(cps => DTOHelper.ConvertToDTO(cps)).ToList();
        }
    }
}
