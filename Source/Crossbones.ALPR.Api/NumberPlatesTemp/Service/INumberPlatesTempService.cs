using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.NumberPlatesTemp.Service
{
    public interface INumberPlatesTempService
    {
        Task<RecId> Add(DTO.NumberPlateTempDTO request);
        Task<DTO.NumberPlateTempDTO> Get(RecId recId);
        Task<PagedResponse<DTO.NumberPlateTempDTO>> GetAll(Pager page);
        Task Change(RecId recId, DTO.NumberPlateTempDTO request);
        Task Delete(RecId recId);
        Task DeleteAll();
    }
}
