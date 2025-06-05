namespace M9Studio.ShadowTalk.Core
{
    public class PacketServerToClientSendMessages : PacketStruct
    {
        //TODO messages[{id:"", uuid:"", key:"", text:""}..]
        public string[] UUIDs;
        public int[] Users;
        public int[] Dates;
        public string[] Texts;
    }
}