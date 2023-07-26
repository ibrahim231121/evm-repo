using AutoMapper;
using Crossbones.ALPR.Common;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryStatusHandler : CommandHandlerBase<ChangeCapturePlatesSummaryStatusItem>
    {
        IMapper mapper;
        public ChangeCapturePlatesSummaryStatusHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task OnMessage(ChangeCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<Entities.CapturePlatesSummaryStatus>();
            bool entityExist = await cpssRepository.Exists(cpss => cpss.SyncId == command.Id, token);

            if (entityExist)
            {
                command.UpdatedItem.SyncId = command.Id;

                var capturePlatesSummary = mapper.Map<Entities.CapturePlatesSummaryStatus>(command.UpdatedItem); //DTOHelper.ConvertFromDTO(command.UpdatedItem);

                await cpssRepository.Update(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummaryStatus with Id:{command.Id} updated successfully.");
            }
            else
            {
                throw new RecordNotFound($"CapturePlatesSummaryStatus with Id:{command.Id} not found.");
            }
        }
    }
}