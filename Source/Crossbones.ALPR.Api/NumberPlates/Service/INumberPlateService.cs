using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.ALPR.Business.NumberPlates.Add;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public interface INumberPlateService
    {
        Task<PageResponse<DTO.NumberPlateHistoryDTO>> GetNumberPlateHistory(RecId recId, Pager page);

        Task Change(RecId recId, DTO.NumberPlateDTO request);
        Task Delete(RecId recId);
        Task<RecId> Add(DTO.NumberPlateDTO request);
        Task<AddNumberPlate> GetAddCommand(DTO.NumberPlateDTO request);
        Task<DTO.NumberPlateDTO> Get(RecId recId);
        Task<PageResponse<DTO.NumberPlateDTO>> GetAll(Pager page);
        Task<PagedResponse<DTO.NumberPlateDTO>> GetAllByHotList(Pager page, long hotListId);
        Task<List<string>> GetNumberPlate(string numberPlate);
        Task DeleteAll();
    }
}