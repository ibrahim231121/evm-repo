using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotList.Get;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
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

        public async Task<RecId> Add(DTO.HotListDTO request)
        {
            var id = new RecId(await _hotListSequenceProxy.Next(CancellationToken.None));
            request.RecId = id;
            var cmd = new AddHotListItem(id, request);
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(RecId recId, DTO.HotListDTO request)
        {
            var cmd = new ChangeHotListItem(recId, request);
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId recId)
        {
            if (recId == null || recId <= 0 || recId == RecId.Empty)
            {
                recId = new RecId(-1);
            }

            var cmd = new DeleteHotListItem(recId);
            _ = await Execute(cmd);

        }

        public async Task DeleteMany(List<long> hotlistIds)
        {
            var cmd = new DeleteHotListItem(RecId.Empty, hotlistIds);
            _ = await Execute(cmd);
        }

        public async Task<DTO.HotListDTO> Get(RecId recId)
        {
            var query = new GetHotListItem(recId, GetQueryFilter.Single);
            var res = await Inquire<DTO.HotListDTO>(query);
            return res;
        }

        public async Task<PageResponse<DTO.HotListDTO>> GetAll(Pager paging, GridFilter filter, GridSort sort)
        {
            var dataQuery = new GetHotListItem(RecId.Empty, GetQueryFilter.All) { Paging = paging, Filter = filter, Sort = sort };
            var t0 = Inquire<PageResponse<DTO.HotListDTO>>(dataQuery);

            var list = await t0;

            return list;
        }
    }
}
