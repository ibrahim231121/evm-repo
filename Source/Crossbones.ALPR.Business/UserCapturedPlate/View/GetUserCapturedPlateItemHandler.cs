using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetUserCapturedPlateItemHandler : QueryHandlerBase<GetUserCapturedPlateItem>
    {
        protected override async Task<object> OnQuery(GetUserCapturedPlateItem query, IQueryContext context, CancellationToken token)
        {
            var ucpRepsitory = context.Get<E.UserCapturedPlate>();

            switch (query.QueryFilter)
            {
                case Enums.GetQueryFilter.Single:
                    var resp = await ucpRepsitory.One(ucp => ucp.RecId == query.Id);
                    return resp == null ?
                                throw new RecordNotFound($"UserCapturedPlate with Id:{query.Id} not found.") :
                                new Tuple<long, long>(resp.UserId, resp.CapturedId);
                    break;
                case Enums.GetQueryFilter.AllWithoutPaging:
                case Enums.GetQueryFilter.All:
                    return await ucpRepsitory.Many().Select(ucp => new Tuple<long, long>(ucp.UserId, ucp.CapturedId)).ToListAsync(token);
                    break;
                case Enums.GetQueryFilter.Count:
                    return await ucpRepsitory.Many().CountAsync();
                    break;
                case Enums.GetQueryFilter.AllByUser:
                    return await ucpRepsitory.Many(ucp => query.UserId > 0 ? ucp.UserId == query.UserId : true).Select(ucp => new Tuple<long, long>(ucp.UserId, ucp.CapturedId)).ToListAsync(token);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
}
