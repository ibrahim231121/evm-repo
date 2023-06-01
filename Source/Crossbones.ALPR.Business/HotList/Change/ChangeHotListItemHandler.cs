using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItemHandler : CommandHandlerBase<ChangeHotListItem>
    {
        protected override async Task OnMessage(ChangeHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.Name == command.Name && x.SysSerial != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Name Already Exist");
                }
                else
                {
                    var hotListItem = await _repository.One(x => x.SysSerial == command.Id);
                    hotListItem.Name = command.Name;
                    hotListItem.Description = command.Description;

                    await _repository.Update(hotListItem, token);
                    context.Success($"HotList item has been updated, SysSerial:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
