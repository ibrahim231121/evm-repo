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

        public async Task<RecId> Add(M.NumberPlateTempDTO request)
        {
            var id = new RecId(await _numberPlatesTempSequenceProxy.Next(CancellationToken.None));
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
        public async Task<PagedResponse<M.NumberPlateTempDTO>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlatesTemp(RecId.Empty, GetQueryFilter.All)
            {
                Paging = paging
            };
            var t0 = Inquire<IEnumerable<M.NumberPlateTempDTO>>(dataQuery);

            var countQuery = new GetNumberPlatesTemp(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
        public async Task<M.NumberPlateTempDTO> Get(RecId LPRecId)
        {
            var query = new GetNumberPlatesTemp(LPRecId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<M.NumberPlateTempDTO>>(query);
            return res.FirstOrDefault();
        }
        public async Task Change(RecId LPRecId, M.NumberPlateTempDTO request)
        {
            var cmd = new ChangeNumberPlatesTemp(LPRecId)
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

        public async Task Delete(RecId LPRecId)
        {
            var cmd = new DeleteNumberPlatesTemp(LPRecId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlatesTemp(RecId.Empty);
            _ = await Execute(cmd);
        }

    }
}
