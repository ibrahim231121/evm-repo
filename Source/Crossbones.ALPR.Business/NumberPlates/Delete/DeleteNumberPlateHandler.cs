using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.ALPR.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Crossbones.Modules.Common.Exceptions;
using Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.NumberPlates.Delete
{
    public class DeleteNumberPlateHandler : CommandHandlerBase<DeleteNumberPlate>
    {
        protected override async Task OnMessage(DeleteNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<NumberPlate>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("License Plate Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"License Plate record has been deleted, SysSerial: {command.Id}"
                    : $"All License Plate records have been deleted";

                context.Success(logMessage);
            }
        }
    }
}
