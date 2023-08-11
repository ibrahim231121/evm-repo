using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping.Service
{
    public interface IHotListDataSourceMappingService
    {
        Task<RecId> ExecuteMappingForSingleDataSource(long recId);

        Task<List<DTO.HotListDataSourceDTO>> ExecuteMappingForAllDataSources();

        Task<object> EnqueJobSingleDataSource(long recId);

        Task EnqueJobAllDataSources();
    }
}
