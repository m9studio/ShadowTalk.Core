using M9Studio.SecureStream;
using System.Net;
using System.Net.Sockets;

namespace M9Studio.ShadowTalk.Core
{
    public class TcpSecureTransportAdapter : ISecureTransportAdapter<IPEndPoint>
    {
        protected Socket _socket;
        public event Action<IPEndPoint>? OnConnected;
        public event Action<IPEndPoint>? OnDisconnected;

        public TcpSecureTransportAdapter(Socket socket) => _socket = socket;
        public virtual byte[] ReceiveFrom(IPEndPoint address) => ReceiveFrom(_socket);
        public virtual bool SendTo(byte[] buffer, IPEndPoint address) => SendTo(buffer, _socket);
        protected void RaiseConnected(IPEndPoint endpoint) => OnConnected?.Invoke(endpoint);


        protected byte[] ReceiveFrom(Socket socket)
        {
            byte[] sizeBuffer = new byte[4];

            int received = socket.Receive(sizeBuffer);
            if (received != 4)
                throw new Exception("Не удалось получить размер сообщения");

            // Преобразуем размер из Big Endian(Network Byte Order) в UInt32
            uint size = (uint)(sizeBuffer[0] << 24 |
                               sizeBuffer[1] << 16 |
                               sizeBuffer[2] << 8 |
                               sizeBuffer[3]);
            byte[] buffer = new byte[size];
            int totalReceived = 0;

            while (totalReceived < size)
            {
                int bytes = socket.Receive(buffer, totalReceived, (int)size - totalReceived, SocketFlags.None);
                if (bytes == 0) // Если соединение закрылось
                {
                    OnDisconnected?.Invoke((IPEndPoint)socket.LocalEndPoint);
                    throw new Exception("Соединение было закрыто");
                }
                totalReceived += bytes;
            }
            return buffer;
        }

        protected bool SendTo(byte[] buffer, Socket socket)
        {
            byte[] size = BitConverter.GetBytes((uint)buffer.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(size);
            byte[] message = new byte[size.Length + buffer.Length];
            Buffer.BlockCopy(size, 0, message, 0, size.Length);
            Buffer.BlockCopy(buffer, 0, message, size.Length, buffer.Length);

            try
            {
                socket.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
