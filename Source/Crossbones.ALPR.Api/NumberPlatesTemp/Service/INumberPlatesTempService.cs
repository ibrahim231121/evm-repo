using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlatesTemp.Service
{
    public interface INumberPlatesTempService
    {
        Task<SysSerial> Add(M.NumberPlatesTemp request);
        Task<M.NumberPlatesTemp> Get(SysSerial LPSysSerial);
        Task<PagedResponse<M.NumberPlatesTemp>> GetAll(Pager page);
        Task Change(SysSerial LPSysSerial, M.NumberPlatesTemp request);
        Task Delete(SysSerial LPSysSerial);
        Task DeleteAll();
    }
}
