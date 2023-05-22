using Corssbones.ALPR.Database.Entities;
using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

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
                    true => hotListNumberPlatesRepo.Many(x => x.SysSerial == query.Id),
                    false => hotListNumberPlatesRepo.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotList Number Plate is not found against provided Id '{query.Id}'");
                }
                data.ForEach(x => { x.NumberPlate = numberPlates.FirstOrDefault(y => y.SysSerial == x.NumberPlatesId); x.HotList = hostLists.FirstOrDefault(z => z.SysSerial == x.HotListId); });
                var res = _mapper.Map<List<HotListNumberPlateItem>>(data);
                return res;
            }
        }
    }
}