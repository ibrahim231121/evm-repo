﻿using AutoMapper;
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
using E = Corssbones.ALPR.Database.Entities;

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
            command.Item = new E.HotListNumberPlate
            {
                RecId = command.Id,
                HotListId = request.HotListId,
                NumberPlatesId = request.NumberPlatesId,
                CreatedOn = DateTime.Now,
                LastUpdatedOn = DateTime.Now,
            };

            return command;
        }

        public async Task Change(RecId Id, DTO.HotListNumberPlateDTO request)
        {
            var cmd = new ChangeHotListNumberPlate(Id)
            {
                HotListID = request.HotListId,
                CreatedOn = DateTime.UtcNow,
                NumberPlatesId = request.NumberPlatesId,
                LastUpdatedOn = DateTime.UtcNow,
            };
            _ = await Execute(cmd);
        }

        public async Task Delete(RecId Id)
        {
            var cmd = new DeleteHotListNumberPlate(Id);
            _ = await Execute(cmd);
        }

        public async Task DeleteAll()
        {
            var cmd = new DeleteHotListNumberPlate(RecId.Empty);
            _ = await Execute(cmd);
        }

        public async Task<DTO.HotListNumberPlateDTO> Get(RecId Id)
        {
            var query = new GetHotListNumberPlate(Id, GetQueryFilter.Single);
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