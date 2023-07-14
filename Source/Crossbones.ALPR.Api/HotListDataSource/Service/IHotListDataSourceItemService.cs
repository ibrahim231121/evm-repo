using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSource.Service
{
    public interface IHotListDataSourceItemService
    {
        /// <summary>
        /// It is used to Add HotListDataSource data in the database
        /// </summary>
        /// <param name="request">HotListDataSource domain model</param>
        /// <returns>HotlistId generated against the record</returns>
        Task<RecId> Add(DTO.HotListDataSourceDTO request);

        /// <summary>
        /// It is used to Get Single HotListDataSource data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns>HotListDataSource record</returns>
        Task<DTO.HotListDataSourceDTO> Get(RecId recId);

        /// <summary>
        /// It is used to Get All HotListDataSource data from database
        /// </summary>
        /// <returns>List of HotListDataSource records</returns>
        Task<PageResponse<DTO.HotListDataSourceDTO>> GetAll(Pager paging);

        /// <summary>
        /// It is used to update HotListDataSource data from database
        /// </summary>
        /// <param name="HotItemRecId">Database record Id</param>
        /// <param name="request">HotListDataSource domain model</param>
        /// <returns></returns>
        Task Change(RecId recId, HotlistDataSource request);

        /// <summary>
        /// It is used to Delete Single HotListDataSource data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns></returns>
        Task Delete(RecId recId);

        /// <summary>
        /// It is used to Delete All HotListDataSource data from database
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}
