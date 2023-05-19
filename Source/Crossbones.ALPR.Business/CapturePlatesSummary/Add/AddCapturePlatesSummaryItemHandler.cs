using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.Validation;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Business.Repositories;
using Crossbones.Modules.Common.Exceptions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryItemHandler : CommandHandlerBase<AddCapturePlatesSummaryItem>
    {
        protected override async Task OnMessage(AddCapturePlatesSummaryItem command, ICommandContext context, CancellationToken token)
        {
            var cpsRepository = context.Get<E.CapturePlatesSummary>();

            bool itemExist = await cpsRepository.Exists(cps=>cps.UserId == command.ItemToAdd.UserId && cps.CapturePlateId == command.ItemToAdd.CapturePlateId, token);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturePlatesSummary with UserId:{command.ItemToAdd.UserId} and CapturedPlateId: {command.ItemToAdd.CapturePlateId} already exist.");
            }
            else
            {
                CapturedPlateValidations.ValidateCapturePlateSummaryItem(command.ItemToAdd);
                var capturePlatesSummary = DTOHelper.ConvertFromDTO(command.ItemToAdd);

                await cpsRepository.Add(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummary with UserId:{command.ItemToAdd.UserId} and CapturedPlateId: {command.ItemToAdd.CapturePlateId} successfully added.");
            }            
        }
    }
}
