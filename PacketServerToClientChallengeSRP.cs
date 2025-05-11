namespace M9Studio.ShadowTalk.Core
{
    public class PacketServerToClientChallengeSRP : PacketStruct
    {
        public int Id;
        public string Name;
        public string Salt;
        public string B;
    }
}
