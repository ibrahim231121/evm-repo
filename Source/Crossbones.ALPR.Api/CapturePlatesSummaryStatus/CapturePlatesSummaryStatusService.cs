using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Business.CapturedPlate.View;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public class CapturePlatesSummaryStatusService: ServiceBase, ICapturePlatesSummaryStatusService
    {
        readonly ISequenceProxy _capturedPlateSummaryStatusSequenceProxy;

        public CapturePlatesSummaryStatusService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => (_capturedPlateSummaryStatusSequenceProxy) = (sequenceProxyFactory.GetProxy(ALPRResources.CapturePlateSummaryStatus));

        public async Task<SysSerial> Add(CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem)
        {
            var capturedPlateSummaryStatusId = new SysSerial(await _capturedPlateSummaryStatusSequenceProxy.Next(CancellationToken.None));

            var addCapturePlateSummaryStatusCommand = new AddCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusId, capturedPlateSummaryStatusItem);

            _ = await Execute(addCapturePlateSummaryStatusCommand);

            return capturedPlateSummaryStatusId;
        }

        public async Task Change(SysSerial capturedPlateSummaryStatusSysSerial, CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem)
        {
            var changeCapturePlateSummaryStatusCommand = new ChangeCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusSysSerial,
                                                                                                              capturedPlateSummaryStatusItem);

            _ = await Execute(changeCapturePlateSummaryStatusCommand);
        }

        public async Task Delete(SysSerial capturedPlateSummaryStatusSysSerial)
        {
            var deleteCapturePlateSummaryStatusCommand = new DeleteCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusSysSerial,
                                                                                                              DeleteCommandFilter.All);

            _ = await Execute(deleteCapturePlateSummaryStatusCommand);
        }

        public async Task DeleteAll()
        {
            var deleteCapturePlateSummaryStatusCommand = new DeleteCapturePlatesSummaryStatusItem(new SysSerial(0),
                                                                                                              DeleteCommandFilter.All);

            _= await Execute(deleteCapturePlateSummaryStatusCommand);
        }

        public async Task<CapturePlatesSummaryStatusItem> Get(SysSerial capturedPlateSummaryStatusSysSerial)
        {
            var getCapturePlateSummaryStatusQuery = new GetCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusSysSerial,
                                                                                                   GetQueryFilter.Single);

            var resp = await Inquire<CapturePlatesSummaryStatusItem>(getCapturePlateSummaryStatusQuery);

            return resp;
        }

        public async Task<PagedResponse<CapturePlatesSummaryStatusItem>> GetAll(Pager paging, GridFilter filter, GridSort sort)
        {
            var getCapturePlateSummaryStatusQuery = new GetCapturePlatesSummaryStatusItem(new SysSerial(0),
                                                                                                   GetQueryFilter.All,
                                                                                                   filter,
                                                                                                   paging,
                                                                                                   sort);

            var resp = await Inquire<PagedResponse<CapturePlatesSummaryStatusItem>>(getCapturePlateSummaryStatusQuery);

            return resp;
        }
    }
}
