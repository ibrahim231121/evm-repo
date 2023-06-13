using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;

namespace Crossbones.ALPR.Api.NumberPlates.Service
{
    public interface INumberPlateService
    {
        Task<SysSerial> Add(NumberPlateItem request);
        Task<NumberPlateItem> Get(SysSerial LPSysSerial);
        Task<PageResponse<NumberPlateItem>> GetAll(Pager page);
        Task<PagedResponse<NumberPlateItem>> GetAllByHotList(Pager page, long hotListId);
        Task Change(SysSerial LPSysSerial, NumberPlateItem request);
        Task Delete(SysSerial LPSysSerial);
        Task DeleteAll();
    }
}