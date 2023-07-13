using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Microsoft.EntityFrameworkCore;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.State.Get
{
    public class GetStateHandler : QueryHandlerBase<GetState>
    {
        IMapper mapper;

        public GetStateHandler(IMapper _mapper)
        {
            mapper = _mapper;
        }
        protected override async Task<object> OnQuery(GetState query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<Corssbones.ALPR.Database.Entities.State>();
            var res = await _repository.Many().ToListAsync();
            var data = mapper.Map<List<DTO.StateDTO>>(res);
            return data;
        }
    }
}