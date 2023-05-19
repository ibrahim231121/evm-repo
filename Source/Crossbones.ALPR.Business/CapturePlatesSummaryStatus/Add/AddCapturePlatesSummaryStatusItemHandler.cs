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
    public class AddCapturePlatesSummaryStatusItemHandler : CommandHandlerBase<AddCapturePlatesSummaryStatusItem>
    {
        protected override async Task OnMessage(AddCapturePlatesSummaryStatusItem command, ICommandContext context, CancellationToken token)
        {
            var cpssRepository = context.Get<E.CapturePlatesSummaryStatus>();

            bool itemExist = await cpssRepository.Exists(cpss=>cpss.SyncId == command.Id, token);

            if (itemExist)
            {
                throw new DuplicationNotAllowed($"CapturePlatesSummaryStatus with Id:{command.Id} already exist.");
            }
            else
            {
                command.ItemToAdd.SyncId = command.Id;

                CapturedPlateValidations.ValidateCapturePlateSummaryStatusItem(command.ItemToAdd);

                var capturePlatesSummaryStatus = DTOHelper.ConvertFromDTO(command.ItemToAdd);

                await cpssRepository.Add(capturePlatesSummaryStatus, token);

                context.Success($"CapturePlatesSummaryStatus with Id: {command.Id} successfully added.");
            }            
        }
    }
}
