
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.SourceType.Add;
using Corssbones.ALPR.Business.SourceType.Change;
using Corssbones.ALPR.Business.SourceType.Delete;
using Corssbones.ALPR.Business.SourceType.View;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
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

        public async Task Change(RecId SourceTypeRecId, SourceType request)
        {
            var cmd = new ChangeSourceType(SourceTypeRecId)
            {
                SourceTypeName = request.SourceTypeName,
                Description = request.Description,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId SourceTypeRecId)
        {
            var cmd = new DeleteSourceType(SourceTypeRecId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteSourceType(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<SourceTypeDTO> Get(RecId SourceTypeRecId)
        {
            var query = new GetSourceType(SourceTypeRecId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<SourceTypeDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<SourceTypeDTO>> GetAll(Pager paging)
        {
            var dataQuery = new GetSourceType(RecId.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<SourceTypeDTO>>(dataQuery);

            var countQuery = new GetSourceType(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}
