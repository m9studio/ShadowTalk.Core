namespace M9Studio.ShadowTalk.Core
{
    /// <summary>
    /// Ответ клиенту, который запросил соединение с другим клиентом
    /// </summary>
    public class PacketServerToClientAnswerOnConnectP2P : PacketStruct
    {
        public string Key;
        public string Ip;
        public int Port;
    }
}
