namespace M9Studio.ShadowTalk.Core
{
    public class PacketClientToServerReconectSRP : PacketStruct
    {
        public string Token;
        public string HMAC;
        public string A;
    }
}
