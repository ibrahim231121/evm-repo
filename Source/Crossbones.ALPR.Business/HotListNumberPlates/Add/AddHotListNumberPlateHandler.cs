using AutoMapper;
using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Add
{
    public class AddHotListNumberPlateHandler : CommandHandlerBase<AddHotListNumberPlate>
    {
        readonly IMapper mapper;
        public AddHotListNumberPlateHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task OnMessage(AddHotListNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var hotListNumberPlateRepo = context.Get<HotListNumberPlate>();
            var hotListRepo = context.Get<Hotlist>();
            var numberPlateRepo = context.Get<NumberPlate>();

            var nameExist = await hotListNumberPlateRepo.Exists(x => x.HotListId == command.Item.HotListId && x.NumberPlatesId == command.Item.NumberPlatesId, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Entry with same attributes already exist");
            }
            else
            {
                await hotListNumberPlateRepo.Add(mapper.Map<HotListNumberPlate>(command.Item), token);
                context.Success($"HotList Number Plate has been added, SysSerial:{command.Id}");
            }
        }
    }
}