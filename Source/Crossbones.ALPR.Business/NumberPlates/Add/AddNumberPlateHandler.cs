
using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.NumberPlates.Add
{
    public class AddNumberPlateHandler : CommandHandlerBase<AddNumberPlate>
    {
        protected override async Task OnMessage(AddNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlate>();
            var numberPlateExist = await _repository.Exists(x => x.NumberPlate1 == command.NumberPlate);

            if (numberPlateExist)
                throw new DuplicationNotAllowed("NumberPlate already exist");
            else
            {
                await _repository.Add(new NumberPlate() {
                    ImportSerialId = command.ImportSerialId,
                    AgencyId = command.AgencyId,
                    Alias = command.Alias,
                    CreatedOn = command.CreatedOn,
                    DateOfInterest = command.DateOfInterest,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    InsertType = command.InsertType,
                    LastTimeStamp = command.LastTimeStamp,
                    LicenseType = command.LicenseType,
                    LicenseYear = command.LicenseYear,
                    Ncicnumber = command.Ncicnumber,
                    Note = command.Note,
                    Notes = command.Notes,
                    NumberPlate1 = command.NumberPlate,
                    StateId = command.StateId,
                    Status = command.Status,
                    VehicleColor = command.VehicleColor,
                    VehicleMake = command.VehicleMake,
                    VehicleModel = command.VehicleModel,
                    VehicleStyle = command.VehicleStyle,
                    VehicleYear = command.VehicleYear,
                    ViolationInfo = command.ViolationInfo,
                    LastUpdatedOn = command.LastUpdatedOn,
                }, token);

                context.Success($"Licnese Plate has been added, SysSerial: {command.Id}");
            }
        }
    }
}
