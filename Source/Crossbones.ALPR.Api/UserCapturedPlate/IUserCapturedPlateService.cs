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
    public interface IUserCapturedPlateService
    {
        /// <summary>
        /// Gets the specified user captured plate identifier.
        /// </summary>
        /// <param name="userCapturedPlateId">The user captured plate identifier.</param>
        /// <returns></returns>
        Task<Tuple<long, long>> Get(SysSerial userCapturedPlateId);

        /// <summary>
        /// Gets all captured plate Id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<Tuple<long, long>>> GetAll(long userId);
    }
}
