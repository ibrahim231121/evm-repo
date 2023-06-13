using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddUserCapturedPlateItemHandler : CommandHandlerBase<AddUserCapturedPlateItem>
    {
        protected override async Task OnMessage(AddUserCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var ucpRepository = context.Get<E.UserCapturedPlate>();

            if (command.UserId < 0)
            {
                throw new InvalidValue("UserCapturedPlate userId can not less than 0");
            }

            if (command.CapturedId < 0)
            {
                throw new InvalidValue("UserCapturedPlate capturedId can not less than 0");
            }

            var userCapturedPlate = new E.UserCapturedPlate()
            {
                SysSerial = command.Id,
                UserId = command.UserId,
                CapturedId = command.CapturedId
            };

            await ucpRepository.Add(userCapturedPlate, token);

            context.Success($"UserCapturedPlate Item has been added, SysSerial:{command.Id}");
        }
    }
}
