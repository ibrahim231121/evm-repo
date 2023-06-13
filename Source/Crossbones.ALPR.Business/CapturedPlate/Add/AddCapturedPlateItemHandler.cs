using Crossbones.ALPR.Common.Validation;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturedPlateItemHandler : CommandHandlerBase<AddCapturedPlateItem>
    {
        protected override async Task OnMessage(AddCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var cpRepository = context.Get<E.CapturedPlate>();

            var itemExist = await cpRepository.Exists(cp => cp.SysSerial == command.Id);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturedPlate with Id:{command.Id} already exist.");
            }

            CapturedPlateValidations.ValidateCapturedPlateItem(command.CapturedPlateItem);

            var point = NtsGeometryServices.Instance.CreateGeometryFactory().CreatePoint(new Coordinate(command.CapturedPlateItem.Longitude, command.CapturedPlateItem.Latitude));
            point.SRID = 4326;

            var capturedPlate = new E.CapturedPlate()
            {
                SysSerial = command.Id,
                NumberPlate = command.CapturedPlateItem.NumberPlate,
                CapturedAt = command.CapturedPlateItem.CapturedAt,
                LastUpdated = command.CapturedPlateItem.CapturedAt,
                Confidence = command.CapturedPlateItem.Confidence,
                State = command.CapturedPlateItem.State,
                Notes = command.CapturedPlateItem.Notes,
                TicketNumber = command.CapturedPlateItem.TicketNumber,
                GeoLocation = point,
                GeoLocationCode = point.SRID
            };

            await cpRepository.Add(capturedPlate, token);

            context.Success($"CapturedPlate Item has been added, SysSerial:{command.Id}");

        }
    }
}
