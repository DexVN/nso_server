using System.Net.Sockets;
using System.Text;

namespace Nso.Server.Messaging;

public class Message
{
    public byte Cmd { get; set; }
    public byte Sub { get; set; }
    public MemoryStream Payload { get; } = new();

    public static async Task<Message?> ReadAsync(NetworkStream stream)
    {
        var header = new byte[2];
        if (await stream.ReadAsync(header.AsMemory()) != 2) return null;

        int length = header[0] << 8 | header[1];
        var buffer = new byte[length];
        int read = await stream.ReadAsync(buffer.AsMemory());
        if (read != length) return null;

        var cmd = buffer[0];
        var sub = buffer[1];

        var message = new Message { Cmd = cmd, Sub = sub };
        message.Payload.Write(buffer, 2, length - 2);
        message.Payload.Position = 0;

        return message;
    }

    public byte[] ToBytes()
    {
        var body = Payload.ToArray();
        var data = new byte[2 + body.Length + 2];
        data[0] = (byte)((body.Length + 2) >> 8);
        data[1] = (byte)((body.Length + 2) & 0xFF);
        data[2] = Cmd;
        data[3] = Sub;
        Buffer.BlockCopy(body, 0, data, 4, body.Length);
        return data;
    }

    public void WriteString(string s)
    {
        var bytes = Encoding.UTF8.GetBytes(s);
        Payload.WriteByte((byte)bytes.Length);
        Payload.Write(bytes, 0, bytes.Length);
    }

    // Other WriteX() / ReadX() helpers...
}
