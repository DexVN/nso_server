namespace Nso.Protocol;

public record Message(sbyte Cmd, byte[] Data);
