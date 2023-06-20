using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlatesTemp.Service
{
    public interface INumberPlatesTempService
    {
        Task<RecId> Add(M.NumberPlateTempDTO request);
        Task<M.NumberPlateTempDTO> Get(RecId LPRecId);
        Task<PagedResponse<M.NumberPlateTempDTO>> GetAll(Pager page);
        Task Change(RecId LPRecId, M.NumberPlateTempDTO request);
        Task Delete(RecId LPRecId);
        Task DeleteAll();
    }
}
