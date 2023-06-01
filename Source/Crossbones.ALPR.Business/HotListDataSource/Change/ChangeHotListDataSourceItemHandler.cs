using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers;
using Crossbones.Modules.Business.Handlers.Command;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotListDataSource.Change
{
    public class ChangeHotListDataSourceItemHandler : CommandHandlerBase<ChangeHotListDataSourceItem>
    {
        protected override async Task OnMessage(ChangeHotListDataSourceItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.HotlistDataSource>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.Name == command.Name && x.SysSerial != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Name Already Exist");
                }
                else
                {
                    var _item = await _repository.One(x => x.SysSerial == command.Id);

                    _item.Name = command.Name;
                    _item.SourceName = command.SourceName;
                    _item.AgencyId = command.AgencyID;
                    _item.SourceTypeId = command.SourceTypeID;
                    _item.SchedulePeriod = command.SchedulePeriod;
                    _item.LastUpdated = command.LastUpdated;
                    _item.IsExpire = command.IsExpire;
                    _item.SchemaDefinition = command.SchemaDefinition;
                    _item.LastUpdateExternalHotListId = command.LastUpdateExternalHotListID;
                    _item.ConnectionType = command.ConnectionType;
                    _item.Userid = command.Userid;
                    _item.LocationPath = command.locationPath;
                    _item.Password = command.Password;
                    _item.Port = command.port;
                    _item.LastRun = command.LastRun;
                    _item.Status = command.Status;
                    _item.SkipFirstLine = command.SkipFirstLine;
                    _item.StatusDesc = command.StatusDesc;
                    _item.SourceType = command.SourceType;

                    await _repository.Update(_item, token);
                    context.Success($"HotListDataSource item has been updated, SysSerial:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
