using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlatesTemp.Service
{
    public interface INumberPlatesTempService
    {
        Task<SysSerial> Add(M.NumberPlateTempItem request);
        Task<M.NumberPlateTempItem> Get(SysSerial LPSysSerial);
        Task<PagedResponse<M.NumberPlateTempItem>> GetAll(Pager page);
        Task Change(SysSerial LPSysSerial, M.NumberPlateTempItem request);
        Task Delete(SysSerial LPSysSerial);
        Task DeleteAll();
    }
}
