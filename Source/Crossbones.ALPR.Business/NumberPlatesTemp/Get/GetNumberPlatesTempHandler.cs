using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Get
{
    public class GetNumberPlatesTempHandler : QueryHandlerBase<GetNumberPlatesTemp>
    {
        readonly IMapper mapper;
        public GetNumberPlatesTempHandler(IMapper _mapper) => mapper = _mapper;
        protected override async Task<object> OnQuery(GetNumberPlatesTemp query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlateTemp>();

            if (query.Filter == GetQueryFilter.Count)
                return new RowCount(await _repository.Count());
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest
                    switch
                {
                    true => _repository.Many(x => x.RecId == query.Id),
                    false => _repository.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because License Plate data is not found against provided Id {query.Id}");
                }
                
                var res = mapper.Map<List<NumberPlateTempDTO>>(data);
                return res;
            }
        }
    }
}