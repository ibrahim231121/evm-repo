using Crossbones.ALPR.Business.State.Get;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.State
{
    public class StateService : ServiceBase, IStateService
    {
        public StateService(ServiceArguments args) : base(args)
        {
        }

        public async Task<List<StateDTO>> GetAll()
        {
            var dataQuery = new GetState(RecId.Empty) { };

            var stateList = await Inquire<IEnumerable<StateDTO>>(dataQuery);

            return stateList.ToList();
        }
    }
}