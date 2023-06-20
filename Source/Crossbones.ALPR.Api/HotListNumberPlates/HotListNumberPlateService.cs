using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListNumberPlates.Add;
using Corssbones.ALPR.Business.HotListNumberPlates.Change;
using Corssbones.ALPR.Business.HotListNumberPlates.Delete;
using Corssbones.ALPR.Business.HotListNumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    public class HotListNumberPlateService : ServiceBase, IHotListNumberPlateService
    {
        readonly ISequenceProxy hotListNumberSequenceProxy;
        readonly IMapper mapper;
        public HotListNumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => hotListNumberSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotListNumberPlate);

        public async Task<RecId> Add(HotListNumberPlateDTO request)
        {
            var id = new RecId(await hotListNumberSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListNumberPlate(id)
            {
                HotListID = request.HotListId,
                CreatedOn = DateTime.UtcNow,
                NumberPlatesId = request.NumberPlatesId,
            };
            await Execute(cmd);
            return id;
        }

        public async Task Change(RecId Id, HotListNumberPlateDTO request)
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

        public async Task Delete(RecId Id)
        {
            var cmd = new DeleteHotListNumberPlate(Id);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListNumberPlateDTO> Get(RecId Id)
        {
            var query = new GetHotListNumberPlate(Id, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<HotListNumberPlateDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<HotListNumberPlateDTO>> GetAll(Pager paging)
        {
            var dataQuery = new GetHotListNumberPlate(RecId.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<HotListNumberPlateDTO>>(dataQuery);

            var countQuery = new GetHotListNumberPlate(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}