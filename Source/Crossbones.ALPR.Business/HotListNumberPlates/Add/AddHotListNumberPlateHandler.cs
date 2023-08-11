﻿using AutoMapper;
using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Add
{
    public class AddHotListNumberPlateHandler : CommandHandlerBase<AddHotListNumberPlate>
    {
        readonly IMapper mapper;
        public AddHotListNumberPlateHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task OnMessage(AddHotListNumberPlate command, ICommandContext context, CancellationToken token)
        {
            var hotListNumberPlateRepo = context.Get<Entities.HotListNumberPlate>();

            var nameExist = await hotListNumberPlateRepo.Exists(x => x.HotListId == command.Item.HotListId && x.NumberPlateId == command.Item.NumberPlateId, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Entry with same attributes already exist");
            }
            else
            {

                await hotListNumberPlateRepo.Add(mapper.Map<Entities.HotListNumberPlate>(command.Item), token);
                context.Success($"HotList Number Plate has been added, RecId:{command.Id}");
            }
        }
    }
}