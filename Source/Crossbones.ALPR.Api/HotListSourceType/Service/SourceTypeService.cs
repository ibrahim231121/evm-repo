
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotList.Get;
using Corssbones.ALPR.Business.SourceType.Add;
using Corssbones.ALPR.Business.SourceType.Change;
using Corssbones.ALPR.Business.SourceType.Delete;
using Corssbones.ALPR.Business.SourceType.View;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Api.HotList.Service;
using Crossbones.ALPR.Api.HotListSourceType.Service;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.ValueObjects;
using Crossbones.Modules.Sequence.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.HotListSourceType.Service
{
    public class SourceTypeService : ServiceBase, ISourceTypeService
    {
        readonly ISequenceProxy _sourceTypeSequenceProxy;

        public SourceTypeService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _sourceTypeSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<SysSerial> Add(SourceType request)
        {
            var id = new SysSerial(await _sourceTypeSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddSourceType(id)
            {
                SourceTypeName = request.SourceTypeName,
                Description = request.Description,
            };
            _ = await Execute(cmd);
            return id;
        }

        public async Task Change(SysSerial SourceTypeSysSerial, SourceType request)
        {
            var cmd = new ChangeSourceType(SourceTypeSysSerial)
            {
                SourceTypeName = request.SourceTypeName,
                Description = request.Description,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial SourceTypeSysSerial)
        {
            var cmd = new DeleteSourceType(SourceTypeSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteSourceType(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<SourceTypeItem> Get(SysSerial SourceTypeSysSerial)
        {
            var query = new GetSourceType(SourceTypeSysSerial, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<SourceTypeItem>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<SourceTypeItem>> GetAll(Pager paging)
        {
            var dataQuery = new GetSourceType(SysSerial.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<SourceTypeItem>>(dataQuery);

            var countQuery = new GetSourceType(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}
