using Crossbones.ALPR.Common;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Reflection;
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
            var hotlistRepository = context.Get<E.HotListNumberPlate>();

            bool applySort = false;

            if (query.Sort != null && !string.IsNullOrEmpty(query.Sort.Field))
            {
                applySort = typeof(E.CapturedPlate).GetProperty(query.Sort.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
            }

            var tsLifeSpan = query.EndDate.Subtract(query.StartDate);

            var lifeSpan = (tsLifeSpan.Days > 0 ? tsLifeSpan.ToString("%d") + "d " : string.Empty)
                            + (tsLifeSpan.Hours > 0 ? tsLifeSpan.ToString("%h") + "h " : string.Empty)
                            + (tsLifeSpan.Minutes > 0 ? tsLifeSpan.ToString("%m") + "m" : string.Empty);

            IQueryable<E.CapturedPlate> capturedPlates = null;

            if (query.QueryFilter == Enums.GetQueryFilter.Single)
            {
                capturedPlates = cpRepsitory.Many(cp => cp.RecId == query.Id);
            }
            else
            {
                capturedPlates = cpRepsitory.Many(cp => query.CapturedPlateIds.Contains(cp.RecId) &&
                                                        cp.CapturedAt >= query.StartDate && cp.CapturedAt <= query.EndDate);
            }

            if (query.HotListIds != null && (query.HotListIds.Count == 1 && query.HotListIds[0] != 0))
            {
                var hotListQueryable = hotlistRepository.Many(hotlist => query.HotListIds.Contains(hotlist.HotListId)).Include(hotList => hotList.NumberPlate);

                capturedPlates = capturedPlates.Join(hotListQueryable,
                                    f => f.NumberPlate,
                                    s => s.NumberPlate.LicensePlate,
                                    (f, s) => f);
            }

            double latitude = 0;
            bool filterByLatitude = false;
            double longitude = 0;
            bool filterByLongitude = false;

            if (query.Filter != null && query.Filter.Filters != null && query.Filter.Filters.Exists(filter => filter.Field == "Latitude"))
            {
                filterByLatitude = true;
                latitude = Convert.ToDouble(query.Filter.Filters.Find(filter => filter.Field == "Latitude").Value);

                query.Filter.Filters = query.Filter.Filters.Filter(filter => filter.Field != "Latitude").ToList();
            }

            if (query.Filter != null && query.Filter.Filters != null && query.Filter.Filters.Exists(filter => filter.Field == "Longitude"))
            {
                filterByLongitude = true;
                longitude = Convert.ToDouble(query.Filter.Filters.Find(filter => filter.Field == "Longitude").Value);

                query.Filter.Filters = query.Filter.Filters.Filter(filter => filter.Field != "Longitude").ToList();
            }

            var capturedPlateQueryable = capturedPlates.Select(z => new CapturedPlateDTO()
            {
                CapturedPlateId = z.RecId,
                NumberPlate = z.NumberPlate,
                Description = "",
                HotlistName = "",
                CapturedAt = z.CapturedAt,
                Confidence = z.Confidence == null ? 0 : (int)z.Confidence,
                State = z.State,
                Notes = z.Notes,
                TicketNumber = z.TicketNumber == null ? 0 : (long)z.TicketNumber,
                Longitude = z.GeoLocation.X,
                Latitude = z.GeoLocation.Y,
                LifeSpan = lifeSpan,
                Distance = filterByLatitude ?
                            filterByLongitude ? z.GeoLocation.Distance(new Point(longitude, latitude)) :
                                                z.GeoLocation.Distance(new Point(z.GeoLocation.X, latitude)) :
                            filterByLongitude ? z.GeoLocation.Distance(new Point(longitude, z.GeoLocation.Y)) : 0
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
                    return await capturedPlateQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, applySort ? query.Sort : null, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await capturedPlateQueryable.ToFilteredSortedListAsync(query.Filter, applySort ? query.Sort : null, token);
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await capturedPlateQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, applySort ? query.Sort : null, token);
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
