using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Crossbones.ALPR.Business.NumberPlates.Get
{
    public class GetNumberPlateHandler : QueryHandlerBase<GetNumberPlate>
    {
        readonly IMapper mapper;
        static List<Hotlist> hotLists;
        static List<HotListNumberPlate> hotListNumberPlates;
        public GetNumberPlateHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task<object> OnQuery(GetNumberPlate query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlate>();
            var hotListNumberPlateRepository = context.Get<HotListNumberPlate>();
            var hotListRepository = context.Get<Hotlist>();
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

                var data = new List<NumberPlate>();

                foreach (var item in hotListNumberPlates)
                {
                    data.Add(numberPlateList.FirstOrDefault(x => x.RecId == item.NumberPlatesId));
                }

                var res = mapper.Map<List<NumberPlateDTO>>(data);
                return res;
            }
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest
                    switch
                {
                    true => _repository.Many(x => x.RecId == query.Id).Include(x=>x.State),
                    false => _repository.Many().Include(x => x.State),
                })
                .Select(z => new NumberPlateDTO()
                {
                    RecId = z.RecId,
                    NCICNumber = z.Ncicnumber,
                    AgencyId = z.AgencyId,
                    DateOfInterest = z.DateOfInterest.ToString("yyyy-MM-dd HH:mm"),
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
                })
                .ToFilteredPagedListAsync(query.GridFilter, query.Paging, query.Sort, token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because License Plate data is not found against provided Id {query.Id}");
                }

                return data;
            }
        }

        static string ReturnHotListName(long numberPlateId)
        {
            long? hotListId = hotListNumberPlates?.FirstOrDefault(z => z.NumberPlatesId == numberPlateId)?.HotListId;
            return (hotListId > 0) ? hotLists?.FirstOrDefault(y => y.RecId == hotListId).Name : "Not Assigned";
        }
    }
}