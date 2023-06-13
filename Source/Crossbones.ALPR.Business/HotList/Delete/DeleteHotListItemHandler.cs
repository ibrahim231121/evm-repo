﻿using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListDataSourceItemHandler : CommandHandlerBase<DeleteHotListItem>
    {
        protected override async Task OnMessage(DeleteHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            if (command.Id == SysSerial.Empty)
            {
                await _repository.Delete(x => true);

                var logMessage = "All HotList records have been deleted";
                context.Success(logMessage);
            }
            else
            {
                await _repository.Delete(x => x.SysSerial == command.Id);

                var logMessage = $"HotList record has been deleted, SysSerial: {command.Id}";
                context.Success(logMessage);
            }

        }
    }
}
