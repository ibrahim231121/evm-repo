using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Models.Items;
using AutoMapper;

namespace Corssbones.ALPR.Business.HotList.Get
{
    public class GetHotListItemHandler : QueryHandlerBase<GetHotListItem>
    {
        readonly IMapper mapper;

        public GetHotListItemHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task<object> OnQuery(GetHotListItem query, IQueryContext context, CancellationToken token)
        {
            var _repository = context.Get<E.Hotlist>();

            if (query.QueryFilter == GetQueryFilter.Count)
            {
                return new RowCount(await _repository.Count());
            }
            else
            {
                switch (query.QueryFilter)
                {
                    true => _repository.Many(x => x.SysSerial == query.Id),
                    false => _repository.Many(),
                }).ApplyPaging(query.Paging).ToListAsync(token);

                if (!data.Any() && singleRequest)
                {
                    throw new RecordNotFound($"Unable to process your request because HotList Item Data is not found against provided Id '{query.Id}'");
                }

                var res = mapper.Map<List<HotListItem>>(data);

                return null;
            }
        }
    }
}
