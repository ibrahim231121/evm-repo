using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Exceptions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;


namespace Corssbones.ALPR.Business.HotListDataSource.View
{
    public class GetHotListDataSourceHandler : QueryHandlerBase<GetHotListDataSource>
    {
        readonly IMapper mapper;

        public GetHotListDataSourceHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task<object> OnQuery(GetHotListDataSource query, IQueryContext context, CancellationToken token)
        {
            var _repositoryHotListDataSource = context.Get<E.HotlistDataSource>();

            if (query.Filter == GetQueryFilter.Count)
            {
                if (query.GridFilter != null)
                {
                    return new RowCount(_repositoryHotListDataSource.Many()
                        .AsQueryable().ToFilteredPagedList(query.GridFilter, query.Paging, query.Sort).TotalCount);
                }
                return new RowCount(await _repositoryHotListDataSource.Count());
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => _repositoryHotListDataSource.Many(x => x.RecId == query.Id).Include(x => x.SourceType),
                    false => _repositoryHotListDataSource.Many().Include(x => x.SourceType),
                })
                //.Select(z => mapper.Map<E.HotlistDataSource, HotListDataSourceItem>(z))
                .Select(z => new HotListDataSourceDTO()
                {
                    RecId = z.RecId,
                    Name = z.Name,
                    SourceName = z.SourceName,
                    //SourceType = mapper.Map<E.SourceType, SourceTypeItem>(z.SourceType),
                    SourceTypeName = z.SourceType.SourceTypeName,
                    SchedulePeriod = z.SchedulePeriod,
                    ConnectionType = z.ConnectionType,
                    LastRun = z.LastRun,
                    Status = z.Status,
                    StatusDesc = z.StatusDesc,
                    SourceTypeId = z.SourceTypeId,
                    SchemaDefinition = z.SchemaDefinition,
                })
                //.AsQueryable()
                .ToFilteredPagedListAsync(query.GridFilter, query.Paging, query.Sort, token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotListDataSource Item Data is not found against provided Id '{query.Id}'");
                }

                return data;
            }
        }
    }
}
