using System.Reflection;
using Nso.Core;
using Nso.Protocol;
using Microsoft.Extensions.DependencyInjection;

namespace Nso.Server.Messaging;

internal sealed class CommandRouter
{
    private readonly Dictionary<(Cmd, sbyte?), ICommandHandler> _map = new();

    public CommandRouter(IServiceProvider serviceProvider, params Assembly[] scan)
    {
        var assemblies = scan.Length == 0
            ? new[] { Assembly.GetExecutingAssembly() }
            : scan;

        foreach (var type in assemblies.SelectMany(a => a.GetTypes()))
        {
            var attrs = type.GetCustomAttributes<CmdHandlerAttribute>();
            if (!attrs.Any()) continue;

            if (!typeof(ICommandHandler).IsAssignableFrom(type))
                throw new InvalidOperationException($"{type.Name} must implement ICommandHandler");

            // Resolve instance từ DI container
            if (serviceProvider.GetService(type) is not ICommandHandler inst)
                throw new InvalidOperationException($"Handler '{type.Name}' chưa được đăng ký vào DI container!");

            foreach (var attr in attrs)
            {
                var key = (attr.CmdId, attr.SubCmdId);
                if (_map.ContainsKey(key))
                    throw new InvalidOperationException($"Duplicate handler for ({key.CmdId}, {key.SubCmdId})");

                _map[key] = inst;
            }
        }
    }

    public async Task DispatchAsync(ServerSession s, Message m)
    {
        sbyte? sub = null;

        if (m.Data.Length > 0)
        {
            sbyte candidate = unchecked((sbyte)m.Data[0]);

            if (_map.ContainsKey(((Cmd)m.Cmd, candidate)))
                sub = candidate;
        }

        if (_map.TryGetValue(((Cmd)m.Cmd, sub), out var handler))
        {
            await handler.HandleAsync(s, m);
        }
        else
        {
            Log.W($"[Router] No handler for cmd {(int)m.Cmd}, sub {sub}");
        }
    }
}
