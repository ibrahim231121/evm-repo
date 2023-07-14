using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public interface IUserCapturedPlateService
    {
        /// <summary>
        /// Gets the specified user captured plate identifier.
        /// </summary>
        /// <param name="recId">The user captured plate identifier.</param>
        /// <returns></returns>
        Task<Tuple<long, long>> Get(RecId recId);

        /// <summary>
        /// Gets all captured plate Id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<Tuple<long, long>>> GetAll(long userId);
    }
}
