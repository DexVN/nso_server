// src/Nso.Server/Utils/BinaryExt.cs
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace Nso.Server.Utils;          // giữ nguyên namespace

/// <summary>
/// Extension methods đọc/ghi big-endian & UTF kiểu DataOutputStream.
/// </summary>
internal static class BinaryExt
{
    /* ========== READ ========== */

    public static ushort ReadUInt16BE(this BinaryReader br)
        => (ushort)((br.ReadByte() << 8) | br.ReadByte());

    public static int ReadInt32BE(this BinaryReader br)
    {
        Span<byte> buf = stackalloc byte[4];
        br.Read(buf);
        return BinaryPrimitives.ReadInt32BigEndian(buf);
    }

    public static string ReadUtfBE(this BinaryReader br)
    {
        ushort len = br.ReadUInt16BE();
        byte[] bytes = br.ReadBytes(len);
        return Encoding.UTF8.GetString(bytes);
    }

    /* ========== WRITE ========== */

    public static void WriteUInt16BE(this BinaryWriter bw, ushort v)
    {
        bw.Write((byte)(v >> 8));
        bw.Write((byte)v);
    }

    public static void WriteInt32BE(this BinaryWriter bw, int v)
    {
        Span<byte> buf = stackalloc byte[4];
        BinaryPrimitives.WriteInt32BigEndian(buf, v);
        bw.Write(buf);
    }

    public static void WriteUtfBE(this BinaryWriter bw, string s)
    {
        var bytes = Encoding.UTF8.GetBytes(s);
        bw.WriteUInt16BE((ushort)bytes.Length);
        bw.Write(bytes);
    }
}
