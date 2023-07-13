using Crossbones.ALPR.Business.NumberPlates.Add;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public interface INumberPlateService
    {
        Task<RecId> Add(DTO.NumberPlateDTO request);
        Task<DTO.NumberPlateDTO> Get(RecId LPRecId);
        Task<PageResponse<DTO.NumberPlateDTO>> GetAll(Pager page);
        Task<PagedResponse<DTO.NumberPlateDTO>> GetAllByHotList(Pager page, long hotListId);

        Task<PageResponse<DTO.NumberPlateHistoryDTO>> GetNumberPlateHistory(RecId numberPlateId, Pager page);

        Task Change(RecId LPRecId, DTO.NumberPlateDTO request);
        Task Delete(RecId LPRecId);
        Task DeleteAll();
        Task<AddNumberPlate> GetAddCommand(DTO.NumberPlateDTO request);
    }
}