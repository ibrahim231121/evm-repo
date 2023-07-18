using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryStatusHandler : CommandHandlerBase<DeleteCapturePlatesSummaryStatusItem>
    {
        protected override async Task OnMessage(DeleteCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<Entities.CapturePlatesSummaryStatus>();

            switch (command.DeleteCommandFilter)
            {
                case DeleteCommandFilter.Single:
                    bool cpExist = await cpssRepository.Exists(cpss => cpss.SyncId == command.Id, token);
                    if (cpExist)
                    {
                        await cpssRepository.Delete(cpss => cpss.SyncId == command.Id, token);
                        context.Success($"CapturePlatesSummaryStatus with Id:{command.Id} successfully deleted.");
                    }
                    else
                    {
                        throw new RecordNotFound($"CapturePlatesSummary with Id:{command.Id} does not exist.");
                    }
                    break;
                case DeleteCommandFilter.All:
                    await cpssRepository.Delete(cpss => true, token);
                    context.Success($"All CapturePlatesSummaryStatus are successfully deleted.");
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
