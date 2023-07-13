using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using DTO = Crossbones.ALPR.Models.DTOs;

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
        Task<DTO.ExportDetailDTO> Get(RecId Id);

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
        Task Change(RecId Id, DTO.ExportDetailDTO request);

        /// <summary>
        /// Delete Export Detail through Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task Delete(RecId Id);

        /// <summary>
        /// Delete every Export Details
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}