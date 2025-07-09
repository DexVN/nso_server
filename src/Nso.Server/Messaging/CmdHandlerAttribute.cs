using Nso.Protocol;

namespace Nso.Server.Messaging;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class CmdHandlerAttribute : Attribute
{
    public Cmd CmdId { get; }
    public sbyte? SubCmdId { get; }   // null = không có sub-cmd

    public CmdHandlerAttribute(Cmd cmdId) { CmdId = cmdId; }
    public CmdHandlerAttribute(Cmd cmdId, sbyte subCmd)
    {
        CmdId = cmdId; SubCmdId = subCmd;
    }
}
