using Crossbones.ALPR.Business.HotListDataSource.Change;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.Change
{
    public class ChangeSourceTypeHandler : CommandHandlerBase<ChangeSourceType>
    {
        protected override async Task OnMessage(ChangeSourceType command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.SourceType>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.SourceTypeName == command.SourceTypeName && x.SysSerial != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("SourceTypeName Already Exist");
                }
                else
                {
                    var sourceTypeNameItem = await _repository.One(x => x.SysSerial == command.Id);
                    sourceTypeNameItem.SourceTypeName = command.SourceTypeName;
                    sourceTypeNameItem.Description = command.Description;

                    await _repository.Update(sourceTypeNameItem, token);
                    context.Success($"SourceType item has been updated, SysSerial:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("SourceType item Not Found");
            }
        }
    }
}
