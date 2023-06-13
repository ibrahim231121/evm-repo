
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotListDataSource.Change
{
    public class ChangeHotListDataSourceItem : SysSerialItemMessage
    {

        public ChangeHotListDataSourceItem(SysSerial id) : base(id)
        {

        }

        public HotListDataSourceItem Item { get; set; }

    }
}
