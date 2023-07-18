using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeUserCapturedPlateHandler : CommandHandlerBase<ChangeUserCapturedPlateItem>
    {
        protected override async Task OnMessage(ChangeUserCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var ucpRepository = context.Get<Entities.UserCapturedPlate>();
            bool entityExist = await ucpRepository.Exists(x => x.RecId == command.Id, token);

            if (entityExist)
            {
                if (command.UserId < 0)
                {
                    throw new InvalidValue("UserCapturedPlate userId can not less than 0");
                }

                if (command.CapturedId < 0)
                {
                    throw new InvalidValue("UserCapturedPlate capturedId can not less than 0");
                }

                var userCapturedPlate = new Entities.UserCapturedPlate()
                {
                    RecId = command.Id,
                    UserId = command.UserId,
                    CapturedId = command.CapturedId
                };

                await ucpRepository.Update(userCapturedPlate, token);

                context.Success($"CapturedPlate item has been updated, RecId:{command.Id}");
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
