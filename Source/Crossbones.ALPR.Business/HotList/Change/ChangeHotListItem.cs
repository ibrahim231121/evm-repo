
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using System.Collections.Generic;
using System.Text;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItem : SysSerialItemMessage
    {

        public ChangeHotListItem(SysSerial id) : base(id)
        {

        }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string ReplyTo { get; set; }
        //public Credential Credential { get; set; }
        //public Server Server { get; set; }
        //public History History { get; set; }
    }
}
