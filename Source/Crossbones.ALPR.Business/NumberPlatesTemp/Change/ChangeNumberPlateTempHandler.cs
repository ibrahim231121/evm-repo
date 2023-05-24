using E = ALPR.Database.Entities;
using Crossbones.ALPR.Business.NumberPlates.Change;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossbones.Modules.Business.Handlers.Command;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Change
{
    public class ChangeNumberPlateTempHandler : CommandHandlerBase<ChangeNumberPlatesTemp>
    {
        protected override async Task OnMessage(ChangeNumberPlatesTemp command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.NumberPlatesTemp>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);

            if (entityExist)
            {
                var numPlateExist = await _repository.Exists(x => x.NumberPlate == command.NumberPlate && x.SysSerial != command.Id, token);
                if (numPlateExist)
                {
                    throw new DuplicationNotAllowed("License Plate Already Exist");
                }
                else
                {
                    var NumberPlate = await _repository.One(x => x.SysSerial == command.Id);
                    NumberPlate.Status = command.Status;
                    NumberPlate.LastUpdatedOn = DateTime.UtcNow;
                    NumberPlate.FirstName = command.FirstName;
                    NumberPlate.LastName = command.LastName;
                    NumberPlate.NumberPlate = command.NumberPlate;
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
