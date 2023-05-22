using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Add
{
    public class AddHotListNumberPlateHandler : CommandHandlerBase<AddHotListNumberPlate>
    {
        protected override async Task OnMessage(AddHotListNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var hotListNumberPlateRepo = context.Get<HotListNumberPlate>();
            var hotListRepo = context.Get<Hotlist>();
            var numberPlateRepo = context.Get<NumberPlate>();

            var nameExist = await hotListNumberPlateRepo.Exists(x => x.HotListId == command.HotListID && x.NumberPlatesId == command.NumberPlatesId, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Entry with same attributes already exist");
            }
            else
            {
                await hotListNumberPlateRepo.Add(new HotListNumberPlate()
                {
                    SysSerial = command.Id,
                    HotListId = command.HotListID,
                    CreatedOn = command.CreatedOn,
                    NumberPlatesId = command.NumberPlatesId
                }, token);
                context.Success($"HotList Number Plate has been added, SysSerial:{command.Id}");
            }
        }
    }
}