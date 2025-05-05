using M9Studio.SecureStream;
using M9Studio.UdpLikeTcp;
using System.Net;

namespace M9Studio.ShadowTalk.Core
{
    public class UdpLikeTcpSecureTransportAdapter : ISecureTransportAdapter<IPEndPoint>
    {
        private Socket _socket = new Socket();

        public event Action<IPEndPoint> OnConnected;
        public event Action<IPEndPoint> OnDisconnected;

        public byte[] ReceiveFrom(IPEndPoint address)
        {
            return _socket.ReceiveFrom(address);
        }
        public bool SendTo(byte[] buffer, IPEndPoint address)
        {
            return _socket.SendTo(address, buffer);
        }
    }
}
