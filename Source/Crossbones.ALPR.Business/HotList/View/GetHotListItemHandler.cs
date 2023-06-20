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
                switch (query.QueryFilter)
                {
                    case GetQueryFilter.Single:
                        return _repository.Many(x => x.RecId == query.Id).Select(x => new HotListDTO()
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
                        }).FirstOrDefault();
                        break;
                    case GetQueryFilter.All:
                        return await _repository.Many().Include(hotlist => hotlist.Source).Select(x => new HotListDTO()
                        {
                            Name = x.Name,
                            Description = x.Description,
                            SourceId = x.SourceId,
                            AlertPriority = x.AlertPriority,
                            CreatedOn = x.CreatedOn,
                            LastTimeStamp = x.LastTimeStamp,
                            LastUpdatedOn = x.LastUpdatedOn,
                            RulesExpression = x.RulesExpression,
                            RecId = x.RecId,
                            Audio = x.Urilocation,
                            Color = x.Color,
                            StationId = x.StationId,
                            SourceName = x.Source == null ? string.Empty : x.Source.SourceName
                        }).ToFilteredPagedListAsync(query.Filter, query.Paging, query.Sort, token);
                        break;
                    default:
                        break;
                }

                return null;
            }
        }
    }
}