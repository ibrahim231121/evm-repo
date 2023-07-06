using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListDataSourceItemHandler : CommandHandlerBase<DeleteHotListItem>
    {
        protected override async Task OnMessage(DeleteHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var hotlistNumberPlateRepo = context.Get<E.HotListNumberPlate>();

            var singleDeleteRequest = command.Id != RecId.Empty;

            if (command.Id == RecId.Empty)
            {
                var hotlistIdHavingNumberPlates = hotlistNumberPlateRepo.Many(x => command.IdsToDelete.Contains(x.HotListId)).Select(x=>x.HotListId).Distinct().ToList();

                if (hotlistIdHavingNumberPlates == null || hotlistIdHavingNumberPlates.Count == 0)
                {
                    await _repository.Delete(x => command.IdsToDelete.Contains(x.RecId));

                    var logMessage = "All HotList records have been deleted";

                    context.Success(logMessage);
                }
                else
                {
                    throw new DeleteNotAllowed(string.Join(",",hotlistIdHavingNumberPlates));
                }               
            }
            else
            {
                bool hotlistHasNumberPlate = await hotlistNumberPlateRepo.Exists(hotlistnumberPlate => hotlistnumberPlate.HotListId == command.Id);
                if (hotlistHasNumberPlate)
                {
                    throw new DeleteNotAllowed(command.Id.ToString());
                }
                else
                {
                    await _repository.Delete(x => x.RecId == command.Id);

                    var logMessage = $"HotList record has been deleted, RecId: {command.Id}";
                    context.Success(logMessage);
                }                
            }

        }
    }
}
