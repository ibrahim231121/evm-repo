using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.Add
{
    public class AdSourceTypeHandler : CommandHandlerBase<AddSourceType>
    {
        protected override async Task OnMessage(AddSourceType command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.SourceType>();
            var nameExist = await _repository.Exists(x => x.SourceTypeName == command.SourceTypeName, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("SourceTypeName already exist");
            }
            else
            {
                await _repository.Add(new E.SourceType()
                {
                    SourceTypeName = command.SourceTypeName,
                    Description = command.Description,

                }, token);
                context.Success($"SourceType Item has been added, RecId:{command.Id}");
            }
        }
    }
}
