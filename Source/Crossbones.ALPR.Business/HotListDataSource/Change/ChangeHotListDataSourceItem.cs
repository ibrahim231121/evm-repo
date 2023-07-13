
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.HotListDataSource.Change
{
    public class ChangeHotListDataSourceItem : RecIdItemMessage
    {

        public ChangeHotListDataSourceItem(RecId id) : base(id)
        {

        }

        public DTO.HotListDataSourceDTO Item { get; set; }

    }
}
