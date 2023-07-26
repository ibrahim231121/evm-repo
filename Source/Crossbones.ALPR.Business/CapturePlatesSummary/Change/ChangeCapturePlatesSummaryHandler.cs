using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryHandler : CommandHandlerBase<ChangeCapturePlatesSummaryItem>
    {
        IMapper mapper;
        public ChangeCapturePlatesSummaryHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task OnMessage(ChangeCapturePlatesSummaryItem command, ICommandContext context, CancellationToken token)
        {
            var cpsRepository = context.Get<Entities.CapturePlatesSummary>();
            bool entityExist = await cpsRepository.Exists(x => x.UserId == command.UpdatedItem.UserId && x.CapturePlateId == command.UpdatedItem.CapturePlateId, token);

            if (entityExist)
            {
                //CapturedPlateValidations.ValidateCapturePlateSummaryItem(command.UpdatedItem);

                var capturePlatesSummary = mapper.Map<Entities.CapturePlatesSummary>(command.UpdatedItem); //DTOHelper.ConvertFromDTO(command.UpdatedItem);

                await cpsRepository.Update(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummary with UserId:{command.UpdatedItem.UserId} and CapturedPlateId: {command.UpdatedItem.CapturePlateId} updated successfully.");
            }
            else
            {
                throw new RecordNotFound($"CapturePlatesSummary with UserId:{command.UpdatedItem.UserId} and CapturedPlateId: {command.UpdatedItem.CapturePlateId} not found.");
            }
        }
    }
}