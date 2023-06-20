using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Business.CapturedPlate.Get;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public class CapturePlatesSummaryStatusService : ServiceBase, ICapturePlatesSummaryStatusService
    {
        readonly ISequenceProxy _capturedPlateSummaryStatusSequenceProxy;

        public CapturePlatesSummaryStatusService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => (_capturedPlateSummaryStatusSequenceProxy) = (sequenceProxyFactory.GetProxy(ALPRResources.CapturePlateSummaryStatus));

        public async Task<RecId> Add(CapturePlatesSummaryStatusDTO capturedPlateSummaryStatusItem)
        {
            var capturedPlateSummaryStatusId = new RecId(await _capturedPlateSummaryStatusSequenceProxy.Next(CancellationToken.None));

            var addCapturePlateSummaryStatusCommand = new AddCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusId, capturedPlateSummaryStatusItem);

            _ = await Execute(addCapturePlateSummaryStatusCommand);

            return capturedPlateSummaryStatusId;
        }

        public async Task Change(RecId capturedPlateSummaryStatusRecId, CapturePlatesSummaryStatusDTO capturedPlateSummaryStatusItem)
        {
            var changeCapturePlateSummaryStatusCommand = new ChangeCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusRecId,
                                                                                                              capturedPlateSummaryStatusItem);

            _ = await Execute(changeCapturePlateSummaryStatusCommand);
        }

        public async Task Delete(RecId capturedPlateSummaryStatusRecId)
        {
            var deleteCapturePlateSummaryStatusCommand = new DeleteCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusRecId,
                                                                                                              DeleteCommandFilter.All);

            _ = await Execute(deleteCapturePlateSummaryStatusCommand);
        }

        public async Task DeleteAll()
        {
            var deleteCapturePlateSummaryStatusCommand = new DeleteCapturePlatesSummaryStatusItem(new RecId(0),
                                                                                                              DeleteCommandFilter.All);

            _ = await Execute(deleteCapturePlateSummaryStatusCommand);
        }

        public async Task<CapturePlatesSummaryStatusDTO> Get(RecId capturedPlateSummaryStatusRecId)
        {
            var getCapturePlateSummaryStatusQuery = new GetCapturePlatesSummaryStatusItem(capturedPlateSummaryStatusRecId,
                                                                                                   GetQueryFilter.Single);

            var resp = await Inquire<CapturePlatesSummaryStatusDTO>(getCapturePlateSummaryStatusQuery);

            return resp;
        }

        public async Task<PagedResponse<CapturePlatesSummaryStatusDTO>> GetAll(Pager paging, GridFilter filter, GridSort sort)
        {
            var getCapturePlateSummaryStatusQuery = new GetCapturePlatesSummaryStatusItem(new RecId(0),
                                                                                                   GetQueryFilter.All,
                                                                                                   filter,
                                                                                                   paging,
                                                                                                   sort);

            var resp = await Inquire<PagedResponse<CapturePlatesSummaryStatusDTO>>(getCapturePlateSummaryStatusQuery);

            return resp;
        }
    }
}
