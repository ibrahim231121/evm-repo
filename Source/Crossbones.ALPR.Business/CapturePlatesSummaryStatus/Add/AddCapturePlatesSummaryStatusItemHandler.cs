using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.Validation;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryStatusItemHandler : CommandHandlerBase<AddCapturePlatesSummaryStatusItem>
    {
        protected override async Task OnMessage(AddCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<Entities.CapturePlatesSummaryStatus>();

            bool itemExist = await cpssRepository.Exists(cpss => cpss.SyncId == command.Id, token);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturePlatesSummaryStatus with Id:{command.Id} already exist.");
            }
            else
            {
                command.ItemToAdd.SyncId = command.Id;

                CapturedPlateValidations.ValidateCapturePlateSummaryStatusItem(command.ItemToAdd);

                var capturePlatesSummaryStatus = DTOHelper.ConvertFromDTO(command.ItemToAdd);

                await cpssRepository.Add(capturePlatesSummaryStatus, token);

                context.Success($"CapturePlatesSummaryStatus with Id: {command.Id} successfully added.");
            }
        }
    }
}
