using System.ComponentModel.DataAnnotations;

namespace Nso.Server.Models;

public class DataVersion
{
    [Key]
    [MaxLength(50)]
    public string Key { get; set; } = null!;

    public int Version { get; set; } = 1;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
