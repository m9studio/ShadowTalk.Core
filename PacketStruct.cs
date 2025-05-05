using Newtonsoft.Json.Linq;
using System.Reflection;

namespace M9Studio.ShadowTalk.Core
{
    public abstract class PacketStruct
    {
        public override string ToString() => ToJObject().ToString();
        public JObject ToJObject()
        {
            JObject jObject = new JObject();

            var fields = GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                jObject[field.Name] = value != null ? JToken.FromObject(value) : JValue.CreateNull();
            }

            return jObject;
        }

        public static T Parse<T>(JObject jObject) where T : PacketStruct, new()
        {
            T instance = new T();
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (!jObject.TryGetValue(field.Name, out var token))
                    throw new InvalidOperationException($"Missing field: {field.Name}");

                object value = token.ToObject(field.FieldType);
                field.SetValue(instance, value);
            }

            return instance;
        }
    }
}
