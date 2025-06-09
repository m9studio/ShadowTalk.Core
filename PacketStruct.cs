using Newtonsoft.Json.Linq;
using System.Reflection;

namespace M9Studio.ShadowTalk.Core
{
    public abstract class PacketStruct
    {
        public override string ToString() => ToJObject().ToString();
        public JObject ToJObject()
        {
            JObject jObject = new JObject
            {
                ["struct"] = GetType().Name
            };

            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                jObject[field.Name] = value != null ? JToken.FromObject(value) : JValue.CreateNull();
            }

            return jObject;
        }

        public static T Parse<T>(JObject jObject) where T : PacketStruct, new()
        {
            string expectedStruct = typeof(T).Name;
            string actualStruct = jObject["struct"]?.ToString();

            if (actualStruct != expectedStruct)
                throw new InvalidOperationException($"Expected struct '{expectedStruct}', but got '{actualStruct}'.");



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
    
    
        public static T TryParse<T>(JObject jObject) where T : PacketStruct, new()
        {
            try
            {
                return Parse<T>(jObject);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
