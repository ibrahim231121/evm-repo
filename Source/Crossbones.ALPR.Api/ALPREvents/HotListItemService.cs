using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotList.Get;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.ALPREvents
{
    public class HotListItemService : ServiceBase, IHotListItemService
    {
        readonly ISequenceProxy _hotListSequenceProxy;

        public HotListItemService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _hotListSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<SysSerial> Add(HotListItem request)
        {
            var id = new SysSerial(await _hotListSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListItem(id)
            {
                Name = request.Name,
                Description = request.Description,
                SourceId = request.SourceId,
                RulesExpression = request.RulesExpression,
                AlertPriority = request.AlertPriority,
                CreatedOn = request.CreatedOn,
                LastUpdatedOn = request.LastUpdatedOn,
                LastTimeStamp = request.LastTimeStamp,
                StationId = request.StationId
            };
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(SysSerial HotlistSysSerial, HotListItem request)
        {
            var cmd = new ChangeHotListItem(HotlistSysSerial)
            {
                Name = request.Name,
                Description = request.Description,
                //From = request.From,
                //ReplyTo = request.ReplyTo,
                //Server = request.Server
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial HotlistSysSerial)
        {
            var cmd = new DeleteHotListItem(HotlistSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListItem(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListItem> Get(SysSerial HotlistSysSerial)
        {
            var query = new GetHotListNumberPlate(HotlistSysSerial, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<HotListItem>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<HotListItem>> GetAll(Pager paging)
        {
            var dataQuery = new GetHotListNumberPlate(SysSerial.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<HotListItem>>(dataQuery);

            var countQuery = new GetHotListNumberPlate(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}
