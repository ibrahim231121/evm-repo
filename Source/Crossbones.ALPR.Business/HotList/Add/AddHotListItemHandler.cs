using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItemHandler : CommandHandlerBase<AddHotListItem>
    {
        protected override async Task OnMessage(AddHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var nameExist = await _repository.Exists(x => x.Name == command.ItemToAdd.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                await _repository.Add(new E.Hotlist()
                {
                    SysSerial = command.Id,
                    Name = command.ItemToAdd.Name,
                    Description = command.ItemToAdd.Description,
                    SourceId = command.ItemToAdd.SourceId,
                    RulesExpression = command.ItemToAdd.RulesExpression,
                    AlertPriority = command.ItemToAdd.AlertPriority,
                    CreatedOn = command.ItemToAdd.CreatedOn,
                    LastUpdatedOn = command.ItemToAdd.LastUpdatedOn,
                    //LastTimeStamp = command.LastTimeStamp,
                    StationId = command.ItemToAdd.StationId,
                    Color = command.ItemToAdd.Color,
                    Urilocation = command.ItemToAdd.Audio
                }, token);
                context.Success($"HotList Item has been added, SysSerial:{command.Id}");
            }
        }
    }
}
