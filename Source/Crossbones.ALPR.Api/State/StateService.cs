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

        public async Task<List<StateItem>> GetAll()
        {
            var dataQuery = new GetState(SysSerial.Empty) { };

            var stateList = await Inquire<IEnumerable<StateItem>>(dataQuery);

            return stateList.ToList();
        }
    }
}