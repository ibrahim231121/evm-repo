using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.Validation;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryItemHandler : CommandHandlerBase<AddCapturePlatesSummaryItem>
    {
        protected override async Task OnMessage(AddCapturePlatesSummaryItem command, ICommandContext context, CancellationToken token)
        {
            var cpsRepository = context.Get<Entities.CapturePlatesSummary>();

            bool itemExist = await cpsRepository.Exists(cps => cps.UserId == command.ItemToAdd.UserId && cps.CapturePlateId == command.ItemToAdd.CapturePlateId, token);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturePlatesSummary with UserId:{command.ItemToAdd.UserId} and CapturedPlateId: {command.ItemToAdd.CapturePlateId} already exist.");
            }
            else
            {
                CapturedPlateValidations.ValidateCapturePlateSummaryItem(command.ItemToAdd);
                var capturePlatesSummary = DTOHelper.ConvertFromDTO(command.ItemToAdd);

                await cpsRepository.Add(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummary with UserId:{command.ItemToAdd.UserId} and CapturedPlateId: {command.ItemToAdd.CapturePlateId} successfully added.");
            }
        }
    }
}
