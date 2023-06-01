using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models.Items;

namespace Corssbones.ALPR.Business.HotList.View
{
    public class GetHotListItemHandler : QueryHandlerBase<GetHotListItem>
    {
        protected override async Task<object> OnQuery(GetHotListItem query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();

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
                    throw new RecordNotFound($"Unable to process your request because HotList Item Data is not found against provided Id '{query.Id}'");
                }

                var res = data.Select(x => new HotListItem()
                {
                    Name = x.Name,
                    Description = x.Description,
                    SourceId = x.SourceId,
                    RulesExpression = x.RulesExpression,
                    AlertPriority = x.AlertPriority,
                    CreatedOn = x.CreatedOn,
                    LastUpdatedOn = x.LastUpdatedOn,
                    //LastTimeStamp = x.LastTimeStamp,
                    StationId = x.StationId,
                });

                return res;
            }
        }
    }
}
