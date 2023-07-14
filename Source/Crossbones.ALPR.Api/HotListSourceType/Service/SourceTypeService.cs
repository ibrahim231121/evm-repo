
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.SourceType.Add;
using Corssbones.ALPR.Business.SourceType.Change;
using Corssbones.ALPR.Business.SourceType.Delete;
using Corssbones.ALPR.Business.SourceType.View;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.HotListSourceType.Service
{
    public class SourceTypeService : ServiceBase, ISourceTypeService
    {
        readonly ISequenceProxy _sourceTypeSequenceProxy;

        public SourceTypeService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _sourceTypeSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<RecId> Add(SourceType request)
        {
            var id = new RecId(await _sourceTypeSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddSourceType(id)
            {
                SourceTypeName = request.SourceTypeName,
                Description = request.Description,
            };
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(RecId recId, SourceType request)
        {
            var cmd = new ChangeSourceType(recId)
            {
                SourceTypeName = request.SourceTypeName,
                Description = request.Description,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId recId)
        {
            var cmd = new DeleteSourceType(recId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteSourceType(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<DTO.SourceTypeDTO> Get(RecId recId)
        {
            var query = new GetSourceType(recId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<DTO.SourceTypeDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<DTO.SourceTypeDTO>> GetAll(Pager paging)
        {
            var dataQuery = new GetSourceType(RecId.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<DTO.SourceTypeDTO>>(dataQuery);

            var countQuery = new GetSourceType(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}
