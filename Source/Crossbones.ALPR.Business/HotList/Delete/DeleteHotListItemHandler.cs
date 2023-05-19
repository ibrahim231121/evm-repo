using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.ALPR.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.HotList.Delete;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListItemHandler : CommandHandlerBase<DeleteHotListItem>
    {
        protected override async Task OnMessage(DeleteHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("HotList Data Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"HotList record has been deleted, SysSerial: {command.Id}" : $"All HotList records have been deleted";
                context.Success(logMessage);
            }
        }
    }
}
