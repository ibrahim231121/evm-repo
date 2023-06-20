
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotListDataSource.Change
{
    public class ChangeHotListDataSourceItem : RecIdItemMessage
    {

        public ChangeHotListDataSourceItem(RecId id) : base(id)
        {

        }

        public HotListDataSourceDTO Item { get; set; }

    }
}
