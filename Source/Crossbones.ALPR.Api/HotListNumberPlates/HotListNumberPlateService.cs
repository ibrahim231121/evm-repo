using AutoMapper;
using Corssbones.ALPR.Business.Enums;
using Corssbones.ALPR.Business.HotListNumberPlates.Add;
using Corssbones.ALPR.Business.HotListNumberPlates.Change;
using Corssbones.ALPR.Business.HotListNumberPlates.Delete;
using Corssbones.ALPR.Business.HotListNumberPlates.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;
using DTO = Crossbones.ALPR.Models.DTOs;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    public class HotListNumberPlateService : ServiceBase, IHotListNumberPlateService
    {
        readonly ISequenceProxy hotListNumberSequenceProxy;
        readonly IMapper mapper;
        public HotListNumberPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory, IMapper _mapper) : base(args)
        {
            hotListNumberSequenceProxy = sequenceProxyFactory.GetProxy(ALPRResources.HotListNumberPlate);
            mapper = _mapper;
        }
        public async Task<RecId> Add(DTO.HotListNumberPlateDTO request)
        {
            var id = new RecId(await hotListNumberSequenceProxy.Next(CancellationToken.None));
            var cmd = await GetAddCommand(request);
            await Execute(cmd);
            return id;
        }

        public async Task<AddHotListNumberPlate> GetAddCommand(DTO.HotListNumberPlateDTO request)
        {
            var hotListNumberPlateId = new RecId(await hotListNumberSequenceProxy.Next(CancellationToken.None));
            var command = new AddHotListNumberPlate(hotListNumberPlateId);
            command.Item = new Entities.HotListNumberPlate
            {
                RecId = command.Id,
                HotListId = request.HotListId,
                NumberPlateId = request.NumberPlateId,
                CreatedOn = DateTime.Now,
                LastUpdatedOn = DateTime.Now,
            };

            return command;
        }

        public async Task Change(RecId recId, DTO.HotListNumberPlateDTO request)
        {
            var cmd = new ChangeHotListNumberPlate(recId)
            {
                HotListID = request.HotListId,
                CreatedOn = DateTime.UtcNow,
                NumberPlatesId = request.NumberPlateId,
                LastUpdatedOn = DateTime.UtcNow,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId recId)
        {
            var cmd = new DeleteHotListNumberPlate(recId);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<DTO.HotListNumberPlateDTO> Get(RecId recId)
        {
            var query = new GetHotListNumberPlate(recId, GetQueryFilter.Single);
            var res = await Inquire<IEnumerable<DTO.HotListNumberPlateDTO>>(query);
            return res.FirstOrDefault();
        }

        public async Task<PagedResponse<DTO.HotListNumberPlateDTO>> GetAll(Pager paging)
        {
            var dataQuery = new GetHotListNumberPlate(RecId.Empty, GetQueryFilter.All) { Paging = paging };
            var t0 = Inquire<IEnumerable<DTO.HotListNumberPlateDTO>>(dataQuery);

            var countQuery = new GetHotListNumberPlate(RecId.Empty, GetQueryFilter.Count);
            var t1 = Inquire<RowCount>(countQuery);

            await Task.WhenAll(t0, t1);
            var list = await t0;
            var total = (await t1).Total;

            return PaginationHelper.GetPagedResponse(list, total);
        }

    }
}