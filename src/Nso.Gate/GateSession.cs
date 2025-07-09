using System.Net.Sockets;

using Nso.Core;
using Nso.Core.Net;
using Nso.Protocol;

namespace Nso.Gate;

sealed class GateSession : Session
{
    private const int KEY_LEN = 32;

    public GateSession(TcpClient c) : base(c) =>
        Log.I($"New client {c.Client.RemoteEndPoint}");

    protected override async Task HandleAsync(Message m)
    {
        switch ((Cmd)m.Cmd)
        {
            case Cmd.HANDSHAKE:
                // phát sinh khoá ngẫu nhiên & gửi cho client
                var rnd = new Random();
                Key = new byte[KEY_LEN];
                rnd.NextBytes(Key);

                var payload = new byte[KEY_LEN + 1];
                payload[0] = (byte)KEY_LEN;
                Array.Copy(Key, 0, payload, 1, KEY_LEN);

                await SendAsync(new Message((sbyte)Cmd.HANDSHAKE, payload));
                CipherOn = true;
                Log.I("Handshake done – cipher ON");
                break;

            case Cmd.PING:
                await SendAsync(new Message((sbyte)Cmd.PING, Array.Empty<byte>()));
                break;

            case Cmd.LOGIN_INFO:
                // Đọc username / pass trong m.Data nếu bạn muốn
                Log.I("Recv LOGIN_INFO – giả lập thành công");
                // Trả về mã thành công (-29) hoặc gì đó tuỳ client
                await SendAsync(new Message((sbyte)Cmd.LOGIN_INFO,
                           System.Text.Encoding.UTF8.GetBytes("OK")));
                break;

            default:
                Log.W($"Unknown cmd {(int)m.Cmd}");
                break;
        }
    }
}
