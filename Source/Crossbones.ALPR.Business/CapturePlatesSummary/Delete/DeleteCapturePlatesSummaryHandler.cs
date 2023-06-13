using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryHandler : CommandHandlerBase<DeleteCapturePlatesSummaryItem>
    {
        protected override async Task OnMessage(DeleteCapturePlatesSummaryItem command, ICommandContext context, CancellationToken token)
        {
            var cpsRepository = context.Get<E.CapturePlatesSummary>();

            switch (command.DeletdCommandFilter)
            {
                case DeleteCommandFilter.Single:
                    if (command.UserId > 0 && command.CapturedPlateId > 0)
                    {
                        bool cpExist = await cpsRepository.Exists(cps => cps.UserId == command.UserId && cps.CapturePlateId == command.CapturedPlateId, token);
                        if (cpExist)
                        {
                            await cpsRepository.Delete(cps => cps.UserId == command.UserId && cps.CapturePlateId == command.CapturedPlateId, token);
                            context.Success($"CapturePlatesSummary with UserId:{command.UserId} and CapturedPlateId: {command.CapturedPlateId} successfully deleted.");
                        }
                        else
                        {
                            throw new RecordNotFound($"CapturePlatesSummary with UserId:{command.UserId} and CapturedPlateId: {command.CapturedPlateId} does not exist.");
                        }
                    }
                    else if (command.CapturedPlateId > 0)
                    {
                        bool cpExist = await cpsRepository.Exists(cps => cps.CapturePlateId == command.CapturedPlateId, token);
                        if (cpExist)
                        {
                            await cpsRepository.Delete(cps => cps.CapturePlateId == command.CapturedPlateId, token);
                            context.Success($"CapturePlatesSummary with CapturedPlateId: {command.CapturedPlateId} successfully deleted.");
                        }
                        else
                        {
                            throw new RecordNotFound($"CapturePlatesSummary with CapturedPlateId: {command.CapturedPlateId} does not exist.");
                        }
                    }


                    break;
                case DeleteCommandFilter.AllOfUser:
                    bool userItemsExist = await cpsRepository.Exists(cps => cps.UserId == command.UserId, token);

                    if (userItemsExist)
                    {
                        await cpsRepository.Delete(cps => cps.UserId == command.UserId, token);

                        context.Success($"CapturePlatesSummary for User Id:{command.UserId} successfully deleted.");
                    }
                    else
                    {
                        throw new RecordNotFound($"CapturePlatesSummary for User Id:{command.UserId} not found.");
                    }

                    break;
                case DeleteCommandFilter.All:
                    await cpsRepository.Delete(cps => true, token);

                    context.Success($"All CapturePlatesSummary are successfully deleted.");
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
