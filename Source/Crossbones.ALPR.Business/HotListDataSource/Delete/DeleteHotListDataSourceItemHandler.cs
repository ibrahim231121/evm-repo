using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotListDataSource.Delete
{
    public class DeleteHotListDataSourceItemHandler : CommandHandlerBase<DeleteHotListDataSourceItem>
    {
        protected override async Task OnMessage(DeleteHotListDataSourceItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.HotlistDataSource>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("HotListDataSource Record Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"HotListDataSource record has been deleted, SysSerial: {command.Id}" : $"All HotListDataSource records have been deleted";
                context.Success(logMessage);
            }
        }
    }
}
