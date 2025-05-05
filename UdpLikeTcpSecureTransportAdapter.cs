using M9Studio.SecureStream;
using M9Studio.UdpLikeTcp;
using System.Net;

namespace M9Studio.ShadowTalk.Core
{
    public class UdpLikeTcpSecureTransportAdapter : ISecureTransportAdapter<IPEndPoint>
    {
        private Socket _socket = new();
        List<IPEndPoint> iPEndPoints = new();

        public event Action<IPEndPoint> OnConnected;
        public event Action<IPEndPoint> OnDisconnected;

        public UdpLikeTcpSecureTransportAdapter() => _socket.OnPacketReceived += PacketReceived;

        private void PacketReceived(IPEndPoint sender, byte[] data)
        {
            if (iPEndPoints.Contains(sender))
            {
                return;
            }
            iPEndPoints.Add(sender);
            OnConnected?.Invoke(sender);
        }

        public byte[] ReceiveFrom(IPEndPoint address) => _socket.ReceiveFrom(address);
        public bool SendTo(byte[] buffer, IPEndPoint address) => _socket.SendTo(address, buffer);
    }
}
