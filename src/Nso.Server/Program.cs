using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

using Nso.Server.Messaging;
using Nso.Server.Models;
using Nso.Server.Core;

namespace Nso.Server;

public static class Program
{
    public const int PORT = 14444;

    public static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: false);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

                // Register Redis
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

                // Register Command Handlers
                services.Scan(scan => scan
                    .FromAssemblyOf<ICommandHandler>()
                    .AddClasses(c => c.AssignableTo<ICommandHandler>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime());

                services.AddSingleton<CommandRouter>();
                
            })
            .Build();

        // Seed database
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        var tcp = new TcpListener(IPAddress.Any, PORT);
        tcp.Start();
        Console.WriteLine($"✅ Server listening on port {PORT}");

        while (true)
        {
            var client = await tcp.AcceptTcpClientAsync();
            var scope = host.Services.CreateScope();
            _ = new ServerSession(client, scope.ServiceProvider);
        }
    }
}
