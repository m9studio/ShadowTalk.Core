using M9Studio.SecureStream;
using Newtonsoft.Json.Linq;
using System.Text;

namespace M9Studio.ShadowTalk.Core
{
    public static class Modifier
    {
        public static string ReceiveString<TAddress>(this SecureSession<TAddress> session) => Encoding.UTF8.GetString(session.Receive());
        public static JObject ReceiveJObject<TAddress>(this SecureSession<TAddress> session) => JObject.Parse(session.ReceiveString());
        public static bool Send<TAddress>(this SecureSession<TAddress> session, string text) => session.Send(Encoding.UTF8.GetBytes(text));
        public static bool Send<TAddress>(this SecureSession<TAddress> session, JObject jObject) => session.Send(jObject.ToString());
    }
}
