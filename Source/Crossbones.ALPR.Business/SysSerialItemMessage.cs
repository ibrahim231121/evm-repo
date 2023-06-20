using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business
{
    public class RecIdItemMessage : MessageBase
    {
        public RecId Id { get; }
        public RecIdItemMessage(RecId _id) => Id = _id;
    }
}