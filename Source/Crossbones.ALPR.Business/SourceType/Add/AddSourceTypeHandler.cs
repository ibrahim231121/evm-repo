using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.Add
{
    public class AddSourceTypeHandler : CommandHandlerBase<AddSourceType>
    {
        IMapper mapper;
        public AddSourceTypeHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task OnMessage(AddSourceType command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.SourceType>();
            var nameExist = await _repository.Exists(x => x.SourceTypeName == command.SourceTypeName, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("SourceTypeName already exist");
            }
            else
            {
                await _repository.Add(mapper.Map<Entities.SourceType>(command), token);
                context.Success($"SourceType Item has been added, RecId:{command.Id}");
            }
        }
    }
}