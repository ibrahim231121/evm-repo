using AutoMapper;
using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.NumberPlates.Add
{
    public class AddNumberPlateHandler : CommandHandlerBase<AddNumberPlate>
    {
        readonly IMapper mapper;
        public AddNumberPlateHandler(IMapper _mapper) => mapper = _mapper;
        protected override async Task OnMessage(AddNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlate>();
            var numberPlateExist = await _repository.Exists(x => x.LicensePlate == command.NumberPlateDTO.LicensePlate);

            if (numberPlateExist)
            {
                throw new DuplicationNotAllowed("Number Plate already exist");
            }
            else
            {
                await _repository.Add(mapper.Map<NumberPlate>(command.NumberPlateDTO), token);

                context.Success($"Licnese Plate has been added, RecId: {command.Id}");
            }
        }
    }
}