using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Entities = Corssbones.ALPR.Database.Entities;
using Microsoft.EntityFrameworkCore;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.ALPR.Models.CapturedPlate;
using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.Search
{
    public class AlprAdvanceSearch : RecIdItemMessage
    {
        public AlprAdvanceSearch(RecId _id) : base(_id)
        {
        }

        public AlprAdvanceSearch() : base(RecId.Empty)
        {
        }

        public AlprAdvanceSearch(AlprSearchParams query) : base(RecId.Empty)
        {
            QueryParam = query;
        }

        public AlprAdvanceSearch(Pager pager, GridFilter gridFilter, GridSort sort) : base(RecId.Empty)
        {
            Paging = pager;
            GridFilter = gridFilter;
            Sort = sort;
        }

        public AlprAdvanceSearch(AlprSearchParams query, Pager pager, GridFilter gridFilter, GridSort sort) : this(query)
        {
            QueryParam = query;
            Paging = pager;
            GridFilter = gridFilter;
            Sort = sort;
        }

        public AlprSearchParams QueryParam { get; set; }

        public Pager Paging { get; set; }
        public GridFilter GridFilter { get; set; }
        public GridSort Sort { get; set; }

        public bool IsServerSide { get; set; }
    }

    public class AlprAdvanceSearchHandler : QueryHandlerBase<AlprAdvanceSearch>
    {
        protected async override Task<object> OnQuery(AlprAdvanceSearch query, IQueryContext context, CancellationToken token)
        {
            var _numberPlateRepository = context.Get<NumberPlate>();
            var _capturePlateRepository = context.Get<Entities.CapturedPlate>();
            var _hotListNumberPlateRepository = context.Get<HotListNumberPlate>();
            var cpRepsitory = context.Get<Entities.CapturedPlate>();
            var ucpRepsitory = context.Get<UserCapturedPlate>();
            var cpsRepsitory = context.Get<CapturePlatesSummary>();
            var statesRepository = context.Get<State>();

            var numberPlateList = _numberPlateRepository.Many().Include(x => x.State);
            var capturePlateList = _capturePlateRepository.Many();
            var hotListNumberPlateList = _hotListNumberPlateRepository.Many().Include(x => x.HotList);
            var cpQueryable = cpRepsitory.Many();
            var ucpQueryable = ucpRepsitory.Many();
            var cpsQueryable = cpsRepsitory.Many();
            var statesQueryable = statesRepository.Many();


            //Join result of Number Plate and Hot List
            var numberPlateData = _numberPlateRepository.Many()
                .Include(x => x.State)
                .GroupJoin(_hotListNumberPlateRepository
                .Many()
                .Include(y => y.NumberPlate)
                .Include(y => y.HotList),
                    ft => ft.RecId,
                    st => st.NumberPlateId,
                    (ft, st) => new { ft, st }).
                    SelectMany(
                    hotList => hotList.st.DefaultIfEmpty(),
                    (np, hl) => new DTO.NumberPlateDTO()
                    {
                        RecId = np.ft.RecId,
                        NCICNumber = np.ft.Ncicnumber,
                        AgencyId = np.ft.AgencyId,
                        DateOfInterest = np.ft.DateOfInterest,
                        LicensePlate = np.ft.LicensePlate,
                        StateId = np.ft.StateId,
                        LicenseYear = np.ft.LicenseYear,
                        LicenseType = np.ft.LicenseType,
                        VehicleYear = np.ft.VehicleYear,
                        VehicleMake = np.ft.VehicleMake,
                        VehicleModel = np.ft.VehicleModel,
                        VehicleStyle = np.ft.VehicleStyle,
                        VehicleColor = np.ft.VehicleColor,
                        Note = np.ft.Note,
                        InsertType = np.ft.InsertType,
                        CreatedOn = np.ft.CreatedOn,
                        LastUpdatedOn = np.ft.LastUpdatedOn,
                        Status = np.ft.Status,
                        FirstName = np.ft.FirstName,
                        LastName = np.ft.LastName,
                        Alias = np.ft.Alias,
                        ViolationInfo = np.ft.ViolationInfo,
                        Notes = np.ft.Notes,
                        ImportSerialId = np.ft.ImportSerialId,
                        HotList = string.IsNullOrEmpty(hl.HotList.Name) ? "Not Assigned" : hl.HotList.Name,
                        StateName = np.ft.State.StateName
                    });

            //Join result of CapturePlate and Capture Plate Summary
            var capturePlateData = cpQueryable.Join(cpsQueryable, x => x.RecId, y => y.CapturePlateId, (x, y) => new CapturedPlateDTO
            {
                CapturedPlateId = x.RecId,
                NumberPlate = x.NumberPlate,
                Description = "",
                CapturedAt = x.CapturedAt,
                LastUpdated = x.LastUpdated,
                UnitId = y.UnitId,
                User = y.UserId,
                Confidence = x.Confidence == null ? 0 : (int)x.Confidence,
                State = x.State,
                Notes = x.Notes,
                TicketNumber = x.TicketNumber == null ? 0 : (long)x.TicketNumber,
                Longitude = x.GeoLocation.X,
                Latitude = x.GeoLocation.Y,
                LoginId = y.LoginId,
            });

            var fullOuterJoin = (from np in numberPlateData
                                 from cp in capturePlateData
                                 select new DTO.AlprSearchResponse
                                 {
                                     NumberPlate = np.LicensePlate,
                                     HotlistName = np.HotList,
                                     CapturedAt = cp.CapturedAt,
                                     StateName = np.StateName,
                                     UnitId = cp.UnitId,
                                     User = cp.User.ToString(),
                                     Confidence = cp.Confidence.ToString(),
                                     TicketNumber = cp.TicketNumber.ToString(),
                                     Longitude = cp.Longitude.ToString(),
                                     Latitude = cp.Latitude.ToString(),
                                     NCICNumber = np.NCICNumber,
                                     DateOfInterest = np.DateOfInterest,
                                     LicenseYear = np.LicenseYear,
                                     LicenseType = np.LicenseType,
                                     VehicleYear = np.VehicleYear,
                                     VehicleMake = np.VehicleMake,
                                     VehicleModel = np.VehicleModel,
                                     InsertType = np.InsertType.ToString(),
                                     Status = np.Status.ToString(),
                                     Note = np.Note,
                                     FirstName = np.FirstName,
                                     LastName = np.LastName,
                                     ViolationInfo = np.ViolationInfo
                                 });

            if (query.QueryParam != null)
            {
                query.GridFilter = new GridFilter() { Logic = "and", Filters = new List<GridFilter>() };

                if (query.GridFilter?.Filters?.Count == 0)
                {
                    if (!string.IsNullOrEmpty(query.QueryParam.NumberPlate))
                    {
                        query.GridFilter.Filters.Add(new GridFilter()
                        {
                            Field = "NumberPlate",
                            Value = query.QueryParam.NumberPlate,
                            Operator = "contains",
                            FieldType = "string"
                        });
                    }

                    if (!string.IsNullOrEmpty(query.QueryParam.UnitId))
                    {
                        query.GridFilter.Filters.Add(new GridFilter()
                        {
                            Field = "UnitId",
                            Value = query.QueryParam.UnitId,
                            Operator = "contains",
                            FieldType = "string"
                        });
                    }

                    if (!string.IsNullOrEmpty(query.QueryParam.UserId))
                    {
                        query.GridFilter.Filters.Add(new GridFilter()
                        {
                            Field = "User",
                            Value = query.QueryParam.UserId,
                            Operator = "contains",
                            FieldType = "string"
                        });
                    }

                    if (!string.IsNullOrEmpty(query.QueryParam.State))
                    {
                        query.GridFilter.Filters.Add(new GridFilter()
                        {
                            Field = "StateName",
                            Value = query.QueryParam.State,
                            Operator = "contains",
                            FieldType = "string"
                        });
                    }

                    if (query.QueryParam.FromDate != null && query.QueryParam.ToDate != null)
                    {
                        query.GridFilter.Filters.Add(new GridFilter()
                        {
                            Field = "CapturedAt",
                            Value = $"{query.QueryParam.FromDate}@{query.QueryParam.ToDate}",
                            Operator = "between",
                            FieldType = "datetime"
                        });
                    }
                }

            }
            var filteredData = await fullOuterJoin.ToFilteredPagedListAsync(query.GridFilter, query.Paging, query.Sort, token);
            if (filteredData.Count() > 0)
            {
                return filteredData;
            }
            else
            {
                throw new RecordNotFound($"No record found against provided parameters.");
            }
        }
    }
}