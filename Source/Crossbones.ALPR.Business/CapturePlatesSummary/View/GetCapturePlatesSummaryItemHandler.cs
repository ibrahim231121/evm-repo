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
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.View
{
    public class GetCapturePlatesSummaryItemHandler : QueryHandlerBase<GetCapturePlatesSummaryItem>
    {
        protected override async Task<object> OnQuery(GetCapturePlatesSummaryItem query, IQueryContext context, CancellationToken token)
        {
            var cpsRepsitory = context.Get<E.CapturePlatesSummary>();
            
            switch (query.QueryFilter)
            {
                case Enums.GetQueryFilter.Single:
                    if (query.UserId > 0 && query.CapturedPlateId > 0)
                    {
                        var resp = await cpsRepsitory.One(cps => cps.UserId == query.UserId && cps.CapturePlateId == query.CapturedPlateId).Select(cps => DTOHelper.ConvertToDTO(cps));

                        return resp == null ?
                                    throw new RecordNotFound($"CapturePlatesSummary with User Id:{query.UserId} and CapturePlateId:{query.CapturedPlateId} not found.") :
                                    resp;
                    }
                    else if(query.CapturedPlateId > 0)
                    {
                        var resp = await cpsRepsitory.One(cps => cps.CapturePlateId == query.CapturedPlateId).Select(cps => DTOHelper.ConvertToDTO(cps));

                        return resp == null ?
                                    throw new RecordNotFound($"CapturePlatesSummary with CapturePlateId:{query.CapturedPlateId} not found.") :
                                    resp;
                    }

                    throw new NotImplementedException();
                    break;
                case Enums.GetQueryFilter.All:
                    return await cpsRepsitory.Many(cps => query.CapturedPlateIds == null ? true : query.CapturedPlateIds.Contains(cps.CapturePlateId))
                                                        .Select(cps => DTOHelper.ConvertToDTO(cps))
                                                        .ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await cpsRepsitory.Many(cps => query.CapturedPlateIds == null ? true : query.CapturedPlateIds.Contains(cps.CapturePlateId))
                                                         .Select(cps => DTOHelper.ConvertToDTO(cps))
                                                         .ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await cpsRepsitory.Many().CountAsync();
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await cpsRepsitory.Many(cps=>cps.UserId == query.UserId)
                                                        .Select(cps => DTOHelper.ConvertToDTO(cps))
                                                        .ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllByUserWithOutPaging:
                    return await cpsRepsitory.Many(cps => cps.UserId == query.UserId)
                                                        .Select(cps => DTOHelper.ConvertToDTO(cps))
                                                        .ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
