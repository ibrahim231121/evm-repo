using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;
using Crossbones.ALPR.Models.Items;
using Crossbones.ALPR.Business.HotListDataSource.Add;
using Crossbones.ALPR.Business.HotListDataSource.Change;
using Corssbones.ALPR.Business.HotListDataSource.View;
using Crossbones.ALPR.Business.HotListDataSource.Delete;
using Azure.Core;
using Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Api.HotListDataSource.Service
{
    public class HotListDataSourceItemService : ServiceBase, IHotListDataSourceItemService
    {
        readonly ISequenceProxy _hotListSequenceProxy;

        public HotListDataSourceItemService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => _hotListSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotList);

        public async Task<SysSerial> Add(HotlistDataSource r)
        {
            var id = new SysSerial(await _hotListSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddHotListDataSourceItem(id)
            {
                Name = r.Name,
                SourceName = r.SourceName,
                AgencyID = r.AgencyId,
                SourceTypeID = r.SourceTypeId,
                SchedulePeriod = r.SchedulePeriod,
                LastUpdated = r.LastUpdated,
                IsExpire = r.IsExpire,
                SchemaDefinition = r.SchemaDefinition,
                LastUpdateExternalHotListID = r.LastUpdateExternalHotListId,
                ConnectionType = r.ConnectionType,
                Userid = r.Userid,
                locationPath = r.LocationPath,
                Password = r.Password,
                port = r.Port,
                LastRun = r.LastRun,
                Status = r.Status,
                SkipFirstLine = r.SkipFirstLine,
                StatusDesc = r.StatusDesc,
            };
            _ = await Execute(cmd);
            return id;
        }


        public async Task Change(SysSerial HotlistSysSerial, HotlistDataSource request)
        {

            var cmd = new ChangeHotListDataSourceItem(HotlistSysSerial)
            {
                Name = request.Name,
                SourceName = request.SourceName,
                AgencyID = request.AgencyId,
                SourceTypeID = request.SourceTypeId,
                SchedulePeriod = request.SchedulePeriod,
                LastUpdated = request.LastUpdated,
                IsExpire = request.IsExpire,
                SchemaDefinition = request.SchemaDefinition,
                LastUpdateExternalHotListID = request.LastUpdateExternalHotListId,
                ConnectionType = request.ConnectionType,
                Userid = request.Userid,
                locationPath = request.LocationPath,
                Password = request.Password,
                port = request.Port,
                LastRun = request.LastRun,
                Status = request.Status,
                SkipFirstLine = request.SkipFirstLine,
                StatusDesc = request.StatusDesc,
            };
            _ = await Execute(cmd);
        }


        public async Task Delete(SysSerial HotlistSysSerial)
        {
            var cmd = new DeleteHotListDataSourceItem(HotlistSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListDataSourceItem(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<HotListDataSourceItem> Get(SysSerial HotlistSysSerial)
        {
            var query = new GetHotListDataSource(HotlistSysSerial, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<HotListDataSourceItem>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<HotListDataSourceItem>> GetAll(Pager paging)
        {
            var dataQuery = new GetHotListDataSource(SysSerial.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<HotListDataSourceItem>>(dataQuery);

            var countQuery = new GetHotListDataSource(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }

    }
}
