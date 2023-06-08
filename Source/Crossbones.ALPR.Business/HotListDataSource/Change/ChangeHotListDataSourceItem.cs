
using Corssbones.ALPR.Business;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.Items;
using System.Collections.Generic;
using System.Text;

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
