﻿using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace Corssbones.ALPR.Business.HotListNumberPlates.Delete
{
    public class DeleteHotListNumberPlateHandler : CommandHandlerBase<DeleteHotListNumberPlate>
    {
        protected override async Task OnMessage(DeleteHotListNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<HotListNumberPlate>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
            {
                throw new RecordNotFound("HotList Data Not Found");
            }                
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"HotList Number Plate record has been deleted, SysSerial: {command.Id}" : $"All HotList Number Plate records have been deleted";
                context.Success(logMessage);
            }
        }
    }
}
