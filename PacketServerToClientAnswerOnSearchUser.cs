namespace M9Studio.ShadowTalk.Core
{
    public class PacketServerToClientAnswerOnSearchUser : PacketStruct
    {
        //TODO Users[{Id:"",Name:""},..]
        public string[] Names;
        public string[] RSAs;
        public int[] Ids;
        public bool[] Online;
    }
}
