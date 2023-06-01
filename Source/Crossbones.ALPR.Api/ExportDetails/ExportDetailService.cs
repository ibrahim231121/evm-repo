using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.ExportDetail.Add;
using Corssbones.ALPR.Business.ExportDetail.Change;
using Corssbones.ALPR.Business.ExportDetail.Delete;
using Corssbones.ALPR.Business.ExportDetail.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.ExportDetails
{
    public class ExportDetailService : ServiceBase, IExportDetailService
    {
        readonly ISequenceProxy exportDetailSequenceProxy;
        public ExportDetailService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args) => exportDetailSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.ExortDetail);

        public async Task<SysSerial> Add(ExportDetailItem addExportDetail)
        {
            var id = new SysSerial(await exportDetailSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddExportDetail(id)
            {
                CapturedPlateId = addExportDetail.CapturedPlateId,
                ExportedOn = addExportDetail.ExportedOn,
                ExportPath = addExportDetail.ExportPath,
                TicketNumber = addExportDetail.TicketNumber,
                UriLocation = addExportDetail.UriLocation
            };

            await Execute(cmd);
            return id;
        }

        public async Task Change(SysSerial Id, ExportDetailItem request)
        {
            var cmd = new ChangeExportDetail(Id)
            {
                TicketNumber = request.TicketNumber,
                CapturedPlateId = request.CapturedPlateId,
                UriLocation = request.UriLocation,
                ExportedOn = request.ExportedOn,
                ExportPath = request.ExportPath
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial Id)
        {
            var cmd = new DeleteExportDetail(Id);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteExportDetail(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<ExportDetailItem> Get(SysSerial Id)
        {
            var query = new GetExportDetail(Id, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<ExportDetailItem>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<ExportDetailItem>> GetAll(Pager paging)
        {
            var dataQuery = new GetExportDetail(SysSerial.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<ExportDetailItem>>(dataQuery);

            var countQuery = new GetExportDetail(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}