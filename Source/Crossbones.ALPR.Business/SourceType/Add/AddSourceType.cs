using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.SourceType.Add
{
    public class AddSourceType : SysSerialItemMessage
    {
        public AddSourceType(SysSerial id) : base(id)
        {

        }

        public string SourceTypeName { get; set; }
        public string Description { get; set; }

    }
}
