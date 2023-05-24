using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public interface INumberPlateService
    {
        Task<SysSerial> Add(M.NumberPlates request);
        Task<M.NumberPlates> Get(SysSerial LPSysSerial);
        Task<PagedResponse<M.NumberPlates>> GetAll(Pager page);
        Task Change(SysSerial LPSysSerial, M.NumberPlates request);
        Task Delete(SysSerial LPSysSerial);
        Task DeleteAll();
    }
}
