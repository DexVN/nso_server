using System.Net.Sockets;

using Nso.Protocol;

namespace Nso.Core.Net;

public abstract class Session
{
    /*───────────── SOCKET & XOR ─────────────*/
    protected readonly TcpClient Client;
    protected readonly NetworkStream Stream;

    protected internal byte[] Key = Array.Empty<byte>();
    protected internal bool Cipher = false;

    /*────────────── CONSTRUCT ───────────────*/
    protected Session(TcpClient c)
    {
        Client = c;
        Stream = c.GetStream();
        _ = Task.Run(Loop);
    }

    /*────────────── MAIN LOOP ───────────────*/
    private async Task Loop()
    {
        try
        {
            while (true)
            {
                var msg = await PacketCodec.ReadAsync(Stream, Key, Cipher, default);
                if (msg is null) break;
                await HandleAsync(msg);
            }
        }
        catch (Exception ex) { Log.E(ex.ToString()); }
        finally { Client.Close(); }
    }

    /*──────────────── SEND API ───────────────*/

    /// <summary>Gửi sẵn <paramref name="msg"/> (đã có payload hoàn chỉnh).</summary>
    public Task SendAsync(Message msg) =>
        PacketCodec.WriteAsync(Stream, msg, Key, Cipher, default);

    /// <summary>
    /// Gửi command không có sub-cmd (payload <paramref name="data"/> thuần).
    /// </summary>
    public Task SendAsync(Cmd cmd, ReadOnlySpan<byte> data = default)
    {
        var msg = new Message((sbyte)cmd, data.ToArray());
        return SendAsync(msg);
    }

    /// <summary>
    /// Gửi command có <paramref name="sub">sub-cmd</paramref> ở byte 0
    /// kèm <paramref name="extra"/> payload phía sau.
    /// </summary>
    public Task SendAsync(Cmd cmd, sbyte sub, ReadOnlySpan<byte> extra = default)
    {
        byte[] payload = new byte[1 + extra.Length];
        payload[0] = unchecked((byte)sub);
        extra.CopyTo(payload.AsSpan(1));
        var msg = new Message((sbyte)cmd, payload);
        return SendAsync(msg);
    }

    /*────────────── ABSTRACT ───────────────*/
    protected abstract Task HandleAsync(Message m);

    /*────────────── HELPERS ───────────────*/
    protected void Close() => Client.Close();
}
