using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nso.Server.Models;

public class SkillTemplate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // 👈 Tắt auto-increment
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    public int MaxPoint { get; set; }
    public int Type { get; set; }
    public int IconId { get; set; }

    public string Description { get; set; } = default!;

    [ForeignKey("NClass")]
    public int NClassId { get; set; }
    public NClass NClass { get; set; } = default!;

    public List<Skill> Skills { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
