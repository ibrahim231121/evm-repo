using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;

namespace Corssbones.ALPR.Business.ExportDetail.Get
{
    public class GetExportDetailHandler : QueryHandlerBase<GetExportDetail>
    {
        readonly IMapper mapper;

        public GetExportDetailHandler(IMapper _mapper) => mapper = _mapper;
        protected override async Task<object> OnQuery(GetExportDetail query, IQueryContext context, CancellationToken token)
        {

            var _repository = context.Get<Entities.AlprExportDetail>();

            if (query.Filter == GetQueryFilter.Count)
            {
                return new RowCount(await _repository.Count());
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => _repository.Many(x => x.RecId == query.Id),
                    false => _repository.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because Export Detail is not found against provided Id '{query.Id}'");
                }

                var res = mapper.Map<List<DTO.ExportDetailDTO>>(data);

                return res;
            }
        }
    }
}