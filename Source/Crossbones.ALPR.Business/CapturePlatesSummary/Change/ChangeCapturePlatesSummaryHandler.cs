using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.Validation;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Business.Repositories;
using Crossbones.Modules.Common.Exceptions;
using LanguageExt;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryHandler : CommandHandlerBase<ChangeCapturePlatesSummaryItem>
    {
        protected override async Task OnMessage(ChangeCapturePlatesSummaryItem command, ICommandContext context, CancellationToken token)
        {
            var cpsRepository = context.Get<E.CapturePlatesSummary>();
            bool entityExist = await cpsRepository.Exists(x => x.UserId == command.UpdatedItem.UserId && x.CapturePlateId == command.UpdatedItem.CapturePlateId, token);

            if (entityExist)
            {
                CapturedPlateValidations.ValidateCapturePlateSummaryItem(command.UpdatedItem);

                var capturePlatesSummary = DTOHelper.ConvertFromDTO(command.UpdatedItem);

                await cpsRepository.Update(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummary with UserId:{command.UpdatedItem.UserId} and CapturedPlateId: {command.UpdatedItem.CapturePlateId} updated successfully.");
            }
            else
            {
                throw new RecordNotFound($"CapturePlatesSummary with UserId:{command.UpdatedItem.UserId} and CapturedPlateId: {command.UpdatedItem.CapturePlateId} not found.");
            }
        }
    }
}
