using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public interface ICapturePlatesSummaryStatusService
    {

        /// <summary>
        /// Adds the specified captured plate summary status item.
        /// </summary>
        /// <param name="capturedPlateSummaryStatusItem">The captured plate summary status item.</param>
        /// <returns></returns>
        Task<RecId> Add(CapturePlatesSummaryStatusDTO capturedPlateSummaryStatusItem);


        /// <summary>
        /// Gets the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="recId">The captured plate summary status system serial.</param>
        /// <returns></returns>
        Task<CapturePlatesSummaryStatusDTO> Get(RecId recId);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        Task<PagedResponse<CapturePlatesSummaryStatusDTO>> GetAll(Pager paging, GridFilter filter, GridSort sort);


        /// <summary>
        /// Changes the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="recId">The captured plate summary status system serial.</param>
        /// <param name="capturedPlateSummaryStatusItem">The captured plate summary status item.</param>
        /// <returns></returns>
        Task Change(RecId recId, CapturePlatesSummaryStatusDTO capturedPlateSummaryStatusItem);


        /// <summary>
        /// Deletes the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="recId">The captured plate summary status system serial.</param>
        /// <returns></returns>
        Task Delete(RecId recId);


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}
