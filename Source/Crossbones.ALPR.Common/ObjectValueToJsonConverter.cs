using Newtonsoft.Json;

namespace Crossbones.ALPR.Common
{
    public interface IObjectConverter
    {
        object ConvertToObject(object value);
    }

    public class ObjectValueToJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (existingValue)
            {
                case IObjectConverter converter:
                    return converter.ConvertToObject(reader.Value);

                default:
                    {
                        var GetInterfaceMap = objectType.GetInterfaceMap(typeof(IObjectConverter));
                        var methodInfo = GetInterfaceMap.TargetMethods.Find(array => array.Name == nameof(IObjectConverter.ConvertToObject)).First();
                        return methodInfo.Invoke(Activator.CreateInstance(GetInterfaceMap.TargetType), new object[] { reader.Value });
                    }
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
    }
}
