using Crossbones.ALPR.Business.State.Get;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.State
{
    public class StateService : ServiceBase, IStateService
    {
        public StateService(ServiceArguments args) : base(args)
        {
        }

        public async Task<List<DTO.StateDTO>> GetAll()
        {
            var dataQuery = new GetState(RecId.Empty) { };

            var stateList = await Inquire<IEnumerable<DTO.StateDTO>>(dataQuery);

            return stateList.ToList();
        }
    }
}