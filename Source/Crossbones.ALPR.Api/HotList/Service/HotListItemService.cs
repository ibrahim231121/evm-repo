﻿using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotList.Get;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotList.Service
{
    public class HotListItemService : ServiceBase, IHotListItemService
    {
        readonly ISequenceProxy _hotListSequenceProxy;

        public HotListItemService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _hotListSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<RecId> Add(DTO.HotListDTO request)
        {
            var id = new RecId(await _hotListSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListItem(id, request);
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(RecId HotlistRecId, DTO.HotListDTO request)
        {
            var cmd = new ChangeHotListItem(HotlistRecId, request);
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId HotlistRecId)
        {
            if (HotlistRecId == null || HotlistRecId <= 0 || HotlistRecId == RecId.Empty)
            {
                HotlistRecId = new RecId(-1);
            }

            var cmd = new DeleteHotListItem(HotlistRecId);
            _ = await Execute(cmd);

        }

        public async Task DeleteMany(List<long> hotlistIds)
        {
            var cmd = new DeleteHotListItem(RecId.Empty, hotlistIds);
            _ = await Execute(cmd);
        }

        public async Task<DTO.HotListDTO> Get(RecId HotlistRecId)
        {
            var query = new GetHotListItem(HotlistRecId, GetQueryFilter.Single);
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
