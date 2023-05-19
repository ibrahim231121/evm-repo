using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Business.CapturedPlate.View;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Tuple<long, long>> Get(SysSerial userCapturedPlateId)
        {
            var query = new GetUserCapturedPlateItem(new SysSerial(0),
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
                                                                          new SysSerial(0),
                                                                          GetQueryFilter.AllByUser);

            var resp = await Inquire<List<Tuple<long, long>>>(query);

            return resp;
        }
    }
}
