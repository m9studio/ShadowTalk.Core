using M9Studio.SecureStream;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Numerics;

namespace M9Studio.ShadowTalk.Core
{
    public static class Modifier
    {
        public static string ReceiveString<TAddress>(this SecureSession<TAddress> session) => Encoding.UTF8.GetString(session.Receive());
        public static JObject ReceiveJObject<TAddress>(this SecureSession<TAddress> session) => JObject.Parse(session.ReceiveString());
        public static bool Send<TAddress>(this SecureSession<TAddress> session, string text) => session.Send(Encoding.UTF8.GetBytes(text));
        public static bool Send<TAddress>(this SecureSession<TAddress> session, JObject jObject) => session.Send(jObject.ToString());
        public static bool Send<TAddress>(this SecureSession<TAddress> session, PacketStruct @struct) => session.Send(@struct.ToJObject());
        public static byte[] ToByteArrayUnsigned(this BigInteger value)
        {
            byte[] bytes = value.ToByteArray();
            if (bytes[^1] == 0) // Удаляем знак, если есть
            {
                Array.Resize(ref bytes, bytes.Length - 1);
            }
            return bytes;
        }
    }
}
