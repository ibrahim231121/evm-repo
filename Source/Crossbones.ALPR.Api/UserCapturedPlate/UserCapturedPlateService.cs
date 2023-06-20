using Corssbones.ALPR.Business.CapturedPlate.Get;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    public class UserCapturedPlateService : ServiceBase, IUserCapturedPlateService
    {
        public UserCapturedPlateService(ServiceArguments args) : base(args)
        {

        }

        /// <summary>
        /// Gets the specified user captured plate identifier.
        /// </summary>
        /// <param name="userCapturedPlateId">The user captured plate identifier.</param>
        /// <returns></returns>
        public async Task<Tuple<long, long>> Get(RecId userCapturedPlateId)
        {
            var query = new GetUserCapturedPlateItem(new RecId(0),
                                                            userCapturedPlateId,
                                                            GetQueryFilter.Single);

            var resp = await Inquire<Tuple<long, long>>(query);

            return resp;
        }

        /// <summary>
        /// Gets all captured plate Id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<Tuple<long, long>>> GetAll(long userId)
        {
            GetUserCapturedPlateItem query = new GetUserCapturedPlateItem(userId,
                                                                          new RecId(0),
                                                                          GetQueryFilter.AllByUser);

            var resp = await Inquire<List<Tuple<long, long>>>(query);

            return resp;
        }
    }
}
