using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.HotListDataSource.Change;
using Crossbones.ALPR.Business.HotListDataSource.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
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

        public async Task<SysSerial> Add(E.HotlistDataSource request)
        {
            var id = new SysSerial(await _hotListSequenceProxy.Next(CancellationToken.None));
            AddHotListDataSourceItem _object = new(id);

            var cmd = mapper.Map<E.HotlistDataSource, HotListDataSourceItem>(request);
            _object.Item = cmd;
            _object.Item.SysSerial = _object.Id;

            _ = await Execute(_object);
            return id;
        }


        public async Task Change(SysSerial SysSerial, E.HotlistDataSource request)
        {
            ChangeHotListDataSourceItem _object = new(SysSerial);

            var cmd = mapper.Map<E.HotlistDataSource, HotListDataSourceItem>(request);
            _object.Item = cmd;
            _object.Item.SysSerial = _object.Id;

            _ = await Execute(_object);
        }


        public async Task Delete(SysSerial HotlistSysSerial)
        {
            var cmd = new DeleteHotListDataSourceItem(HotlistSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListDataSourceItem(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListDataSourceItem> Get(SysSerial HotlistSysSerial)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var query = new GetHotListDataSource(HotlistSysSerial, GetQueryFilter.Single, filter, sort);
            var res = await Inquire<IEnumerable<HotListDataSourceItem>>(query);
            return res.FirstOrDefault();
        }

        public Task<PageResponse<HotListDataSourceItem>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetHotListDataSource(SysSerial.Empty, GetQueryFilter.All, filter, sort) { Paging = paging };
            return Inquire<PageResponse<HotListDataSourceItem>>(dataQuery);
        }

    }
}
