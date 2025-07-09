using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nso.Server.Models;

public class NClass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // 👈 Tắt auto-increment
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    public List<SkillTemplate> SkillTemplates { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
