using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers;
using Crossbones.Modules.Business.Handlers.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;


namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItemHandler : CommandHandlerBase<AddHotListDataSourceItem>
    {
        protected override async Task OnMessage(AddHotListDataSourceItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.HotlistDataSource>();
            var nameExist = await _repository.Exists(x => x.Name == command.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                await _repository.Add(new E.HotlistDataSource()
                {
                    SysSerial = command.Id,
                    Name = command.Name,
                    SourceName = command.SourceName,
                    AgencyId = command.AgencyID,
                    SourceTypeId = command.SourceTypeID,
                    SchedulePeriod = command.SchedulePeriod,
                    LastUpdated = command.LastUpdated,
                    IsExpire = command.IsExpire,
                    SchemaDefinition = command.SchemaDefinition,
                    LastUpdateExternalHotListId = command.LastUpdateExternalHotListID,
                    ConnectionType = command.ConnectionType,
                    Userid = command.Userid,
                    LocationPath = command.locationPath,
                    Password = command.Password,
                    Port = command.port,
                    LastRun = command.LastRun,
                    Status = command.Status,
                    SkipFirstLine = command.SkipFirstLine,
                    StatusDesc = command.StatusDesc,
                    SourceType = command.SourceType,
                }, token);
                context.Success($"HotListDataSource Item has been added, SysSerial:{command.Id}");
            }
        }
    }
}
