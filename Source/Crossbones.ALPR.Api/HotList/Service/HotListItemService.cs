using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotList.Get;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.HotList.Service
{
    public class HotListItemService : ServiceBase, IHotListItemService
    {
        readonly ISequenceProxy _hotListSequenceProxy;

        public HotListItemService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _hotListSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<SysSerial> Add(HotListItem request)
        {
            var id = new SysSerial(await _hotListSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListItem(id, request);
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(SysSerial HotlistSysSerial, HotListItem request)
        {
            var cmd = new ChangeHotListItem(HotlistSysSerial, request);
            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial HotlistSysSerial)
        {
            if (HotlistSysSerial == null || HotlistSysSerial <= 0 || HotlistSysSerial == SysSerial.Empty)
            {
                HotlistSysSerial = new SysSerial(-1);
            }

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
            var query = new GetHotListItem(HotlistSysSerial, GetQueryFilter.Single);
            var res = await Inquire<HotListItem>(query);
            return res;
        }

        public async Task<PageResponse<HotListItem>> GetAll(Pager paging, GridFilter filter, GridSort sort)
        {
            var dataQuery = new GetHotListItem(SysSerial.Empty, GetQueryFilter.All) { Paging = paging, Filter = filter, Sort = sort };
            var t0 = Inquire<PageResponse<HotListItem>>(dataQuery);

            var list = await t0;

            return list;
        }
    }
}
