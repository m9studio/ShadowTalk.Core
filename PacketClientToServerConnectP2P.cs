namespace M9Studio.ShadowTalk.Core
{
    /// <summary>
    /// Запрос клиента, чтобы к нему подключился другой клиент
    /// </summary>
    public class PacketClientToServerConnectP2P : PacketStruct
    {
        public int UserId;
        public string Key;
    }
}
