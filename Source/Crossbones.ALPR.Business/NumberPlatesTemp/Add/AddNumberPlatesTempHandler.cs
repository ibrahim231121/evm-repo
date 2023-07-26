using AutoMapper;
using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Add
{
    public class AddNumberPlatesTempHandler : CommandHandlerBase<AddNumberPlatesTemp>
    {
        readonly IMapper mapper;

        public AddNumberPlatesTempHandler(IMapper _mapper) => mapper = _mapper;
        protected override async Task OnMessage(AddNumberPlatesTemp command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.NumberPlateTemp>();
            var numberPlateExist = await _repository.Exists(x => x.NumberPlate == command.NumberPlate);

            if (numberPlateExist)
            {
                throw new DuplicationNotAllowed("NumberPlate already exist");
            }                
            else
            {
                await _repository.Add(mapper.Map<Entities.NumberPlateTemp>(command), token);
                context.Success($"Licnese Plate has been added, RecId: {command.Id}");
            }
        }
    }
}