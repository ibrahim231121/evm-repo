using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Entites = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Microsoft.EntityFrameworkCore;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.NumberPlates.Get
{
    public class GetNumberPlateHandler : QueryHandlerBase<GetNumberPlate>
    {
        readonly IMapper mapper;
        static List<Entites.Hotlist> hotLists;
        static List<Entites.HotListNumberPlate> hotListNumberPlates;
        public GetNumberPlateHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task<object> OnQuery(GetNumberPlate query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<Entites.NumberPlate>();
            var hotListNumberPlateRepository = context.Get<Entites.HotListNumberPlate>();
            var hotListRepository = context.Get<Entites.Hotlist>();
            hotListNumberPlates = await hotListNumberPlateRepository.Many().ToListAsync();
            hotLists = await hotListRepository.Many().ToListAsync();

            if (query.Filter == GetQueryFilter.Count)
            {
                if (query.GridFilter != null)
                {
                    return new RowCount(_repository.Many().AsQueryable().ToFilteredPagedList(query.GridFilter, query.Paging, query.Sort).TotalCount);
                }
                else
                {
                    return new RowCount(await _repository.Count());
                }
            }
            else if (query.Filter == GetQueryFilter.FilterByHostList)
            {
                hotListNumberPlates = await hotListNumberPlateRepository.Many(x => x.HotListId == query.HotListID).ToListAsync();
                var numberPlateList = await _repository.Many().Include(x => x.State).ToListAsync();

                var data = new List<Entites.NumberPlate>();

                foreach (var item in hotListNumberPlates)
                {
                    data.Add(numberPlateList.FirstOrDefault(x => x.RecId == item.NumberPlateId));
                }

                var res = mapper.Map<List<DTO.NumberPlateDTO>>(data);
                return res;
            }
            else if (query.Filter == GetQueryFilter.SearchByNumberPlate)
            {
                var numberPlateList = await _repository
                    .Many(x=>x.LicensePlate.ToLower().StartsWith(query.NumberPlateString.ToLower()))
                    .OrderByDescending(x => x.CreatedOn)
                    .Take(100)
                    .ToListAsync();

                if (numberPlateList.Count == 0)
                {
                    throw new RecordNotFound($"Number Plate against following input {query.NumberPlateString} was not found.");
                }
                else
                {
                    return numberPlateList.Select(x => x.LicensePlate).ToList();
                }                
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                if (singleRequest)
                {
                    var data = _repository.Many(x => x.RecId == query.Id).Include(x => x.State).Select(z => new DTO.NumberPlateDTO()
                    {
                        RecId = z.RecId,
                        NCICNumber = z.Ncicnumber,
                        AgencyId = z.AgencyId,
                        DateOfInterest = z.DateOfInterest,
                        LicensePlate = z.LicensePlate,
                        StateId = z.StateId,
                        LicenseYear = z.LicenseYear,
                        LicenseType = z.LicenseType,
                        VehicleYear = z.VehicleYear,
                        VehicleMake = z.VehicleMake,
                        VehicleModel = z.VehicleModel,
                        VehicleStyle = z.VehicleStyle,
                        VehicleColor = z.VehicleColor,
                        Note = z.Note,
                        InsertType = z.InsertType,
                        CreatedOn = z.CreatedOn,
                        LastUpdatedOn = z.LastUpdatedOn,
                        Status = z.Status,
                        FirstName = z.FirstName,
                        LastName = z.LastName,
                        Alias = z.Alias,
                        ViolationInfo = z.ViolationInfo,
                        Notes = z.Notes,
                        ImportSerialId = z.ImportSerialId,
                        HotList = ReturnHotListName(z.RecId),
                        StateName = z.State.StateName
                    });
                    return data.FirstOrDefault();
                }
                else
                {
                    var data = await _repository.Many().Include(x => x.State).GroupJoin(hotListNumberPlateRepository.Many().Include(y => y.NumberPlate).Include(y => y.HotList),
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
                         }).ToFilteredPagedListAsync(query.GridFilter, query.Paging, query.Sort, token);

                    return data;
                }

            }
        }

        static string ReturnHotListName(long numberPlateId)
        {
            long? hotListId = hotListNumberPlates?.FirstOrDefault(z => z.NumberPlateId == numberPlateId)?.HotListId;
            return (hotListId > 0) ? hotLists?.FirstOrDefault(y => y.RecId == hotListId).Name : "Not Assigned";
        }
    }
}