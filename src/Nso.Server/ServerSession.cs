using System.Net.Sockets;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Nso.Protocol;
using Nso.Server.Messaging;

namespace Nso.Server.Core;

public class ServerSession
{
    private readonly TcpClient _client;
    private readonly IServiceProvider _provider;
    private readonly CommandRouter _router;
    private readonly SessionRegistry _sessionRegistry;

    public Guid SessionId { get; private set; }
    public int? UserId { get; private set; }

    public ServerSession(TcpClient client, IServiceProvider provider)
    {
        _client = client;
        _provider = provider;
        _router = _provider.GetRequiredService<CommandRouter>();
        _sessionRegistry = _provider.GetRequiredService<SessionRegistry>();
        _ = Task.Run(RunAsync);
    }

    private async Task RunAsync()
    {
        var stream = _client.GetStream();

        while (_client.Connected)
        {
            var msg = await Message.ReadAsync(stream);
            if (msg == null) break;

            if (UserId.HasValue && !await _sessionRegistry.IsValidAsync(UserId.Value, SessionId))
            {
                Console.WriteLine($"[KICK] Session {SessionId} expired for user {UserId}");
                _client.Close();
                return;
            }

            await _router.RouteAsync(this, msg);
        }
    }

    public void Send(Message msg)
    {
        var stream = _client.GetStream();
        var bytes = msg.ToBytes();
        stream.Write(bytes, 0, bytes.Length);
    }

    public void AttachSession(int userId, Guid sessionId)
    {
        UserId = userId;
        SessionId = sessionId;
    }

    public string Id => _client.Client.RemoteEndPoint?.ToString() ?? Guid.NewGuid().ToString();
}
