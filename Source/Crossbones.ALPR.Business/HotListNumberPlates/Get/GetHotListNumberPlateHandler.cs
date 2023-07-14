using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Get
{
    public class GetHotListNumberPlateHandler : QueryHandlerBase<GetHotListNumberPlate>
    {
        readonly IMapper _mapper;
        public GetHotListNumberPlateHandler(IMapper mapper)
        {
            _mapper = mapper;

        }
        protected override async Task<object> OnQuery(GetHotListNumberPlate query, IQueryContext context, CancellationToken token)
        {
            var hotListNumberPlatesRepo = context.Get<HotListNumberPlate>();
            var hostLists = await context.Get<Hotlist>().Many().ToListAsync(token);
            var numberPlates = await context.Get<NumberPlate>().Many().ToListAsync(token);

            if (query.Filter == GetQueryFilter.Count)
            {
                return new RowCount(await hotListNumberPlatesRepo.Count());
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => hotListNumberPlatesRepo.Many(x => x.RecId == query.Id).Include(x => x.NumberPlate).Include(x => x.HotList),
                    false => hotListNumberPlatesRepo.Many().Include(x => x.NumberPlate).Include(x => x.HotList),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotList Number Plate is not found against provided Id '{query.Id}'");
                }
                var res = _mapper.Map<List<DTO.HotListNumberPlateDTO>>(data);
                return res;
            }
        }
    }
}