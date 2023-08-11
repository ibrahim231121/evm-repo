using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.NumberPlateHistory.Get;
using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Business.NumberPlates.Change;
using Crossbones.ALPR.Business.NumberPlates.Delete;
using Crossbones.ALPR.Business.NumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using Crossbones.ALPR.Models;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public class NumberPlateService : ServiceBase, INumberPlateService
    {
        readonly ISequenceProxy _numberPlatesSequenceProxy;
        public NumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory) : base(args)
        {
            _numberPlatesSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.NumberPlate);
        }

        public async Task<RecId> Add(DTO.NumberPlateDTO request)
        {
            request.CreatedOn = DateTime.UtcNow;
            request.LastUpdatedOn = DateTime.UtcNow;
            var cmd = await GetAddCommand(request);
            _ = await Execute(cmd);
            return cmd.Id;
        }

        public async Task<AddNumberPlate> GetAddCommand(DTO.NumberPlateDTO request)
        {
            var recId = new RecId(await _numberPlatesSequenceProxy.Next(CancellationToken.None));
            var command = new AddNumberPlate(recId);
            command.NumberPlateDTO = request;
            command.NumberPlateDTO.RecId = command.Id;

            return command;
        }

        public Task<PageResponse<DTO.NumberPlateDTO>> GetAll(Pager paging)
        {
            GridFilter filter = GetGridFilter();
            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.All) { Paging = paging, GridFilter = filter, Sort = sort};
            return Inquire<PageResponse<DTO.NumberPlateDTO>>(dataQuery);

        }

        public async Task<PageResponse<DTO.NumberPlateHistoryDTO>> GetNumberPlateHistory(RecId recId, Pager pager)
        {
            GridFilter filter = GetGridFilter();

            GridSort sort = GetGridSort();

            var dataQuery = new GetNumberPlateHistoryItem(recId, pager, filter, sort);

            var taskGetNumberPlateHistory = Inquire<PageResponse<DTO.NumberPlateHistoryDTO>>(dataQuery);

            var numberPlateHistory = await taskGetNumberPlateHistory;

            return numberPlateHistory;
        }
        public async Task Change(RecId recId, DTO.NumberPlateDTO request)
        {
            request.LastUpdatedOn = DateTime.UtcNow;
            var cmd = new ChangeNumberPlate(recId)
            {
                NumberPlateDTO = request              
            };

            _ = await Execute(cmd);
        }

        public async Task Delete(RecId recId)
        {
            var cmd = new DeleteNumberPlate(recId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<DTO.NumberPlateDTO> Get(RecId RecId)
        {
            var query = new GetNumberPlate(RecId, GetQueryFilter.Single);
            var res = await Inquire<DTO.NumberPlateDTO>(query);
            return res;
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

        public async Task<List<string>> GetNumberPlate(string numberPlate)
        {
            var dataQuery = new GetNumberPlate(RecId.Empty, GetQueryFilter.SearchByNumberPlate, numberPlate);
            var list = await Inquire<List<string>>(dataQuery);
            return list;
        }
        
    }
}