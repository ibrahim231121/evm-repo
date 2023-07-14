using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.HotListDataSource.Change;
using Crossbones.ALPR.Business.HotListDataSource.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using DTO = Crossbones.ALPR.Models.DTOs;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Api.HotListDataSource.Service
{
    public class HotListDataSourceItemService : ServiceBase, IHotListDataSourceItemService
    {
        readonly ISequenceProxy _hotListSequenceProxy;
        readonly IMapper mapper;

        public HotListDataSourceItemService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory, IMapper _mapper) : base(args)
        {
            _hotListSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);
            mapper = _mapper;
        }

        public async Task<RecId> Add(DTO.HotListDataSourceDTO request)
        {
            var id = new RecId(await _hotListSequenceProxy.Next(CancellationToken.None));
            AddHotListDataSourceItem _object = new(id);
            _object.Item = request;
            //var cmd = mapper.Map<, HotListDataSourceDTO>(request);
            //_object.Item = cmd;
            _object.Item.RecId = _object.Id;

            _ = await Execute(_object);
            return id;
        }


        public async Task Change(RecId recId, E.HotlistDataSource request)
        {
            ChangeHotListDataSourceItem _object = new(recId);

            var cmd = mapper.Map<E.HotlistDataSource, DTO.HotListDataSourceDTO>(request);
            _object.Item = cmd;
            _object.Item.RecId = _object.Id;

            _ = await Execute(_object);
        }


        public async Task Delete(RecId recId)
        {
            var cmd = new DeleteHotListDataSourceItem(recId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListDataSourceItem(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListDataSourceDTO> Get(RecId recId)
        {
            var query = new GetHotListDataSource(recId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<HotListDataSourceDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PageResponse<HotListDataSourceDTO>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetHotListDataSource(RecId.Empty, GetQueryFilter.All)
            { Paging = paging, GridFilter = filter, Sort = sort };
            return await Inquire<PageResponse<HotListDataSourceDTO>>(dataQuery);
        }

    }
}
