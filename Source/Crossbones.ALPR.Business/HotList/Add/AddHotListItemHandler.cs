using Crossbones.Modules.Common.Exceptions;
using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItemHandler : CommandHandlerBase<AddHotListItem>
    {
        readonly IMapper mapper;

        public AddHotListItemHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task OnMessage(AddHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.Hotlist>();
            var nameExist = await _repository.Exists(x => x.Name == command.ItemToAdd.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                var res = mapper.Map<Entities.Hotlist>(command.ItemToAdd);
                await _repository.Add(res, token);
                context.Success($"HotList Item has been added, RecId:{command.Id}");
            }
        }
    }
}