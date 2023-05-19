using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public interface ICapturePlatesSummaryStatusService
    {

        /// <summary>
        /// Adds the specified captured plate summary status item.
        /// </summary>
        /// <param name="capturedPlateSummaryStatusItem">The captured plate summary status item.</param>
        /// <returns></returns>
        Task<SysSerial> Add(CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem);


        /// <summary>
        /// Gets the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="capturedPlateSummaryStatusSysSerial">The captured plate summary status system serial.</param>
        /// <returns></returns>
        Task<CapturePlatesSummaryStatusItem> Get(SysSerial capturedPlateSummaryStatusSysSerial);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="paging">The paging.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        Task<PagedResponse<CapturePlatesSummaryStatusItem>> GetAll(Pager paging, GridFilter filter, GridSort sort);


        /// <summary>
        /// Changes the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="capturedPlateSummaryStatusSysSerial">The captured plate summary status system serial.</param>
        /// <param name="capturedPlateSummaryStatusItem">The captured plate summary status item.</param>
        /// <returns></returns>
        Task Change(SysSerial capturedPlateSummaryStatusSysSerial, CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem);


        /// <summary>
        /// Deletes the specified captured plate summary status system serial.
        /// </summary>
        /// <param name="capturedPlateSummaryStatusSysSerial">The captured plate summary status system serial.</param>
        /// <returns></returns>
        Task Delete(SysSerial capturedPlateSummaryStatusSysSerial);


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}
