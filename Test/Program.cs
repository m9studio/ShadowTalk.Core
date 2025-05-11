using Newtonsoft.Json.Linq;

namespace M9Studio.ShadowTalk.Core.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestPacketStruct test = new TestPacketStruct();
            test.Name = "TestName";
            test.Value = "12345";
            Console.WriteLine(test.ToString());
            JObject obj = test.ToJObject();

            obj["demo"] = 12345;

            //obj["struct"] = 12345;

            PacketStruct test2 = PacketStruct.Parse<TestPacketStruct>(obj);
            if (test2 != null)
            {
                Console.WriteLine(test2.ToString());
                Console.WriteLine(((TestPacketStruct)test2).ToString());

            }

        }
    }
}
