using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.NumberPlateHistory.Get;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Business.NumberPlates.Change;
using Crossbones.ALPR.Business.NumberPlates.Delete;
using Crossbones.ALPR.Business.NumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using DTO = Crossbones.ALPR.Models.DTOs;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public class NumberPlateService : ServiceBase, INumberPlateService
    {
        readonly ISequenceProxy _numberPlatesSequenceProxy;
        readonly IMapper mapper;

        public NumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory, IMapper _mapper) : base(args)
        {
            _numberPlatesSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.NumberPlate);
            mapper = _mapper;
        }

        public async Task<RecId> Add(DTO.NumberPlateDTO request)
        {
            var id = new RecId(await _numberPlatesSequenceProxy.Next(CancellationToken.None));
            var cmd = await GetAddCommand(request);
            _ = await Execute(cmd);
            return id;
        }

        public async Task<AddNumberPlate> GetAddCommand(DTO.NumberPlateDTO request)
        {
            var numberPlateSysSerial = new RecId(await _numberPlatesSequenceProxy.Next(CancellationToken.None));
            var command = new AddNumberPlate(numberPlateSysSerial);

            command.Item = new E.NumberPlate()
            {
                RecId = command.Id,
                LicensePlate = request.LicensePlate,
                DateOfInterest = Convert.ToDateTime(request.DateOfInterest),
                CreatedOn = request.CreatedOn,
                LastUpdatedOn = request.LastUpdatedOn,
                //LastTimeStamp = request.LastTimeStamp,
            };

            //var _obj = mapper.Map<M.NumberPlates, E.NumberPlate>(request);
            //command.Item = _obj;
            //command.Item.SysSerial = command.Id;

            return command;
        }

        public async Task<PageResponse<DTO.NumberPlateDTO>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.All)
            { Paging = paging, GridFilter = filter, Sort = sort };
            return await Inquire<PageResponse<DTO.NumberPlateDTO>>(dataQuery);

        }
        public async Task<PagedResponse<DTO.NumberPlateDTO>> GetAllByHotList(Pager page, long hotListId)
        {
            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.FilterByHostList, hotListId)
            {
                Paging = page
            };
            var t0 = Inquire<IEnumerable<DTO.NumberPlateDTO>>(dataQuery);

            var countQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }

        public async Task<PageResponse<DTO.NumberPlateHistoryDTO>> GetNumberPlateHistory(RecId numberPlateId, Pager pager)
        {
            GridFilter filter = GetGridFilter();

            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlateHistoryItem(numberPlateId, pager, filter, sort);

            var taskGetNumberPlateHistory = Inquire<PageResponse<DTO.NumberPlateHistoryDTO>>(dataQuery);

            var numberPlateHistory = await taskGetNumberPlateHistory;

            return numberPlateHistory;
        }
        public async Task Change(RecId LPRecId, DTO.NumberPlateDTO request)
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
                DateOfInterest = Convert.ToDateTime(request.DateOfInterest),
                //LastTimeStamp = request.LastTimeStamp,
                LastUpdatedOn = DateTime.UtcNow,
                LicenseYear = request.LicenseYear,
                Note = request.Note,
                Notes = request.Notes,
                NumberPlate = request.LicensePlate,
                //StateId = request.StateId,
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

        public async Task Delete(RecId LPSysSerial)
        {
            var cmd = new DeleteNumberPlate(LPSysSerial);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<DTO.NumberPlateDTO> Get(RecId LPSysSerial)
        {
            var query = new GetNumberPlate(LPSysSerial, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<DTO.NumberPlateDTO>>(query);
            return res.FirstOrDefault();
        }
    }
}