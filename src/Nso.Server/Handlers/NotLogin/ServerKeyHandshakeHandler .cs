// Handlers/Handshake/ServerKeyHandshakeHandler.cs
using Nso.Core;
using Nso.Protocol;
using Nso.Server.Messaging;

namespace Nso.Server.Handlers;

[CmdHandler(Cmd.GET_SESSION_ID /* -27 */)]
public sealed class ServerKeyHandshakeHandler : ICommandHandler
{
    private const int KEY_LEN = 1;          // đúng mẫu Java (key = ‘D’)

    public async Task HandleAsync(ServerSession s, Message _)
    {
        // 1) Tạo key "D" và đóng gói như Java
        byte[] rawKey = { (byte)'D' };      // 0x44
        var payload = new byte[1 + KEY_LEN];
        payload[0] = (byte)KEY_LEN;         // keyLen
        payload[1] = rawKey[0];             // key[0] (ko cần XOR dồn vì 1 byte)

        // 2) Gửi gói -27 CHƯA XOR
        await s.SendAsync(new Message((sbyte)Cmd.GET_SESSION_ID, payload));

        // 3) Lưu & bật XOR
        s.ActivateCipher(rawKey);

        Log.I("[Handshake] Sent server key, XOR ON");
    }
}
