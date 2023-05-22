
using Corssbones.ALPR.Business.HotList;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItem : HotListItemMessage
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
