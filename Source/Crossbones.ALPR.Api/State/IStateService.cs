using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.State
{
    public interface IStateService
    {
        Task<List<StateDTO>> GetAll();
    }
}
