using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common.Pagination;

namespace Crossbones.ALPR.Api.ExportDetails
{
    public interface IExportDetailService
    {
        /// <summary>
        /// Add Export Detail
        /// </summary>
        /// <param name="addExportDetail"></param>
        /// <returns></returns>
        Task<RecId> Add(DTO.ExportDetailDTO addExportDetail);

        /// <summary>
        /// Get Export Detail by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DTO.ExportDetailDTO> Get(RecId recId);

        /// <summary>
        /// Get all Export Details
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<PagedResponse<DTO.ExportDetailDTO>> GetAll(Pager paging);

        /// <summary>
        /// Edit or Update Export Detail
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Change(RecId recId, DTO.ExportDetailDTO request);

        /// <summary>
        /// Delete Export Detail through Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task Delete(RecId recId);

        /// <summary>
        /// Delete every Export Details
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}