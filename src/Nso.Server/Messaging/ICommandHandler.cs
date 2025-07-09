
using Nso.Core.Net;
using Nso.Protocol;

namespace Nso.Server.Messaging;

public interface ICommandHandler
{
    Task HandleAsync(ServerSession session, Message m);
}
