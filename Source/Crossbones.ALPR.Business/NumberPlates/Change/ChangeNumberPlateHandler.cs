using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.NumberPlates.Change
{
    public class ChangeNumberPlateHandler : CommandHandlerBase<ChangeNumberPlate>
    {
        protected override async Task OnMessage(ChangeNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlate>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);

            if (entityExist)
            {
                var numPlateExist = await _repository.Exists(x => x.LicensePlate == command.NumberPlate && x.SysSerial != command.Id, token);
                if (numPlateExist)
                {
                    throw new DuplicationNotAllowed("License Plate Already Exist");
                }
                else
                {
                    var NumberPlate = await _repository.One(x => x.SysSerial == command.Id);
                    NumberPlate.Status = command.Status;
                    NumberPlate.Notes = command.Notes;
                    NumberPlate.LastUpdatedOn = DateTime.UtcNow;
                    NumberPlate.FirstName = command.FirstName;
                    NumberPlate.LastName = command.LastName;
                    NumberPlate.LicensePlate = command.NumberPlate;
                    NumberPlate.AgencyId = command.AgencyId;
                    NumberPlate.StateId = command.StateId;
                    NumberPlate.DateOfInterest = command.DateOfInterest;
                    NumberPlate.ViolationInfo = command.ViolationInfo;
                    NumberPlate.LicenseType = command.LicenseType;
                    NumberPlate.Ncicnumber = command.Ncicnumber;
                    NumberPlate.LicenseYear = command.LicenseYear;
                    NumberPlate.VehicleColor = command.VehicleColor;
                    NumberPlate.VehicleMake = command.VehicleMake;
                    NumberPlate.VehicleModel = command.VehicleModel;
                    NumberPlate.VehicleYear = command.VehicleYear;
                    NumberPlate.VehicleStyle = command.VehicleStyle;
                    NumberPlate.LicenseType = command.LicenseType;
                    NumberPlate.Alias = command.Alias;

                    await _repository.Update(NumberPlate, token);
                    context.Success($"License Plate has been updated, SysSerial:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("License Plate Not Found");
            }
        }
    }
}