using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturedPlateItemHandler : CommandHandlerBase<AddCapturedPlateItem>
    {
        IMapper mapper;

        public AddCapturedPlateItemHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task OnMessage(AddCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var cpRepository = context.Get<Entities.CapturedPlate>();

            var itemExist = await cpRepository.Exists(cp => cp.RecId == command.Id);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturedPlate with Id:{command.Id} already exist.");
            }

            var point = NtsGeometryServices.Instance.CreateGeometryFactory().CreatePoint(new Coordinate(command.CapturedPlateItem.Longitude, command.CapturedPlateItem.Latitude));
            point.SRID = 4326;

            var capturedPlate = mapper.Map<Entities.CapturedPlate>(command.CapturedPlateItem);
            capturedPlate.RecId = command.Id;
            capturedPlate.GeoLocation = point;
            capturedPlate.GeoLocationCode = point.SRID;

            await cpRepository.Add(capturedPlate, token);

            context.Success($"CapturedPlate Item has been added, RecId:{command.Id}");
        }
    }
}