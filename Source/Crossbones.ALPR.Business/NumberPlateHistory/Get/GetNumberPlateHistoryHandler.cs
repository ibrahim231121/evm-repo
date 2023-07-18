using Crossbones.ALPR.Common;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.NumberPlateHistory.Get
{
    public class GetNumberPlateHistoryHandler : QueryHandlerBase<GetNumberPlateHistoryItem>
    {
        protected override async Task<object> OnQuery(GetNumberPlateHistoryItem query, IQueryContext context, CancellationToken token)
        {
            var numberPlateRepository = context.Get<Entities.NumberPlate>();
            var capturedPlateRepository = context.Get<Entities.CapturedPlate>();
            var statesRepository = context.Get<Entities.State>();
            var hotlistNumberPlateRepository = context.Get<Entities.HotListNumberPlate>();
            var capturePlateSummaryRepository = context.Get<Entities.CapturePlatesSummary>();

            bool exist = await numberPlateRepository.Exists(numberPlate => numberPlate.RecId == query.Id, token);

            if(exist)
            {
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


                var queryHistory = numberPlateRepository.Many(numberPlate => numberPlate.RecId == query.Id).
                    Join(capturedPlateRepository.Many(),
                         np => np.LicensePlate,
                         cp => cp.NumberPlate,
                         (np, cp) => new { np, cp }).
                    Join(statesRepository.Many(),
                         f => f.cp.State,
                         state => state.RecId,
                         (f, state) => new { f.np , f.cp, state }).
                    Join(capturePlateSummaryRepository.Many(),
                        f=>f.cp.RecId,
                        cps=> cps.CapturePlateId,
                        (f, cps) => new {f.np, f.cp, f.state, cps}).
                    GroupJoin(hotlistNumberPlateRepository.Many().Include(hotListNumberPlate => hotListNumberPlate.NumberPlate).Include(hotListNumberPlate => hotListNumberPlate.HotList),
                        fh => fh.np.LicensePlate,
                        hotListNumberPlate => hotListNumberPlate.NumberPlate.LicensePlate,
                        (fh, hotListNumberPlate) => new { fh.np, fh.cp, fh.state, fh.cps, hotListNumberPlate }).
                    SelectMany(
                        hotlist => hotlist.hotListNumberPlate.DefaultIfEmpty(),
                        (z,e) => new DTO.NumberPlateHistoryDTO()
                        {
                            Id = z.np.RecId,
                            NumberPlate = z.np.LicensePlate,
                            VehicleMake = z.np.VehicleMake,
                            VehicleModel = z.np.VehicleModel,
                            VehicleYear = z.np.VehicleYear,
                            CapturedAt = z.cp.CapturedAt,
                            Confidence = z.cp.Confidence.HasValue ? z.cp.Confidence.Value : 0,
                            Latitude = z.cp.GeoLocation.Y,
                            Longitude = z.cp.GeoLocation.X,
                            TicketNumber = z.cp.TicketNumber.HasValue ? z.cp.TicketNumber.Value : 0,
                            HotlistName = e.HotList.Name,
                            State = z.state.StateName,
                            UserId = z.cps.UserId,
                            Unit = z.cps.UnitId,
                            Notes = z.cp.Notes,
                            Distance = filterByLatitude ?
                                            filterByLongitude ? z.cp.GeoLocation.Distance(new Point(longitude, latitude)) :
                                                                z.cp.GeoLocation.Distance(new Point(z.cp.GeoLocation.X, latitude)) :
                                            filterByLongitude ? z.cp.GeoLocation.Distance(new Point(longitude, z.cp.GeoLocation.Y)) : 0
                        });

                if (filterByLatitude || filterByLongitude)
                {
                    var capturePlateItems = await queryHistory.ToFilteredSortedListAsync(query.Filter, query.Sort, token);
                    capturePlateItems = capturePlateItems.Where(cpItem => cpItem.Distance < 0.05).ToList();

                    int page = 1, size = AlprConstants.PAGE_SIZE, skip = 0, count = 1;
                    if (query.Pager != null)
                    {
                        page = query.Pager.Page <= 0 ? 1 : query.Pager.Page;
                        size = query.Pager.Size <= 0 ? AlprConstants.PAGE_SIZE : query.Pager.Size;
                        skip = (query.Pager.Page - 1) * query.Pager.Size;
                        count = capturePlateItems.Count();
                    }
                    var items = capturePlateItems.Skip(skip).Take(size).ToList();

                    return new PageResponse<DTO.NumberPlateHistoryDTO>(items, count);
                }
                else
                {
                    return await queryHistory.ToFilteredPagedSortedListAsync(query.Filter, query.Pager, query.Sort, token);
                }
            }
            else
            {
                throw new RecordNotFound($"Number with ID: {query.Id} does not found.");
            }
        }
    }
}
