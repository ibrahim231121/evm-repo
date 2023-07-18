using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using Entities = Corssbones.ALPR.Database.Entities;

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
            var _repository = context.Get<Entities.Hotlist>();
            var entityExist = await _repository.Exists(x => x.RecId == command.Id, token);
            if (entityExist)
            {
                var nameExist = await _repository.Exists(x => x.Name == command.ItemToUpdate.Name && x.RecId != command.Id, token);
                if (nameExist)
                {
                    throw new DuplicationNotAllowed("Name Already Exist");
                }
                else
                {
                    //var hotList = _mapper.Map<Entities.Hotlist>(command.ItemToUpdate);

                    var hotListItem = await _repository.One(x => x.RecId == command.Id, token);
                    hotListItem.Name = command.ItemToUpdate.Name;
                    hotListItem.Description = command.ItemToUpdate.Description;
                    hotListItem.RulesExpression = command.ItemToUpdate.RulesExpression;
                    hotListItem.AlertPriority = Convert.ToInt16(command.ItemToUpdate.AlertPriority);
                    hotListItem.SourceId = command.ItemToUpdate.SourceId;
                    hotListItem.StationId = command.ItemToUpdate.StationId;
                    hotListItem.Color = command.ItemToUpdate.Color;
                    hotListItem.Urilocation = command.ItemToUpdate.Audio;

                    await _repository.Update(hotListItem, token);
                    context.Success($"HotList item has been updated, RecId:{command.Id}");
                }
            }
            else
            {
                throw new RecordNotFound("HotList item Not Found");
            }
        }
    }
}
