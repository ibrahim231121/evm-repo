using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItem : RecIdItemMessage
    {
        public AddHotListDataSourceItem(RecId id) : base(id)
        {
        }

        public DTO.HotListDataSourceDTO Item { get; set; }

    }
}
