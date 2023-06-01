
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.View
{
    public class GetSourceTypeHandler : QueryHandlerBase<GetSourceType>
    {
        protected override async Task<object> OnQuery(GetSourceType query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<E.SourceType>();

            if (query.Filter == GetQueryFilter.Count)
            {
                return new RowCount(await _repository.Count());
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => _repository.Many(x => x.SysSerial == query.Id),
                    false => _repository.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because SourceType Item Data is not found against provided Id '{query.Id}'");
                }

                var res = data.Select(x => new SourceTypeItem()
                {
                    SourceTypeName = x.SourceTypeName,
                    Description = x.Description
                });

                return res;
            }
        }
    }
}
