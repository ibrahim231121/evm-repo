using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.NumberPlatesTemp.Add;
using Corssbones.ALPR.Business.NumberPlatesTemp.Change;
using Corssbones.ALPR.Business.NumberPlatesTemp.Delete;
using Corssbones.ALPR.Business.NumberPlatesTemp.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlatesTemp.Service
{
    public class NumberPlatesTempService : ServiceBase, INumberPlatesTempService
    {
        readonly ISequenceProxy _numberPlatesTempSequenceProxy;
        public NumberPlatesTempService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args)
            => _numberPlatesTempSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.NumberPlatesTemp);

        public async Task<SysSerial> Add(M.NumberPlateTempItem request)
        {
            var id = new SysSerial(await _numberPlatesTempSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddNumberPlatesTemp(id)
            {
                Ncicnumber = request.Ncicnumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Alias = request.Alias,
                AgencyId = request.AgencyId,
                InsertType = request.InsertType,
                LicenseType = request.LicenseType,
                CreatedOn = DateTime.UtcNow,
                DateOfInterest = request.DateOfInterest,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                NumberPlate = request.NumberPlate,
                StateId = request.StateId,
                Status = request.Status,
                VehicleColor = request.VehicleColor,
                VehicleMake = request.VehicleMake,
                VehicleModel = request.VehicleModel,
                VehicleStyle = request.VehicleStyle,
                VehicleYear = request.VehicleYear,
                ViolationInfo = request.ViolationInfo,
                ImportSerialId = request.ImportSerialId
            };
            _ = await Execute(cmd);
            return id;
        }
        public async Task<PagedResponse<M.NumberPlateTempItem>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlatesTemp(SysSerial.Empty, GetQueryFilter.All)
            {
                Paging = paging
            };
            var t0 = Inquire<IEnumerable<M.NumberPlateTempItem>>(dataQuery);

            var countQuery = new GetNumberPlatesTemp(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
        public async Task<M.NumberPlateTempItem> Get(SysSerial LPSysSerial)
        {
            var query = new GetNumberPlatesTemp(LPSysSerial, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<M.NumberPlateTempItem>>(query);
            return res.FirstOrDefault();
        }
        public async Task Change(SysSerial LPSysSerial, M.NumberPlateTempItem request)
        {
            var cmd = new ChangeNumberPlatesTemp(LPSysSerial)
            {
                Ncicnumber = request.Ncicnumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Alias = request.Alias,
                AgencyId = request.AgencyId,
                InsertType = request.InsertType,
                LicenseType = request.LicenseType,
                CreatedOn = request.CreatedOn,
                DateOfInterest = request.DateOfInterest,
                LastUpdatedOn = DateTime.UtcNow,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                NumberPlate = request.NumberPlate,
                StateId = request.StateId,
                Status = request.Status,
                VehicleColor = request.VehicleColor,
                VehicleMake = request.VehicleMake,
                VehicleModel = request.VehicleModel,
                VehicleStyle = request.VehicleStyle,
                VehicleYear = request.VehicleYear,
                ViolationInfo = request.ViolationInfo,
                ImportSerialId = request.ImportSerialId
            };

            _ = await Execute(cmd);
        }

        public async Task Delete(SysSerial LPSysSerial)
        {
            var cmd = new DeleteNumberPlatesTemp(LPSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlatesTemp(SysSerial.Empty);
            _ = await Execute(cmd);
        }

    }
}
