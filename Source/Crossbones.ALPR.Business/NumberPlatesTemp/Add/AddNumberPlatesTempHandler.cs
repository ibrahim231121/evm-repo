using E = Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Add
{
    public class AddNumberPlatesTempHandler : CommandHandlerBase<AddNumberPlatesTemp>
    {
        protected override async Task OnMessage(AddNumberPlatesTemp command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.NumberPlatesTemp>();
            var numberPlateExist = await _repository.Exists(x => x.NumberPlate == command.NumberPlate);

            if (numberPlateExist)
                throw new DuplicationNotAllowed("NumberPlate already exist");
            else
            {
                await _repository.Add(new E.NumberPlatesTemp()
                {
                    ImportSerialId = command.ImportSerialId,
                    AgencyId = command.AgencyId,
                    Alias = command.Alias,
                    DateOfInterest = command.DateOfInterest,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    InsertType = command.InsertType,
                    LicenseType = command.LicenseType,
                    LicenseYear = command.LicenseYear,
                    Ncicnumber = command.Ncicnumber,
                    Note = command.Note,
                    NumberPlate = command.NumberPlate,
                    StateId = command.StateId,
                    Status = command.Status,
                    VehicleColor = command.VehicleColor,
                    VehicleMake = command.VehicleMake,
                    VehicleModel = command.VehicleModel,
                    VehicleStyle = command.VehicleStyle,
                    VehicleYear = command.VehicleYear,
                    ViolationInfo = command.ViolationInfo,
                    CreatedOn = DateTime.UtcNow,
                    LastUpdatedOn = DateTime.UtcNow
                }, token);

                context.Success($"Licnese Plate has been added, SysSerial: {command.Id}");
            }
        }
    }
}
