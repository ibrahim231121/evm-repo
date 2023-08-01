using AutoMapper;
using Crossbones.ALPR.Common;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryStatusItemHandler : CommandHandlerBase<AddCapturePlatesSummaryStatusItem>
    {
        IMapper mapper;
        public AddCapturePlatesSummaryStatusItemHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }

        protected override async Task OnMessage(AddCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<Entities.CapturePlatesSummaryStatus>();

            bool itemExist = await cpssRepository.Exists(cpss => cpss.SyncId == command.Id, token);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturePlatesSummaryStatus with Id:{command.Id} already exist.");
            }
            else
            {
                command.ItemToAdd.SyncId = command.Id;
                var capturePlatesSummaryStatus = mapper.Map<Entities.CapturePlatesSummaryStatus>(command.ItemToAdd);

                await cpssRepository.Add(capturePlatesSummaryStatus, token);

                context.Success($"CapturePlatesSummaryStatus with Id: {command.Id} successfully added.");
            }
        }
    }
}