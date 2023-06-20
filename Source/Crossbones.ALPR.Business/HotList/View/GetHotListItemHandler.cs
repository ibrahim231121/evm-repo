using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotList.Get
{
    public class GetHotListItemHandler : QueryHandlerBase<GetHotListItem>
    {
        protected override async Task<object> OnQuery(GetHotListItem query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();

            if (query.QueryFilter == GetQueryFilter.Count)
            {
                return new RowCount(await _repository.Count());
            }
            else
            {
                var hotlistQuery = _repository.Many(x => query.QueryFilter == GetQueryFilter.Single ? x.RecId == query.Id : true).Include(hotlist => hotlist.Source).Select(x => new HotListDTO()
                {
                    Name = x.Name,
                    Description = x.Description,
                    AlertPriority = x.AlertPriority,
                    CreatedOn = x.CreatedOn,
                    LastTimeStamp = x.LastTimeStamp,
                    LastUpdatedOn = x.LastUpdatedOn,
                    RulesExpression = x.RulesExpression,
                    RecId = x.RecId,
                    Audio = x.Urilocation,
                    Color = x.Color,
                    SourceId = x.SourceId,
                    StationId = x.StationId,
                    SourceName = x.Source == null ? string.Empty : x.Source.SourceName
                });

                switch (query.QueryFilter)
                {
                    case GetQueryFilter.Single:
                        return hotlistQuery.FirstOrDefault();
                        break;
                    case GetQueryFilter.All:
                        return await hotlistQuery.ToFilteredPagedListAsync(query.Filter, query.Paging, query.Sort, token);
                        break;
                    default:
                        break;
                }

                return null;
            }
        }
    }
}