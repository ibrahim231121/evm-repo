using E = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.NumberPlates.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Delete
{
    public class DeleteNumberPlatesTempHandler : CommandHandlerBase<DeleteNumberPlatesTemp>
    {
        protected override async Task OnMessage(DeleteNumberPlatesTemp command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.NumberPlatesTemp>();
            var singleDeleteRequest = command.Id != SysSerial.Empty;

            var result = singleDeleteRequest switch
            {
                true => await _repository.Many(x => x.SysSerial == command.Id).ToListAsync(token),
                false => await _repository.Many().ToListAsync(token),
            };

            if (!result.Any())
                throw new RecordNotFound("License Plate Not Found");
            else
            {
                await _repository.Delete(result, token);

                var logMessage = singleDeleteRequest ? $"License Plate record has been deleted, SysSerial: {command.Id}"
                    : $"All License Plate records have been deleted";

                context.Success(logMessage);
            }

        }
    }
 }
