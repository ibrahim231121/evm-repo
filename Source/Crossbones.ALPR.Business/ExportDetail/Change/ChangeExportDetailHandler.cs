﻿using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.ExportDetail.Change
{
    public class ChangeExportDetailHandler : CommandHandlerBase<ChangeExportDetail>
    {
        protected override async Task OnMessage(ChangeExportDetail command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.ALPRExportDetail>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.TicketNumber == command.ItemToUpdate.TicketNumber && x.RecId != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Export Detail Already Exist");
                }
                else
                {
                    var exportDetail = await _repository.One(x => x.RecId == command.Id);
                    exportDetail.TicketNumber = command.ItemToUpdate.TicketNumber;
                    exportDetail.CapturedPlateId = command.ItemToUpdate.CapturedPlateId;
                    exportDetail.ExportedOn = command.ItemToUpdate.ExportedOn;
                    exportDetail.ExportPath = command.ItemToUpdate.ExportPath;
                    exportDetail.UriLocation = command.ItemToUpdate.UriLocation;

                    await _repository.Update(exportDetail, token);
                    context.Success($"Export Detail has been updated, RecId:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("Export Detail Not Found");
            }
        }
    }
}