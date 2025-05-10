namespace M9Studio.ShadowTalk.Core
{
    public class PacketServerToClientSendMessages : PacketStruct
    {
        //TODO messages[{id:"", uuid:"", key:"", text:""}..]
        public string[] UUIDs;
        public int[] Ids;
        public string[] Keys;
        public string[] Texts;
    }
}