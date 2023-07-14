using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Corssbones.ALPR.Business.HotListDataSource.Mapping
{
    public class GetHotListDataSourceMappingDetails : RecIdItemMessage
    {
        public GetHotListDataSourceMappingDetails(DTO.HotListDataSourceDTO hotListDataSourceItem)
            : base(new RecId(hotListDataSourceItem.RecId)) => DataSourceItem = hotListDataSourceItem;

        public DTO.HotListDataSourceDTO DataSourceItem { get; set; }
    }

}
