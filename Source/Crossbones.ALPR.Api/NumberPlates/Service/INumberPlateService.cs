using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public interface INumberPlateService
    {
        Task<RecId> Add(NumberPlateDTO request);
        Task<NumberPlateDTO> Get(RecId LPRecId);
        Task<PageResponse<NumberPlateDTO>> GetAll(Pager page);
        Task<PagedResponse<NumberPlateDTO>> GetAllByHotList(Pager page, long hotListId);

        Task<PageResponse<NumberPlateHistoryDTO>> GetNumberPlateHistory(RecId numberPlateId, Pager page);

        Task Change(RecId LPRecId, NumberPlateDTO request);
        Task Delete(RecId LPRecId);
        Task DeleteAll();
    }
}