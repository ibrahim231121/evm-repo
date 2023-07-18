using Crossbones.ALPR.Common;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturePlatesSummaryItemHandler : QueryHandlerBase<GetCapturePlatesSummaryItem>
    {
        protected override async Task<object> OnQuery(GetCapturePlatesSummaryItem query, IQueryContext context, CancellationToken token)
        {
            bool applySort = false;

            if (query.Sort != null && !string.IsNullOrEmpty(query.Sort.Field))
            {
                applySort = typeof(CapturePlatesSummaryDTO).GetProperty(query.Sort.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
            }

            var cpsRepsitory = context.Get<Entities.CapturePlatesSummary>();

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
                    else if (query.CapturedPlateId > 0)
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
                                                        .ToFilteredPagedSortedListAsync(query.Filter, query.Paging, applySort ? query.Sort : null, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await cpsRepsitory.Many(cps => query.CapturedPlateIds == null ? true : query.CapturedPlateIds.Contains(cps.CapturePlateId))
                                                         .ToFilteredSortedListAsync(query.Filter, applySort ? query.Sort : null, token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await cpsRepsitory.Many().CountAsync();
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await cpsRepsitory.Many(cps => cps.UserId == query.UserId)
                                                        .Select(cps => DTOHelper.ConvertToDTO(cps))
                                                        .ToFilteredPagedSortedListAsync(query.Filter, query.Paging, applySort ? query.Sort : null, token);
                    break;
                case Enums.GetQueryFilter.AllByUserWithOutPaging:
                    return await cpsRepsitory.Many(cps => cps.UserId == query.UserId)
                                                        .ToFilteredSortedListAsync(query.Filter, applySort ? query.Sort : null, token);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
