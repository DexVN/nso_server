namespace Nso.Protocol;

public static class PacketCodec
{
    public static async Task<Message?> ReadAsync(Stream s,
                                             byte[] key,
                                             bool ciphered,
                                             CancellationToken ct)
    {
        int cur = 0;
        byte Decode(byte b) => ciphered ? (byte)(b ^ key[cur++ % key.Length]) : b;

        // --- 1. Đọc CMD ---
        byte[] cmdBuf = new byte[1];
        int read = await s.ReadAsync(cmdBuf, 0, 1, ct);
        if (read == 0) return null;
        sbyte cmd = (sbyte)Decode(cmdBuf[0]);

        // --- 2. Đọc LENGTH ---
        byte[] lenBuf = new byte[2];
        int totalRead = 0;
        while (totalRead < 2)
        {
            int readLen = await s.ReadAsync(lenBuf, totalRead, 2 - totalRead, ct);
            if (readLen == 0) return null; // Stream closed
            totalRead += readLen;
        }
        ushort len = (ushort)((Decode(lenBuf[0]) << 8) | Decode(lenBuf[1]));

        // --- 3. Đọc PAYLOAD ---
        byte[] payload = new byte[len];
        totalRead = 0;
        while (totalRead < len)
        {
            int readPayload = await s.ReadAsync(payload, totalRead, len - totalRead, ct);
            if (readPayload == 0) return null; // Stream closed
            totalRead += readPayload;
        }
        for (int i = 0; i < payload.Length; i++)
            payload[i] = Decode(payload[i]);

        return new Message(cmd, payload);
    }



    // Encode + ghi ra stream
    public static async Task WriteAsync(Stream s,
                                    Message msg,
                                    byte[] key,
                                    bool ciphered,
                                    CancellationToken ct)
    {
        int cur = 0;
        byte Encode(byte b) => ciphered ? (byte)(b ^ key[cur++ % key.Length]) : b;

        // --- 1. CMD ---
        await s.WriteAsync(new[] { Encode((byte)msg.Cmd) }, ct);

        // --- 2. Length ---
        ushort len = (ushort)(msg.Data?.Length ?? 0);
        await s.WriteAsync(new[] {
        Encode((byte)(len >> 8)),
        Encode((byte)len)
    }, ct);

        // --- 3. Payload ---
        if (msg.Data != null && msg.Data.Length > 0)
        {
            byte[] encoded = new byte[msg.Data.Length];
            for (int i = 0; i < msg.Data.Length; i++)
                encoded[i] = Encode(msg.Data[i]);

            await s.WriteAsync(encoded, ct);
        }
    }
}
