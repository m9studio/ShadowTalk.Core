using M9Studio.SecureStream;
using M9Studio.UdpLikeTcp;
using System.Net;

namespace M9Studio.ShadowTalk.Core
{
    public class UdpLikeTcpSecureTransportAdapter : ISecureTransportAdapter<IPEndPoint>
    {
        public Socket _socket = new();
        Dictionary<IPEndPoint, int> iPEndPoints = new();

        public event Action<IPEndPoint> OnConnected;
        public event Action<IPEndPoint> OnDisconnected;

        public UdpLikeTcpSecureTransportAdapter() => _socket.OnConnected += Connected;

        private void Connected(IPEndPoint sender)
        {
            iPEndPoints.Add(sender, 5);
            OnConnected?.Invoke(sender);
        }

        public byte[] ReceiveFrom(IPEndPoint address) => _socket.ReceiveFrom(address);
        public bool SendTo(byte[] buffer, IPEndPoint address)
        {
            bool _ = _socket.SendTo(address, buffer);
            if (_)
            {
                iPEndPoints[address]= 5;
            }
            else
            {
                iPEndPoints[address]--;
            }
            if(iPEndPoints[address] <= 0)
            {
                OnDisconnected?.Invoke(address);
                iPEndPoints.Remove(address);
            }
            return _;
        }
    }
}
