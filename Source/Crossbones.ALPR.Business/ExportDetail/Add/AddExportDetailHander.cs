using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.ExportDetail.Add
{
    public class AddExportDetailHander : CommandHandlerBase<AddExportDetail>
    {
        protected override async Task OnMessage(AddExportDetail command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.ALPRExportDetail>();
            var isEntryExists = await _repository.Exists((x => x.TicketNumber == command.TicketNumber), token);
            if (isEntryExists)
            {
                throw new DuplicationNotAllowed("Entry exists againts this record");
            }
            else
            {
                await _repository.Add(new E.ALPRExportDetail()
                {
                    RecId = command.Id,
                    TicketNumber = command.TicketNumber,
                    CapturedPlateId = command.CapturedPlateId,
                    ExportedOn = command.ExportedOn,
                    ExportPath = command.ExportPath,
                    UriLocation = command.UriLocation
                }, token);
                context.Success($"Export Detail has been added, RecId:{command.Id}");
            }
        }
    }
}