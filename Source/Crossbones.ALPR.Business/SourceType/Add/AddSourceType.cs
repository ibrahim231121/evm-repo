using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.SourceType.Add
{
    public class AddSourceType : RecIdItemMessage
    {
        public AddSourceType(RecId recId) : base(recId)
        {

        }

        public string SourceTypeName { get; set; }
        public string Description { get; set; }

    }
}
