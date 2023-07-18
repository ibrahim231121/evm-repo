﻿using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.SourceType.Delete
{
    public class DeleteSourceTypeHandler : CommandHandlerBase<DeleteSourceType>
    {
        protected override async Task OnMessage(DeleteSourceType command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<Entities.SourceType>();
            var singleDeleteRequest = command.Id != RecId.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.RecId == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("SourceType Data Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"SourceType record has been deleted, RecId: {command.Id}" : $"All SourceType records have been deleted";
                context.Success(logMessage);
            }
        }
    }
}
