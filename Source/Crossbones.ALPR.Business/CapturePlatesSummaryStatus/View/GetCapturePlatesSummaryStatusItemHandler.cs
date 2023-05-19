using Crossbones.ALPR.Common;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Business.Repositories;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.View
{
    public class GetCapturePlatesSummaryStatusItemHandler : QueryHandlerBase<GetCapturePlatesSummaryStatusItem>
    {
        protected override async Task<object> OnQuery(GetCapturePlatesSummaryStatusItem query, IQueryContext context, CancellationToken token)
        {
            var cpsRepsitory = context.Get<E.CapturePlatesSummaryStatus>();
            
            switch (query.QueryFilter)
            {
                case Enums.GetQueryFilter.Single:
                    var resp = await cpsRepsitory.One(cps => cps.SyncId == query.Id).Select(cps => DTOHelper.ConvertToDTO(cps));
                    return resp == null ? 
                                throw new RecordNotFound($"CapturePlatesSummaryStatus with Id:{query.Id} not found.") :
                                resp;
                    break;
                case Enums.GetQueryFilter.All:
                    return await cpsRepsitory.Many().Select(cps => DTOHelper.ConvertToDTO(cps)).ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await cpsRepsitory.Many().Select(cps => DTOHelper.ConvertToDTO(cps)).ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await cpsRepsitory.Many().CountAsync();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
