using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.NumberPlates.Change
{
    public class ChangeNumberPlateHandler : CommandHandlerBase<ChangeNumberPlate>
    {
        protected override async Task OnMessage(ChangeNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.NumberPlate>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);

            if (entityExist)
            {
                var numPlateExist = await _repository.Exists(x => x.LicensePlate == command.NumberPlateDTO.LicensePlate && x.RecId != command.Id, token);
                if (numPlateExist)
                {
                    throw new DuplicationNotAllowed("License Plate Already Exist");
                }
                else
                {
                    var NumberPlate = await _repository.One(x => x.RecId == command.Id);
                    NumberPlate.Status = command.NumberPlateDTO.Status;
                    NumberPlate.Notes = command.NumberPlateDTO.Notes;
                    NumberPlate.LastUpdatedOn = DateTime.UtcNow;
                    NumberPlate.FirstName = command.NumberPlateDTO.FirstName;
                    NumberPlate.LastName = command.NumberPlateDTO.LastName;
                    NumberPlate.LicensePlate = command.NumberPlateDTO.LicensePlate;
                    NumberPlate.AgencyId = command.NumberPlateDTO.AgencyId;
                    NumberPlate.StateId = command.NumberPlateDTO.StateId;
                    NumberPlate.DateOfInterest = command.NumberPlateDTO.DateOfInterest;
                    NumberPlate.ViolationInfo = command.NumberPlateDTO.ViolationInfo;
                    NumberPlate.LicenseType = command.NumberPlateDTO.LicenseType;
                    NumberPlate.Ncicnumber = command.NumberPlateDTO.NCICNumber;
                    NumberPlate.LicenseYear = command.NumberPlateDTO.LicenseYear;
                    NumberPlate.VehicleColor = command.NumberPlateDTO.VehicleColor;
                    NumberPlate.VehicleMake = command.NumberPlateDTO.VehicleMake;
                    NumberPlate.VehicleModel = command.NumberPlateDTO.VehicleModel;
                    NumberPlate.VehicleYear = command.NumberPlateDTO.VehicleYear;
                    NumberPlate.VehicleStyle = command.NumberPlateDTO.VehicleStyle;
                    NumberPlate.LicenseType = command.NumberPlateDTO.LicenseType;
                    NumberPlate.Alias = command.NumberPlateDTO.Alias;

                    await _repository.Update(NumberPlate, token);
                    context.Success($"License Plate has been updated, RecId:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("License Plate Not Found");
            }
        }
    }
}