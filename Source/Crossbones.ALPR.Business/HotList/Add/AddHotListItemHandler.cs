using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.Modules.Common.Exceptions;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItemHandler : CommandHandlerBase<AddHotListItem>
    {
        protected override async Task OnMessage(AddHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var nameExist = await _repository.Exists(x => x.Name == command.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                await _repository.Add(new E.Hotlist()
                {
                    SysSerial = command.Id,
                    Name = command.Name,
                    Description = command.Description,
                    SourceId = command.SourceId,
                    RulesExpression = command.RulesExpression,
                    AlertPriority = command.AlertPriority,
                    CreatedOn = command.CreatedOn,
                    LastUpdatedOn = command.LastUpdatedOn,
                    //LastTimeStamp = command.LastTimeStamp,
                    StationId = command.StationId
                }, token);
                context.Success($"HotList Item has been added, SysSerial:{command.Id}");
            }
        }
    }
}
