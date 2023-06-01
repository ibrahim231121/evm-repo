using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;
using M = Crossbones.ALPR.Models.Items;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.View
{
    public class GetNumberPlatesTempHandler : QueryHandlerBase<GetNumberPlatesTemp>
    {
        protected override async Task<object> OnQuery(GetNumberPlatesTemp query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<E.NumberPlatesTemp>();

            if (query.Filter == GetQueryFilter.Count)
                return new RowCount(await _repository.Count());
            else
            {
                var singleRequest = query.Filter == GetQueryFilter.Single;
                var data = await (singleRequest
                    switch
                {
                    true => _repository.Many(x => x.SysSerial == query.Id),
                    false => _repository.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                    throw new RecordNotFound($"Unable to process your request because License Plate data is not found against provided Id {query.Id}");

                var res = data.Select(x => new M.NumberPlatesTemp()
                {
                    AgencyId = x.AgencyId,
                    Ncicnumber = x.Ncicnumber,
                    Alias = x.Alias,
                    CreatedOn = x.CreatedOn,
                    DateOfInterest = x.DateOfInterest.GetValueOrDefault(),
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    InsertType = (short)x.InsertType,
                    LastUpdatedOn = x.LastUpdatedOn,
                    LicenseType = x.LicenseType,
                    LicenseYear = x.LicenseYear,
                    Note = x.Note,
                    StateId = x.StateId,
                    VehicleColor = x.VehicleColor,
                    Status = x.Status,
                    VehicleMake = x.VehicleMake,
                    VehicleModel = x.VehicleModel,
                    VehicleStyle = x.VehicleStyle,
                    VehicleYear = x.VehicleYear,
                    ViolationInfo = x.ViolationInfo,
                    NumberPlate = x.NumberPlate,
                    ImportSerialId = x.ImportSerialId,
                    SysSerial = x.SysSerial


                });
                return res;
            }
        }
    }
}
