﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Business.Repositories;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturedPlateHandler : CommandHandlerBase<DeleteCapturedPlateItem>
    {
        protected override async Task OnMessage(DeleteCapturedPlateItem command, ICommandContext context, CancellationToken token)
        {            
            var cpRepository = context.Get<E.CapturedPlate>();

            switch (command.DeletdCommandFilter)
            {
                case DeleteCommandFilter.Single:
                    bool cpExist = await cpRepository.Exists(cp => cp.SysSerial == command.Id, token);
                    if (cpExist)
                    {
                        await cpRepository.Delete(cp=>cp.SysSerial == command.Id, token);
                        context.Success($"CapturedPlate with Id:{command.Id} successfully deleted.");
                    }
                    else 
                    {
                        throw new RecordNotFound($"CapturedPlate with Id:{command.Id} does not exist.");
                    }
                    break;
                case DeleteCommandFilter.AllOfUser:
                    var ucpRepository = context.Get<E.UserCapturedPlate>();

                    var capturedPlates = await ucpRepository.Many(ucp=>ucp.UserId == command.UserId, token).
                                                                            Select(ucp=>new E.CapturedPlate() { SysSerial = ucp.CapturedId }).
                                                                            ToListAsync();

                    if (capturedPlates == null || capturedPlates.Count == 0)
                    {
                        throw new RecordNotFound($"No CapturedPlate found for user Id:{command.UserId}.");
                    }
                    else
                    {
                        await cpRepository.Delete(capturedPlates, token);
                        context.Success($"CapturedPlate for User Id:{command.UserId} successfully deleted.");
                    }

                    break;
                case DeleteCommandFilter.All:
                    await cpRepository.Delete(cp => true, token);
                    context.Success($"All CapturedPlate are successfully deleted.");
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

        }
    }
}
