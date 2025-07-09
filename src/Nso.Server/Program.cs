using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Nso.Core;
using Nso.Server.Models;
using Nso.Server.Messaging;

namespace Nso.Server;

static class Program
{
    const int PORT = 14444;

    static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: false);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString));

                // ✅ Scan & register all ICommandHandler with DI
                services.Scan(scan => scan
                    .FromAssemblyOf<ICommandHandler>()
                    .AddClasses(c => c.AssignableTo<ICommandHandler>())
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime());

                // ✅ Register CommandRouter (singleton)
                services.AddSingleton(provider =>
                    new CommandRouter(provider, typeof(ICommandHandler).Assembly));
            })
            .Build();

        // 1. Seed DB
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            DbSeeder.Seed(db);
        }

        // 2. Listen TCP
        var tcp = new TcpListener(IPAddress.Any, PORT);
        tcp.Start();
        Log.I($"✅ Server listening on 0.0.0.0:{PORT}");

        // 3. Accept clients
        while (true)
        {
            var client = await tcp.AcceptTcpClientAsync();
            var scope = host.Services.CreateScope();

            // ✅ Pass IServiceProvider to ServerSession (so it can resolve CommandRouter + handlers)
            _ = new ServerSession(client, scope.ServiceProvider);
        }
    }
}
