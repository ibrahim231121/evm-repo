using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Business.NumberPlates.Change;
using Crossbones.ALPR.Business.NumberPlates.Delete;
using Crossbones.ALPR.Business.NumberPlates.View;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public class NumberPlateService : ServiceBase, INumberPlateService
    {
        readonly ISequenceProxy _numberPlatesSequenceProxy;
        public NumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args)
            => _numberPlatesSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.NumberPlate);

        public async Task<SysSerial> Add(M.NumberPlates request)
        {
            var id = new SysSerial(await _numberPlatesSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddNumberPlate(id)
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
                //LastTimeStamp = request.LastTimeStamp,
                LastUpdatedOn = request.LastUpdatedOn,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                Notes = request.Notes,
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

        public async Task<PagedResponse<M.NumberPlates>> GetAll(Pager paging)
        {
            var dataQuery = new GetNumberPlate(SysSerial.Empty, GetQueryFilter.All)
            {
                Paging = paging
            };
            var t0 = Inquire<IEnumerable<M.NumberPlates>>(dataQuery);

            var countQuery = new GetNumberPlate(SysSerial.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);

        }
        public async Task Change(SysSerial LPSysSerial, M.NumberPlates request)
        {
            var cmd = new ChangeNumberPlate(LPSysSerial)
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
                //LastTimeStamp = request.LastTimeStamp,
                LastUpdatedOn = request.LastUpdatedOn,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                Notes = request.Notes,
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
            var cmd = new DeleteNumberPlate(LPSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlate(SysSerial.Empty);
            _ = await Execute(cmd);
        }

        public async Task<M.NumberPlates> Get(SysSerial LPSysSerial)
        {
            var query = new GetNumberPlate(LPSysSerial, GetQueryFilter.Single);    
            var res = await Inquire<IEnumerable<M.NumberPlates>>(query);
            return res.FirstOrDefault();
        }
    }
}
