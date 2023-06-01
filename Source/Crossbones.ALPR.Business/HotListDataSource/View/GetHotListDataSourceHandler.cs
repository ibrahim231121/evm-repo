using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models.Items;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;

using E = Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using LanguageExt;

namespace Corssbones.ALPR.Business.HotListDataSource.View
{
    public class GetHotListDataSourceHandler : QueryHandlerBase<GetHotListDataSource>
    {
        protected override async Task<object> OnQuery(GetHotListDataSource query, IQueryContext context, CancellationToken token)
        {
            var _repositoryHotListDataSource = context.Get<E.HotlistDataSource>();

            if (query.Filter == GetQueryFilter.Count)
            {
                return new RowCount(await _repositoryHotListDataSource.Count());
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest switch
                {
                    true => _repositoryHotListDataSource.Many(x => x.SysSerial == query.Id),
                    false => _repositoryHotListDataSource.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotList Item Data is not found against provided Id '{query.Id}'");
                }

                var sourceTypeRepo = await context.Get<E.SourceType>().Many().ToListAsync(token);

                var res = data
                    .Join(sourceTypeRepo, x => x.SourceTypeId, y => y.SysSerial, (x, y) => new { x, y })
                    .Select(z => new HotListDataSourceItem()
                    {
                        //SysSerial = z.x.SysSerial,
                        Name = z.x.Name,
                        SourceName = z.x.SourceName,
                        //AgencyId = z.x.AgencyId,
                        //SourceTypeId = z.x.SourceTypeId,
                        //SchedulePeriod = z.x.SchedulePeriod,
                        //LastUpdated = z.x.LastUpdated,
                        //IsExpire = z.x.IsExpire,
                        //SchemaDefinition = z.x.SchemaDefinition,
                        //LastUpdateExternalHotListId = z.x.LastUpdateExternalHotListId,
                        //ConnectionType = z.x.ConnectionType,
                        //Userid = z.x.Userid,
                        //LocationPath = z.x.LocationPath,
                        //Password = z.x.Password,
                        //Port = z.x.Port,
                        //LastRun = z.x.LastRun,
                        //Status = z.x.Status,
                        //SkipFirstLine = z.x.SkipFirstLine,
                        //StatusDesc = z.x.StatusDesc,
                        //Hotlists = x.Hotlists,
                        SourceType = new SourceTypeItem
                        {
                            SourceTypeName = z.y.SourceTypeName,
                            Description = z.y.Description,
                        },
                    });

                return res;
            }
        }
    }
}
