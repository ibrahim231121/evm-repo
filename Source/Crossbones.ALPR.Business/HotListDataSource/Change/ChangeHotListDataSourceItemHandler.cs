﻿using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;
using AutoMapper;

namespace Crossbones.ALPR.Business.HotListDataSource.Change
{
    public class ChangeHotListDataSourceItemHandler : CommandHandlerBase<ChangeHotListDataSourceItem>
    {
        readonly IMapper mapper;
        public ChangeHotListDataSourceItemHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task OnMessage(ChangeHotListDataSourceItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.HotlistDataSource>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.Name == command.Item.Name && x.RecId != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Name Already Exist");
                }
                else
                {
                    var res = mapper.Map<E.HotlistDataSource>(command.Item);
                    await _repository.Update(res, token);
                    context.Success($"HotListDataSource item has been updated, RecId:{res.RecId}");
                }
            }
            else
            {
                throw new RecordNotFound("HotListDataSource item Not Found");
            }
        }
    }
}
