using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;

namespace Crossbones.ALPR.Api.HotList.Service
{
    public interface IHotListService
    {
        /// <summary>
        /// It is used to Add Hotlist data in the database
        /// </summary>
        /// <param name="request">Hotlist domain model</param>
        /// <returns>HotlistId generated against the record</returns>
        Task<RecId> Add(DTO.HotListDTO request);

        /// <summary>
        /// It is used to Get Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns>Hotlist record</returns>
        Task<DTO.HotListDTO> Get(RecId recId);

        /// <summary>
        /// It is used to Get All Hotlist data from database
        /// </summary>
        /// <returns>List of Hotlist records</returns>
        Task<PageResponse<DTO.HotListDTO>> GetAll(Pager paging);

        /// <summary>
        /// It is used to update Hotlist data from database
        /// </summary>
        /// <param name="HotItemRecId">Database record Id</param>
        /// <param name="request">Hotlist domain model</param>
        /// <returns></returns>
        Task Change(RecId recId, DTO.HotListDTO request);

        /// <summary>
        /// It is used to Delete Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns></returns>
        Task Delete(RecId recId);

        /// <summary>
        /// It is used to Delete All Hotlist data from database
        /// </summary>
        /// <returns></returns>
        Task DeleteMany(List<long> hotlistIds);
    }
}
