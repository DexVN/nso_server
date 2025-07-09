using System.Net.Sockets;

using Microsoft.Extensions.DependencyInjection;

using Nso.Core;
using Nso.Core.Net;
using Nso.Protocol;
using Nso.Server.Messaging;
using Nso.Server.Models;

public sealed class ServerSession : Session
{
    private readonly IServiceProvider _provider;
    private readonly TcpClient _client;
    private readonly CommandRouter _router;
    private readonly AppDbContext _db;

    public bool IsAuthenticated { get; set; }
    public ClientInfo ClientInfo { get; } = new();

    public ServerSession(TcpClient client, IServiceProvider provider) : base(client)
    {
        _client = client;
        _provider = provider;

        _router = _provider.GetRequiredService<CommandRouter>();
        _db = _provider.GetRequiredService<AppDbContext>();

        Log.I($"[+] New conn {_client.Client.RemoteEndPoint}");
    }

    protected override Task HandleAsync(Message msg)
    {
        var cmd = (Cmd)msg.Cmd;

        // luôn xử lý handshake (-27)
        if (cmd == Cmd.GET_SESSION_ID)
            return _router.DispatchAsync(this, msg);

        // chặn gameplay nếu chưa login
        if (!IsAuthenticated && cmd != Cmd.NOT_LOGIN)
            return Task.CompletedTask;

        return _router.DispatchAsync(this, msg);
    }

    internal void ActivateCipher(byte[] key)
    {
        Key = key;
        Cipher = true;
    }
}
