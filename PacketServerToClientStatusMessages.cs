namespace M9Studio.ShadowTalk.Core
{
    public class PacketServerToClientStatusMessages : PacketStruct
    {
        //TODO messages[{uuid:"", check:""}..]
        public string[] UUIDs;
        public int[] Checks;

        public enum CheckType
        {
            DELETED = -1,
            AWAITING = 0,
            VIEWED = 1
        }
    }
}
