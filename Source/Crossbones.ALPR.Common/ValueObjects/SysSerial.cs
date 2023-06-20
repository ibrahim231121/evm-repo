using Crossbones.Modules.Common.Exceptions;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Common.ValueObjects
{
    [JsonConverter(typeof(ObjectValueToJsonConverter))]
    public struct RecId : IObjectConverter
    {
        readonly long _id;
        public RecId(long id)
        {
            if (id >= 0)
                _id = id;
            else
                throw new ValidationFailed("Id can not be negative");
        }
        public static implicit operator long(RecId obj) => obj._id;
        public override string ToString() => _id.ToString();

        public object ConvertToObject(object value) => new RecId(value != null ? Convert.ToInt64(value.ToString()) : 0);

        public static RecId Empty => new RecId(0);

        public static bool operator ==(RecId obj1, RecId obj2) => obj1._id == obj2._id;
        public static bool operator !=(RecId obj1, RecId obj2) => obj1._id != obj2._id;

        public override bool Equals(object obj)
        {
            if (!(obj is RecId))
                return false;
            else
                return this == (RecId)obj;
        }

        public override int GetHashCode() => _id.GetHashCode();
    }
}