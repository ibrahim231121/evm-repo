using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Command;
using Crossbones.Modules.Common.Exceptions;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItemHandler : CommandHandlerBase<AddHotListDataSourceItem>
    {
        readonly IMapper mapper;

        public AddHotListDataSourceItemHandler(IMapper _mapper) => mapper = _mapper;
        protected override async Task OnMessage(AddHotListDataSourceItem command, ICommandContext context, CancellationToken token)
        {
            var _repository = context.Get<E.HotlistDataSource>();
            var nameExist = await _repository.Exists(x => x.Name == command.Item.Name, token);
            if (nameExist)
            {
                throw new DuplicationNotAllowed("Name already exist");
            }
            else
            {
                var res = mapper.Map<E.HotlistDataSource>(command.Item);

                await _repository.Add(res, token);
                context.Success($"HotListDataSource Item has been added, SysSerial:{command.Id}");
            }
        }
    }
}
