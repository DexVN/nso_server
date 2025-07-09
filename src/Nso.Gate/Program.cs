using System.Net;
using System.Net.Sockets;

using Nso.Core;
using Nso.Gate;

const int PORT = 14444;                 // client NSO cho phép tuỳ chỉnh port

var listener = new TcpListener(IPAddress.Any, PORT);
listener.Start();
Log.I($"Gate is listening on 0.0.0.0:{PORT}");

while (true)
{
    var c = await listener.AcceptTcpClientAsync();
    _ = new GateSession(c);
}
