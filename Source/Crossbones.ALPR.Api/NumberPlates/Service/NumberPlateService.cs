﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Business.NumberPlates.Change;
using Crossbones.ALPR.Business.NumberPlates.Delete;
using Crossbones.ALPR.Business.NumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public class NumberPlateService : ServiceBase, INumberPlateService
    {
        readonly ISequenceProxy _numberPlatesSequenceProxy;
        public NumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args)
            => _numberPlatesSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.NumberPlate);

        public async Task<RecId> Add(NumberPlateDTO request)
        {
            var id = new RecId(await _numberPlatesSequenceProxy.Next(CancellationToken.None));
            var cmd = new AddNumberPlate(id)
            {
                RecId = id,
                Ncicnumber = request.NCICNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Alias = request.Alias,
                AgencyId = request.AgencyId,
                InsertType = (short)request.InsertType,
                LicenseType = request.LicenseType,
                CreatedOn = DateTime.UtcNow,
                DateOfInterest = DateTime.Parse(request.DateOfInterest),
                //LastTimeStamp = request.LastTimeStamp,
                LastUpdatedOn = request.LastUpdatedOn,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                Notes = request.Notes,
                LicensePlate = request.LicensePlate,
                StateId = request.StateId,
                Status = 0,
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

        public Task<PageResponse<NumberPlateDTO>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.All, filter, sort) { Paging = paging };
            return Inquire<PageResponse<NumberPlateDTO>>(dataQuery);

        }
        public async Task Change(RecId LPRecId, NumberPlateDTO request)
        {
            var cmd = new ChangeNumberPlate(LPRecId)
            {
                Ncicnumber = request.NCICNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Alias = request.Alias,
                AgencyId = request.AgencyId,
                InsertType = request.InsertType,
                LicenseType = request.LicenseType,
                CreatedOn = (DateTime)request.CreatedOn,
                DateOfInterest = DateTime.Parse(request.DateOfInterest),
                //LastTimeStamp = request.LastTimeStamp,
                LastUpdatedOn = DateTime.UtcNow,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                Notes = request.Notes,
                NumberPlate = request.LicensePlate,
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
            var cmd = new DeleteNumberPlate(LPRecId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<NumberPlateDTO> Get(RecId RecId)
        {
            var query = new GetNumberPlate(RecId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<NumberPlateDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<NumberPlateDTO>> GetAllByHotList(Pager page, long hotListId)
        {
            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.FilterByHostList, hotListId)
            {
                Paging = page
            };
            var t0 = Inquire<IEnumerable<NumberPlateDTO>>(dataQuery);

            var countQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }
    }
}