using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping.Service
{
    public interface IHotListDataSourceMappingService
    {
        Task<RecId> ExecuteMappingForSingleDataSource(long HotlistSysSerial);

        Task<List<DTO.HotListDataSourceDTO>> ExecuteMappingForAllDataSources();
    }
}
