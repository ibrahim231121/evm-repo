using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.ExportDetail.Add
{
    public class AddExportDetailHander : CommandHandlerBase<AddExportDetail>
    {
        IMapper mapper;
        public AddExportDetailHander(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task OnMessage(AddExportDetail command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.AlprExportDetail>();
            var isEntryExists = await _repository.Exists((x => x.TicketNumber == command.ItemToAdd.TicketNumber), token);
            if (isEntryExists)
            {
                throw new DuplicationNotAllowed("Entry exists againts this record");
            }
            else
            {
                var exportDetail = mapper.Map<Entities.AlprExportDetail>(command.ItemToAdd);
                await _repository.Add(exportDetail, token);
                context.Success($"Export Detail has been added, RecId:{command.Id}");
            }
        }
    }
}