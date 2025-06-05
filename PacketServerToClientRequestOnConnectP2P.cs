namespace M9Studio.ShadowTalk.Core
{
    /// <summary>
    /// Отправка пакета, о новом подключение пользователю к которому хотят подключиться
    /// </summary>
    public class PacketServerToClientRequestOnConnectP2P : PacketStruct
    {
        public int UserId;
        public string UserName;
        public string Key;
        public string Ip;
        public string RSA;
        public int Port;
    }
}
