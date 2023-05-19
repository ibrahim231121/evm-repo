using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.CapturePlatesSummary
{
    public interface ICapturePlatesSummaryService
    {

        /// <summary>
        /// Adds the specified captured plate summary  item.
        /// </summary>
        /// <param name="capturedPlateSummaryItem">The captured plate summary  item.</param>
        /// <returns></returns>
        Task<SysSerial> Add(CapturePlatesSummaryItem capturedPlateSummaryItem);


        /// <summary>
        /// Gets the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="capturedPlateId">The captured plate identifier.</param>
        /// <returns></returns>
        Task<CapturePlatesSummaryItem> Get(long userId, long capturedPlateId);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        Task<PagedResponse<CapturePlatesSummaryItem>> GetAll(Pager paging, GridFilter filter, GridSort sort, long userId = 0);

        /// <summary>
        /// Gets all CapturePlatesSummaryItems with out paging.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<CapturePlatesSummaryItem>> GetAllWithOutPaging(GridFilter filter, GridSort sort, long userId = 0);


        /// <summary>
        /// Changes the specified captured plate summary  system serial.
        /// </summary>
        /// <param name="capturedPlateSummarySysSerial">The captured plate summary  system serial.</param>
        /// <param name="capturedPlateSummaryItem">The captured plate summary  item.</param>
        /// <returns></returns>
        Task Change(CapturePlatesSummaryItem capturedPlateSummaryItem);


        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="capturedPlateId">The captured plate identifier.</param>
        /// <returns></returns>
        Task Delete(long userId, long capturedPlateId);


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task DeleteAll(long userId);
    }
}
