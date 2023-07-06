using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.State
{
    public interface IStateService
    {
        Task<List<DTO.StateDTO>> GetAll();
    }
}
