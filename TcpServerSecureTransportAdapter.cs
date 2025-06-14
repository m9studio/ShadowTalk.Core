﻿using System.Net;
using System.Net.Sockets;

namespace M9Studio.ShadowTalk.Core
{
    public class TcpServerSecureTransportAdapter : TcpSecureTransportAdapter
    {
        private Dictionary<IPEndPoint, Socket> sockets = new();
        public TcpServerSecureTransportAdapter(Socket socket) : base(socket) => Task.Run(WaitNewConnections);
        private async Task WaitNewConnections()
        {
            while (true)
            {
                Socket newSocket = await _socket.AcceptAsync();
                IPEndPoint iPEndPoint = (IPEndPoint)newSocket.RemoteEndPoint;
                sockets.Add(iPEndPoint, newSocket);
                RaiseConnected(iPEndPoint);
            }
        }
        public void Disconect(IPEndPoint address)
        {
            if (sockets.ContainsKey(address))
            {
                sockets[address].Close();
                sockets.Remove(address);
                RaiseDisconect(address);
            }
        }
        public override byte[] ReceiveFrom(IPEndPoint address) => ReceiveFrom(sockets[address]);
        public override bool SendTo(byte[] buffer, IPEndPoint address) => SendTo(buffer, sockets[address]);
    }
}
