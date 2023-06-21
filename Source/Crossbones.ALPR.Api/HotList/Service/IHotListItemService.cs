using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.HotList.Service
{
    public interface IHotListItemService
    {
        /// <summary>
        /// It is used to Add Hotlist data in the database
        /// </summary>
        /// <param name="request">Hotlist domain model</param>
        /// <returns>HotlistId generated against the record</returns>
        Task<RecId> Add(HotListDTO request);

        /// <summary>
        /// It is used to Get Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns>Hotlist record</returns>
        Task<HotListDTO> Get(RecId HotlistRecId);

        /// <summary>
        /// It is used to Get All Hotlist data from database
        /// </summary>
        /// <returns>List of Hotlist records</returns>
        Task<PageResponse<HotListDTO>> GetAll(Pager paging, GridFilter filter, GridSort sort);

        /// <summary>
        /// It is used to update Hotlist data from database
        /// </summary>
        /// <param name="HotItemRecId">Database record Id</param>
        /// <param name="request">Hotlist domain model</param>
        /// <returns></returns>
        Task Change(RecId HotlistRecId, HotListDTO request);

        /// <summary>
        /// It is used to Delete Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns></returns>
        Task Delete(RecId HotlistRecId);

        /// <summary>
        /// It is used to Delete All Hotlist data from database
        /// </summary>
        /// <returns></returns>
        Task DeleteMany(List<long> hotlistIds);
    }
}
