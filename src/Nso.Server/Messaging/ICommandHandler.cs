
using Nso.Core.Net;
using Nso.Protocol;
using Nso.Server.Core;

namespace Nso.Server.Messaging;

public interface ICommandHandler
{
    Task HandleAsync(ServerSession session, Message m);
}
