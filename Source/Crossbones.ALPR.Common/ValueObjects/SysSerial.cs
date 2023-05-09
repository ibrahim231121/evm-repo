using Crossbones.Modules.Common.Exceptions;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Common.ValueObjects
{
    [JsonConverter(typeof(ObjectValueToJsonConverter))]
    public struct SysSerial : IObjectConverter
    {
        readonly long _id;
        public SysSerial(long id)
        {
            if (id >= 0)
                _id = id;
            else
                throw new ValidationFailed("Id can not be negative");
        }
        public static implicit operator long(SysSerial obj) => obj._id;
        public override string ToString() => _id.ToString();

        public object ConvertToObject(object value) => new SysSerial(value != null ? Convert.ToInt64(value.ToString()) : 0);

        public static SysSerial Empty => new SysSerial(0);

        public static bool operator ==(SysSerial obj1, SysSerial obj2) => obj1._id == obj2._id;
        public static bool operator !=(SysSerial obj1, SysSerial obj2) => obj1._id != obj2._id;

        public override bool Equals(object obj)
        {
            if (!(obj is SysSerial))
                return false;
            else
                return this == (SysSerial)obj;
        }

        public override int GetHashCode() => _id.GetHashCode();
    }
}
