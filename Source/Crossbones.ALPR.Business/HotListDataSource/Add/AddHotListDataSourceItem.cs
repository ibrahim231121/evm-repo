using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotListDataSource.Add
{
    public class AddHotListDataSourceItem : SysSerialItemMessage
    {
        public AddHotListDataSourceItem(SysSerial id) : base(id)
        {
        }

        public HotListDataSourceItem Item { get; set; }

    }
}
