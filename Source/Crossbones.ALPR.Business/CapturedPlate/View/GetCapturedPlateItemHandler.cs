using Crossbones.ALPR.Common;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Business.Repositories;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturedPlateItemHandler : QueryHandlerBase<GetCapturedPlateItem>
    {
        protected override async Task<object> OnQuery(GetCapturedPlateItem query, IQueryContext context, CancellationToken token)
        {
            if (query.QueryFilter != Enums.GetQueryFilter.Single && query.StartDate > query.EndDate)
            {
                throw new InvalidValue("In date range start datetime can not be greater than end datetime.");
            }

            var cpRepsitory = context.Get<E.CapturedPlate>();


            var tsLifeSpan = query.EndDate.Subtract(query.StartDate);

            var lifeSpan = (tsLifeSpan.Days > 0 ? tsLifeSpan.ToString("%d") + "d " : string.Empty)
                            + (tsLifeSpan.Hours > 0 ? tsLifeSpan.ToString("%h") + "h " : string.Empty)
                            + (tsLifeSpan.Minutes > 0 ? tsLifeSpan.ToString("%m") + "m" : string.Empty);

            IQueryable<E.CapturedPlate> capturedPlates = null;

            if (query.QueryFilter == Enums.GetQueryFilter.Single)
            {
                capturedPlates = cpRepsitory.Many(cp => cp.SysSerial == query.Id);
            }
            else
            {
                capturedPlates = cpRepsitory.Many(cp => query.CapturedPlateIds.Contains(cp.SysSerial) && 
                                                        cp.CapturedAt >= query.StartDate && cp.CapturedAt <= query.EndDate);
            }

            


            var capturedPlateQueryable = capturedPlates.Select(z => new CapturedPlateItem()
            {
                CapturedPlateId= z.SysSerial,
                PlateNumber = z.NumberPlate,
                Description = "",
                HotlistName = "",
                CapturedAt = z.CapturedAt,
                Confidence = z.Confidence,
                State = z.State,
                Notes = z.Notes,
                TicketNumber = z.TicketNumber,
                Longitude = z.GeoLocation.X,
                Latitude = z.GeoLocation.Y,
                LifeSpan = lifeSpan
            });
            
            switch (query.QueryFilter)
            {
                case Enums.GetQueryFilter.Single:
                    var resp = await capturedPlateQueryable.FirstOrDefaultAsync();
                    return resp == null ?
                                throw new RecordNotFound($"CapturePlatesSummaryStatus with Id:{query.Id} not found.") :
                                resp;
                    break;
                case Enums.GetQueryFilter.All:
                    return await capturedPlateQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await capturedPlateQueryable.ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await capturedPlateQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await capturedPlateQueryable.CountAsync();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
