using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.CapturedPlate
{
    /// <summary>
    /// ICapturedPlateService
    /// </summary>
    public interface ICapturedPlateService
    {


        /// <summary>
        /// Adds the specified captured plate.
        /// </summary>
        /// <param name="capturedPlate">The captured plate.</param>
        /// <returns></returns>
        Task<RecId> Add(CapturedPlateDTO capturedPlate);


        /// <summary>
        /// Gets the specified captured plate identifier.
        /// </summary>
        /// <param name="capturedPlateId">The captured plate identifier.</param>
        /// <returns></returns>
        Task<CapturedPlateDTO> Get(RecId capturedPlateId);


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        Task<PageResponse<CapturedPlateDTO>> GetAll(long userID, DateTime startDate, DateTime endDate, Pager paging, GridFilter filter, GridSort sort, long hotListId);


        /// <summary>
        /// Changes the specified captured plate system serial.
        /// </summary>
        /// <param name="capturedPlateRecId">The captured plate system serial.</param>
        /// <param name="capturedPlateItem">The captured plate item.</param>
        /// <returns></returns>
        Task Change(RecId capturedPlateRecId, CapturedPlateDTO capturedPlateItem);


        /// <summary>
        /// Deletes the specified captured plate system serial.
        /// </summary>
        /// <param name="capturedPlateRecId">The captured plate system serial.</param>
        /// <returns></returns>
        Task Delete(RecId capturedPlateRecId);


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <returns></returns>
        Task DeleteAll(long userId);
    }
}
