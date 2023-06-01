using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListNumberPlates.Add;
using Corssbones.ALPR.Business.HotListNumberPlates.Change;
using Corssbones.ALPR.Business.HotListNumberPlates.Delete;
using Corssbones.ALPR.Business.HotListNumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    public class HotListNumberPlateService : ServiceBase, IHotListNumberPlateService
    {
        readonly ISequenceProxy hotListNumberSequenceProxy;
        readonly IMapper mapper;
        public HotListNumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => hotListNumberSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotListNumberPlate);

        public async Task<SysSerial> Add(HotListNumberPlateItem request)
        {
            var id = new SysSerial(await hotListNumberSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListNumberPlate(id)
            {
                HotListID = request.HotListId,
                CreatedOn = DateTime.UtcNow,
                NumberPlatesId = request.NumberPlatesId,
            };
            await Execute(cmd);
            return id;
        }

        public async Task Change(SysSerial Id, HotListNumberPlateItem request)
        {
            var cmd = new ChangeHotListNumberPlate(Id)
            {
                HotListID = request.HotListId,
                CreatedOn = DateTime.UtcNow,
                NumberPlatesId = request.NumberPlatesId,
                LastUpdatedOn = DateTime.UtcNow,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial Id)
        {
            var cmd = new DeleteHotListNumberPlate(Id);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListNumberPlate(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListNumberPlateItem> Get(SysSerial Id)
        {
            var query = new GetHotListNumberPlate(Id, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<HotListNumberPlateItem>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<HotListNumberPlateItem>> GetAll(Pager paging)
        {
            var dataQuery = new GetHotListNumberPlate(SysSerial.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<HotListNumberPlateItem>>(dataQuery);

            var countQuery = new GetHotListNumberPlate(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}