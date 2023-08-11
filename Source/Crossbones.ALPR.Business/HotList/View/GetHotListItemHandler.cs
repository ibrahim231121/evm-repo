using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Exceptions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using DTO = Crossbones.ALPR.Models.DTOs;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotList.Get
{
    public class GetHotListItemHandler : QueryHandlerBase<GetHotListItem>
    {
        readonly IMapper _mapper;
        public GetHotListItemHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<object> OnQuery(GetHotListItem query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.Hotlist>();

            if (query.QueryFilter == GetQueryFilter.Count)
            {
                return new RowCount(await _repository.Count(token));
            }
            else
            {
                var singleRequest = query.QueryFilter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => _repository.Many(x => x.RecId == query.Id, token).Include(x => x.Source),
                    false => _repository.Many(token).Include(x => x.Source),
                })
                .Select(x => new DTO.HotListDTO()
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
                    SourceName = x.Source == null ? string.Empty : x.Source.Name

                }).ToFilteredPagedListAsync(query.Filter, query.Paging, query.Sort, token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotList Record is not found against provided Id '{query.Id}'");
                }

                return data;
            }
        }
    }
}