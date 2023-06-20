using Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Change
{
    public class ChangeHotListNumberPlateHandler : CommandHandlerBase<ChangeHotListNumberPlate>
    {
        protected override async Task OnMessage(ChangeHotListNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<HotListNumberPlate>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.HotListId == command.HotListID && x.NumberPlatesId == command.NumberPlatesId && x.RecId != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Entry with same attributes already exist");
                }
                else
                {
                    var hotListItem = await _repository.One(x => x.RecId == command.Id);
                    hotListItem.HotListId = command.HotListID;
                    hotListItem.NumberPlatesId = command.NumberPlatesId;
                    hotListItem.LastUpdatedOn = DateTime.UtcNow;

                    await _repository.Update(hotListItem, token);
                    context.Success($"HotList Number Plate has been updated, RecId:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("HotList Number Plate Not Found");
            }
        }
    }
}