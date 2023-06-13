using Crossbones.ALPR.Common.Validation;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturedPlateHandler : CommandHandlerBase<ChangeCapturedPlateItem>
    {
        protected override async Task OnMessage(ChangeCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var cpRepository = context.Get<E.CapturedPlate>();
            bool entityExist = await cpRepository.Exists(x => x.SysSerial == command.Id, token);

            if (entityExist)
            {
                CapturedPlateValidations.ValidateCapturedPlateItem(command.UpdateItem);

                var point = NtsGeometryServices.Instance.CreateGeometryFactory().CreatePoint(new Coordinate(command.UpdateItem.Longitude, command.UpdateItem.Latitude));
                point.SRID = 4326;

                var capturedPlate = await cpRepository.One(x => x.SysSerial == command.Id);

                capturedPlate.Confidence = command.UpdateItem.Confidence;
                capturedPlate.State = command.UpdateItem.State;
                capturedPlate.Notes = command.UpdateItem.Notes;
                capturedPlate.TicketNumber = command.UpdateItem.TicketNumber;
                capturedPlate.GeoLocation = point;
                capturedPlate.GeoLocationCode = point.SRID;

                await cpRepository.Update(capturedPlate, token);

                context.Success($"CapturedPlate item has been updated, SysSerial:{command.Id}");
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
