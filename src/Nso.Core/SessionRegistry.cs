using StackExchange.Redis;

namespace Nso.Server.Core;

public class SessionRegistry
{
    private readonly IDatabase _db;

    public SessionRegistry(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task RegisterAsync(int userId, Guid sessionId, string connectionId)
    {
        string key = $"session:user:{userId}";
        string value = $"{sessionId}|{connectionId}";
        await _db.StringSetAsync(key, value, TimeSpan.FromHours(1));
    }

    public async Task<bool> IsValidAsync(int userId, Guid sessionId)
    {
        string key = $"session:user:{userId}";
        var data = await _db.StringGetAsync(key);
        if (!data.HasValue) return false;

        var parts = data.ToString().Split('|');
        return Guid.TryParse(parts[0], out var stored) && stored == sessionId;
    }

    public async Task<string?> GetConnectionIdAsync(int userId)
    {
        var data = await _db.StringGetAsync($"session:user:{userId}");
        if (!data.HasValue) return null;
        return data.ToString().Split('|')[1];
    }
}
