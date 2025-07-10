using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Nso.Server.Core;

namespace Nso.Server.Messaging;

public class CommandRouter
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<byte, Type> _handlers = new();

    public CommandRouter(IServiceProvider provider, Assembly assembly)
    {
        _provider = provider;
        foreach (var type in assembly.GetTypes())
        {
            if (!typeof(ICommandHandler).IsAssignableFrom(type)) continue;

            var attr = type.GetCustomAttribute<CmdHandlerAttribute>();
            if (attr != null)
                _handlers[attr.Cmd] = type;
        }
    }

    public async Task RouteAsync(ServerSession session, Message msg)
    {
        if (_handlers.TryGetValue(msg.Cmd, out var type))
        {
            var handler = (ICommandHandler)_provider.GetRequiredService(type);
            await handler.HandleAsync(session, msg);
        }
        else
        {
            Console.WriteLine($"[WARN] No handler for Cmd = {msg.Cmd}");
        }
    }
}
