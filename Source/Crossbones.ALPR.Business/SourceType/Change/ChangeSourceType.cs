using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.SourceType.Change
{
    public class ChangeSourceType : SysSerialItemMessage
    {

        public ChangeSourceType(SysSerial id) : base(id)
        {

        }
        public string SourceTypeName { get; set; }
        public string Description { get; set; }
    }
}
