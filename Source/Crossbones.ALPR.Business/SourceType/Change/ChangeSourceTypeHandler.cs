using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.Change
{
    public class ChangeSourceTypeHandler : CommandHandlerBase<ChangeSourceType>
    {
        protected override async Task OnMessage(ChangeSourceType command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.SourceType>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.SourceTypeName == command.SourceTypeName && x.RecId != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("SourceTypeName Already Exist");
                }
                else
                {
                    var sourceTypeNameItem = await _repository.One(x => x.RecId == command.Id);
                    sourceTypeNameItem.SourceTypeName = command.SourceTypeName;
                    sourceTypeNameItem.Description = command.Description;

                    await _repository.Update(sourceTypeNameItem, token);
                    context.Success($"SourceType item has been updated, RecId:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("SourceType item Not Found");
            }
        }
    }
}
