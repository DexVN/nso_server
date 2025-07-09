namespace Nso.Server.Models;

/// <summary>Thông tin thiết bị/ứng dụng do client gửi ở gói -29/-125.</summary>
public sealed class ClientInfo
{
    public byte ClientType { get; set; }
    public byte ZoomLevel { get; set; }
    public bool IsGprs { get; set; }
    public bool IsQwerty { get; set; }
    public bool IsTouch { get; set; }

    public int ScreenWidth { get; set; }
    public int ScreenHeight { get; set; }

    public string Platform { get; set; } = "";
    public int VersionIp { get; set; }

    public byte LanguageId { get; set; }
    public int UserProvider { get; set; }
    public string Agent { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
