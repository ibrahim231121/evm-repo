using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    public interface IHotListNumberPlateService
    {
        /// <summary>
        /// It is used to Get All Hotlist Number Plate data from database
        /// </summary>
        /// <returns>List of Hotlist Number Plate records</returns>
        Task<PagedResponse<HotListNumberPlateItem>> GetAll(Pager paging);

        /// <summary>
        /// Add HotList Number Plate
        /// </summary>
        /// <param name="addHotListNumber"></param>
        /// <returns></returns>
        Task<SysSerial> Add(HotListNumberPlateItem request);

        /// <summary>
        /// Get HotList Number Plate by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<HotListNumberPlateItem> Get(SysSerial Id);

        /// <summary>
        /// Edit or Update HotList Number Plate
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Change(SysSerial Id, HotListNumberPlateItem request);

        /// <summary>
        /// Delete HotList Number Plate through Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task Delete(SysSerial Id);

        /// <summary>
        /// Delete every HotList Number Plates
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}