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
    public class ChangeCapturePlatesSummaryStatusHandler : CommandHandlerBase<ChangeCapturePlatesSummaryStatusItem>
    {
        protected override async Task OnMessage(ChangeCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<E.CapturePlatesSummaryStatus>();
            bool entityExist = await cpssRepository.Exists(cpss => cpss.SyncId == command.Id, token);

            if (entityExist)
            {
                command.UpdatedItem.SyncId = command.Id;

                CapturedPlateValidations.ValidateCapturePlateSummaryStatusItem(command.UpdatedItem);

                var capturePlatesSummary = DTOHelper.ConvertFromDTO(command.UpdatedItem);

                await cpssRepository.Update(capturePlatesSummary, token);

                context.Success($"CapturePlatesSummaryStatus with Id:{command.Id} updated successfully.");
            }
            else
            {
                throw new RecordNotFound($"CapturePlatesSummaryStatus with Id:{command.Id} not found.");
            }
        }
    }
}
