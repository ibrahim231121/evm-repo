﻿using Crossbones.Modules.Common.Exceptions;

using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;

using E = Corssbones.ALPR.Database.Entities;
using AutoMapper;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItemHandler : CommandHandlerBase<AddHotListItem>
    {
        readonly IMapper mapper;

        public AddHotListItemHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task OnMessage(AddHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var nameExist = await _repository.Exists(x => x.Name == command.ItemToAdd.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                var res = mapper.Map<E.Hotlist>(command);
                await _repository.Add(res, token);
                context.Success($"HotList Item has been added, SysSerial:{command.Id}");
            }
        }
    }
}
