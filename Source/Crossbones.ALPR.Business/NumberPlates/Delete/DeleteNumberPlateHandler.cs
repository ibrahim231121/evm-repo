using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Crossbones.ALPR.Business.NumberPlates.Delete
{
    public class DeleteNumberPlateHandler : CommandHandlerBase<DeleteNumberPlate>
    {
        protected override async Task OnMessage(DeleteNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.NumberPlate>();
            var hotListNumberPlateList = await context.Get<Entities.HotListNumberPlate>().Many().ToListAsync();
            var singleDeleteRequest = command.Id != RecId.Empty;

            if (hotListNumberPlateList.Any(x=>x.NumberPlatesId == command.Id))
            {
                throw new DeleteNotAllowed("Can not delete number plate since it is associated with hotlist.");
            }
            else
            {
                var result = singleDeleteRequest switch
                {
                    true => await _repository.Many(x => x.RecId == command.Id).ToListAsync(token),
                    false => await _repository.Many().ToListAsync(token),
                };

                if (!result.Any())
                    throw new RecordNotFound("License Plate Not Found");
                else
                {
                    await _repository.Delete(result, token);

                    var logMessage = singleDeleteRequest ? $"License Plate record has been deleted, RecId: {command.Id}"
                        : $"All License Plate records have been deleted";

                    context.Success(logMessage);
                }
            }            
        }
    }
}
