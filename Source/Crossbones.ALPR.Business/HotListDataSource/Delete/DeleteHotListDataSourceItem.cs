using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotListDataSource.Delete
{
    public class DeleteHotListDataSourceItem : SysSerialItemMessage
    {
        public DeleteHotListDataSourceItem(SysSerial id) : base(id)
        {

        }
    }
}
