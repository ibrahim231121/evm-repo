using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Change
{
    public class ChangeNumberPlateTempHandler : CommandHandlerBase<ChangeNumberPlatesTemp>
    {
        protected override async Task OnMessage(ChangeNumberPlatesTemp command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlateTemp>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);

            if (entityExist)
            {
                var numPlateExist = await _repository.Exists(x => x.NumberPlate == command.NumberPlate && x.RecId != command.Id, token);
                if (numPlateExist)
                {
                    throw new DuplicationNotAllowed("License Plate Already Exist");
                }
                else
                {
                    var NumberPlate = await _repository.One(x => x.RecId == command.Id);
                    NumberPlate.Status = command.Status;
                    NumberPlate.LastUpdatedOn = DateTime.UtcNow;
                    NumberPlate.FirstName = command.FirstName;
                    NumberPlate.LastName = command.LastName;
                    NumberPlate.NumberPlate = command.NumberPlate;
                    NumberPlate.AgencyId = command.AgencyId;
                    //NumberPlate.StateId = command.StateId;
                    NumberPlate.DateOfInterest = command.DateOfInterest;
                    NumberPlate.ViolationInfo = command.ViolationInfo;
                    NumberPlate.LicenseType = command.LicenseType;
                    NumberPlate.NCICNumber = command.Ncicnumber;
                    NumberPlate.LicenseYear = command.LicenseYear;
                    NumberPlate.VehicleColor = command.VehicleColor;
                    NumberPlate.VehicleMake = command.VehicleMake;
                    NumberPlate.VehicleModel = command.VehicleModel;
                    NumberPlate.VehicleYear = command.VehicleYear;
                    NumberPlate.VehicleStyle = command.VehicleStyle;
                    NumberPlate.LicenseType = command.LicenseType;
                    NumberPlate.Alias = command.Alias;

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