using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;

namespace Crossbones.ALPR.Api.HotList.Service
{
    public interface IHotListItemService
    {
        /// <summary>
        /// It is used to Add Hotlist data in the database
        /// </summary>
        /// <param name="request">Hotlist domain model</param>
        /// <returns>HotlistId generated against the record</returns>
        Task<SysSerial> Add(HotListItem request);

        /// <summary>
        /// It is used to Get Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns>Hotlist record</returns>
        Task<HotListItem> Get(SysSerial HotlistSysSerial);

        /// <summary>
        /// It is used to Get All Hotlist data from database
        /// </summary>
        /// <returns>List of Hotlist records</returns>
        Task<PagedResponse<HotListItem>> GetAll(Pager paging);

        /// <summary>
        /// It is used to update Hotlist data from database
        /// </summary>
        /// <param name="HotItemSysSerial">Database record Id</param>
        /// <param name="request">Hotlist domain model</param>
        /// <returns></returns>
        Task Change(SysSerial HotlistSysSerial, HotListItem request);

        /// <summary>
        /// It is used to Delete Single Hotlist data from database
        /// </summary>
        /// <param name="HotlistId">Database record Id</param>
        /// <returns></returns>
        Task Delete(SysSerial HotlistSysSerial);

        /// <summary>
        /// It is used to Delete All Hotlist data from database
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}
