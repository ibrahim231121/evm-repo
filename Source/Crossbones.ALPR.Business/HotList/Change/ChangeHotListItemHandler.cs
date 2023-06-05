﻿using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using E = Corssbones.ALPR.Database.Entities;
using AutoMapper;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItemHandler : CommandHandlerBase<ChangeHotListItem>
    {
        readonly IMapper _mapper;

        public ChangeHotListItemHandler(IMapper mapper)
        {
            _mapper = mapper;

        }

        protected override async Task OnMessage(ChangeHotListItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();
            var entityExist = await _repository.Exists(x => x.SysSerial == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.Name == command.ItemToUpdate.Name && x.SysSerial != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Name Already Exist");
                }
                else
                {
                    //var hotList = _mapper.Map<E.Hotlist>(command.ItemToUpdate);

                    var hotListItem = await _repository.One(x => x.SysSerial == command.Id, token);
                    hotListItem.Name = command.ItemToUpdate.Name;
                    hotListItem.Description = command.ItemToUpdate.Description;
                    hotListItem.RulesExpression = command.ItemToUpdate.RulesExpression;
                    hotListItem.AlertPriority = command.ItemToUpdate.AlertPriority;
                    hotListItem.SourceId = command.ItemToUpdate.SourceId;
                    hotListItem.StationId = command.ItemToUpdate.StationId;
                    hotListItem.Color = command.ItemToUpdate.Color;
                    hotListItem.Urilocation = command.ItemToUpdate.Audio;

                    await _repository.Update(hotListItem, token);
                    context.Success($"HotList item has been updated, SysSerial:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
