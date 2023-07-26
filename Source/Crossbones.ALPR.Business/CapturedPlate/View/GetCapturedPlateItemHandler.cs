using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

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

            var tsLifeSpan = query.EndDate.Subtract(query.StartDate);

            var lifeSpan = (tsLifeSpan.Days > 0 ? tsLifeSpan.ToString("%d") + "d " : string.Empty)
                            + (tsLifeSpan.Hours > 0 ? tsLifeSpan.ToString("%h") + "h " : string.Empty)
                            + (tsLifeSpan.Minutes > 0 ? tsLifeSpan.ToString("%m") + "m" : string.Empty);

            var cpRepsitory = context.Get<Entities.CapturedPlate>();
            var ucpRepsitory = context.Get<Entities.UserCapturedPlate>();
            var cpsRepsitory = context.Get<Entities.CapturePlatesSummary>();
            var hnpRepository = context.Get<Entities.HotListNumberPlate>();
            var npRepository = context.Get<Entities.NumberPlate>();
            var statesRepository = context.Get<Entities.State>();

            
            var ucpQueryable = ucpRepsitory.Many(cp =>
                                                        query.QueryFilter == Enums.GetQueryFilter.Single ?
                                                        (cp.CapturedId == query.Id) :
                                                        (query.UserId > 0 ? cp.UserId == query.UserId : true));

            var cpsQueryable = cpsRepsitory.Many();

            var cpQueryable = cpRepsitory.Many();

            var statesQueryable = statesRepository.Many();

            var numberPlatesQueryable = npRepository.Many();

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

            
            IQueryable<CapturedPlateDTO> cpJoinQueryable;

            var capturedPlatesubQuery = ucpQueryable.
                Join(cpQueryable,
                     f => f.CapturedId,
                     s => s.RecId,
                     (f, s) => new { f, s }).
                Where(cp => query.QueryFilter == Enums.GetQueryFilter.Single ? true : cp.s.CapturedAt >= query.StartDate && cp.s.CapturedAt <= query.EndDate).
                Join(cpsQueryable,
                     fi => fi.f.CapturedId,
                     si => si.CapturePlateId,
                     (x, y) => new { x.f, x.s, y }).
                Join(statesQueryable,
                     fs=>fs.s.State,
                     st=>st.RecId,
                     (fs, st) => new { fs.f, fs.s, fs.y, st }).
                Join(numberPlatesQueryable,
                     fn => fn.s.NumberPlate,
                     np => np.LicensePlate,
                     (fn, np) => new { fn.f, fn.s, fn.y, fn.st, np });

            if (query.HotListId == 0)
            {
                cpJoinQueryable = capturedPlatesubQuery.
                                            GroupJoin(hnpRepository.Many().Include(hotListNumberPlate => hotListNumberPlate.NumberPlate).Include(hotListNumberPlate => hotListNumberPlate.HotList),
                                             fh => fh.s.NumberPlate,
                                             sh => sh.NumberPlate.LicensePlate,
                                             (fh, sh) => new { fh.f, fh.s, fh.y, fh.st, fh.np, sh }).
                                            SelectMany(
                                                hotlist => hotlist.sh.DefaultIfEmpty(),
                                            (z, e) => new CapturedPlateDTO()
                                            {
                                                CapturedPlateId = z.s.RecId,
                                                NumberPlateId = z.np.RecId,
                                                NumberPlate = z.s.NumberPlate,
                                                Description = "",
                                                HotlistName = e.HotList.Name,
                                                CapturedAt = z.s.CapturedAt,
                                                LastUpdated = z.s.LastUpdated,
                                                UnitId = z.y.UnitId,
                                                User = z.f.UserId,
                                                Confidence = z.s.Confidence == null ? 0 : (int)z.s.Confidence,
                                                State = z.s.State,
                                                StateName = z.st.StateName,
                                                Notes = z.s.Notes,
                                                TicketNumber = z.s.TicketNumber == null ? 0 : (long)z.s.TicketNumber,
                                                Longitude = z.s.GeoLocation.X,
                                                Latitude = z.s.GeoLocation.Y,
                                                LifeSpan = lifeSpan,
                                                LoginId = z.y.LoginId,
                                                Distance = filterByLatitude ?
                                                        filterByLongitude ? z.s.GeoLocation.Distance(new Point(longitude, latitude)) :
                                                                            z.s.GeoLocation.Distance(new Point(z.s.GeoLocation.X, latitude)) :
                                                        filterByLongitude ? z.s.GeoLocation.Distance(new Point(longitude, z.s.GeoLocation.Y)) : 0
                                            });
            }
            else
            {
                cpJoinQueryable = capturedPlatesubQuery.
                                            Join(hnpRepository.Many(hotListNumPlate => hotListNumPlate.HotListId == query.HotListId).Include(hotListNumberPlate => hotListNumberPlate.NumberPlate).Include(hotListNumberPlate => hotListNumberPlate.HotList),
                                                fh => fh.s.NumberPlate,
                                                sh => sh.NumberPlate.LicensePlate,
                                                (fh, sh) => new { fh.f, fh.s, fh.y, fh.st, fh.np, sh }).
                                            Select(z => new CapturedPlateDTO()
                                            {
                                                CapturedPlateId = z.s.RecId,
                                                NumberPlateId = z.np.RecId,
                                                NumberPlate = z.s.NumberPlate,
                                                Description = "",
                                                HotlistName = z.sh.HotList.Name,
                                                CapturedAt = z.s.CapturedAt,
                                                UnitId = z.y.UnitId,
                                                User = z.f.UserId,
                                                Confidence = z.s.Confidence == null ? 0 : (int)z.s.Confidence,
                                                State = z.s.State,
                                                StateName = z.st.StateName,
                                                Notes = z.s.Notes,
                                                TicketNumber = z.s.TicketNumber == null ? 0 : (long)z.s.TicketNumber,
                                                Longitude = z.s.GeoLocation.X,
                                                Latitude = z.s.GeoLocation.Y,
                                                LifeSpan = lifeSpan,
                                                LoginId = z.y.LoginId,
                                                Distance = filterByLatitude ?
                                                        filterByLongitude ? z.s.GeoLocation.Distance(new Point(longitude, latitude)) :
                                                                            z.s.GeoLocation.Distance(new Point(z.s.GeoLocation.X, latitude)) :
                                                        filterByLongitude ? z.s.GeoLocation.Distance(new Point(longitude, z.s.GeoLocation.Y)) : 0
                                            });
            }

            if (filterByLatitude || filterByLongitude)
            {
                var capturePlateItems =  await cpJoinQueryable.ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                capturePlateItems = capturePlateItems.Where(cpItem => cpItem.Distance < 0.05).ToList();

                int page = 1, size = AlprConstants.PAGE_SIZE, skip = 0, count = 1;
                if (query.Paging != null)
                {
                    page = query.Paging.Page <= 0 ? 1 : query.Paging.Page;
                    size = query.Paging.Size <= 0 ? AlprConstants.PAGE_SIZE : query.Paging.Size;
                    skip = (query.Paging.Page - 1) * query.Paging.Size;
                    count = capturePlateItems.Count();
                }
                var items = capturePlateItems.Skip(skip).Take(size).ToList();

                return new PageResponse<CapturedPlateDTO>(items, count);
            }

            switch (query.QueryFilter)
            {
                case Enums.GetQueryFilter.Single:
                    var resp = await cpJoinQueryable.FirstOrDefaultAsync();
                    return resp == null ?
                                throw new RecordNotFound($"CapturePlatesSummaryStatus with Id:{query.Id} not found.") :
                                resp;
                    break;
                case Enums.GetQueryFilter.All:
                    return await cpJoinQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                    return await cpJoinQueryable.ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await cpJoinQueryable.ToFilteredPagedSortedListAsync(query.Filter, query.Paging, query.Sort, token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await cpJoinQueryable.CountAsync();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
