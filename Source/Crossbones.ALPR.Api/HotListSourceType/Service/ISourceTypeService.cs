using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.HotListSourceType.Service
{
    public interface ISourceTypeService
    {

        /// <summary>
        /// It is used to Add SourceType data in the database
        /// </summary>
        /// <param name="request">SourceType domain model</param>
        /// <returns>SourceTypeId generated against the record</returns>
        Task<SysSerial> Add(SourceType request);

        /// <summary>
        /// It is used to Get Single SourceType data from database
        /// </summary>
        /// <param name="SourceTypeId">Database record Id</param>
        /// <returns>SourceType record</returns>
        Task<SourceTypeItem> Get(SysSerial SourceTypeSysSerial);

        /// <summary>
        /// It is used to Get All SourceType data from database
        /// </summary>
        /// <returns>List of SourceType records</returns>
        Task<PagedResponse<SourceTypeItem>> GetAll(Pager paging);

        /// <summary>
        /// It is used to update SourceType data from database
        /// </summary>
        /// <param name="SourceTypeSysSerial">Database record Id</param>
        /// <param name="request">SourceType domain model</param>
        /// <returns></returns>
        Task Change(SysSerial SourceTypeSysSerial, SourceType request);

        /// <summary>
        /// It is used to Delete Single SourceType data from database
        /// </summary>
        /// <param name="SourceTypeId">Database record Id</param>
        /// <returns></returns>
        Task Delete(SysSerial SourceTypeSysSerial);

        /// <summary>
        /// It is used to Delete All SourceType data from database
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();
    }
}
