using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteUserCapturedPlateHandler : CommandHandlerBase<DeleteUserCapturedPlateItem>
    {
        protected override async Task OnMessage(DeleteUserCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {
            var ucpRepository = context.Get<E.UserCapturedPlate>();

            switch (command.DeletdCommandFilter)
            {
                case DeleteCommandFilter.Single:
                    if (command.Id > 0)
                    {
                        bool cpExist = await ucpRepository.Exists(cp => cp.SysSerial == command.Id, token);
                        if (cpExist)
                        {
                            await ucpRepository.Delete(cp => cp.SysSerial == command.Id, token);
                            context.Success($"UserCapturedPlate with Id:{command.Id} successfully deleted.");
                        }
                        else
                        {
                            throw new RecordNotFound($"UserCapturedPlate with Id:{command.Id} does not exist.");
                        }
                    }
                    else if (command.CapturedPlateId > 0)
                    {
                        bool itemExist = await ucpRepository.Exists(cp => cp.CapturedId == command.CapturedPlateId, token);
                        if (itemExist)
                        {
                            await ucpRepository.Delete(cp => cp.CapturedId == command.CapturedPlateId, token);
                            context.Success($"UserCapturedPlate with CapturedPlateId:{command.CapturedPlateId} successfully deleted.");
                        }
                        else
                        {
                            throw new RecordNotFound($"UserCapturedPlate with CapturedPlateId:{command.CapturedPlateId} does not exist.");
                        }
                    }

                    break;
                case DeleteCommandFilter.AllOfUser:
                    bool userItemExist = await ucpRepository.Exists(ucp => ucp.UserId == command.UserId, token);

                    if (userItemExist)
                    {
                        await ucpRepository.Delete(ucp => ucp.UserId == command.UserId, token);

                        context.Success($"UserCapturedPlate for User Id:{command.UserId} successfully deleted.");
                    }
                    else
                    {
                        throw new RecordNotFound($"UserCapturedPlate with UserId:{command.UserId} does not exist.");
                    }
                    break;
                case DeleteCommandFilter.All:
                    await ucpRepository.Delete(ucp => true, token);

                    context.Success($"All UserCapturedPlate are successfully deleted.");
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
