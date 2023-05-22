using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.ExportDetail.Delete
{
    public class DeleteExportDetailHander : CommandHandlerBase<DeleteExportDetail>
    {
        protected override async Task OnMessage(DeleteExportDetail command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.ALPRExportDetail>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("Export Detail Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"Export Detail record has been deleted, SysSerial: {command.Id}" : $"All Export Detail records have been deleted";
                context.Success(logMessage);
            }
        }
    }
}