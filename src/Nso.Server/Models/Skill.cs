using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nso.Server.Models;

public class Skill
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // 👈 Tắt auto-increment
    public int Id { get; set; }

    public int Point { get; set; }
    public int Level { get; set; }
    public int ManaUse { get; set; }
    public int CoolDown { get; set; }
    public int Dx { get; set; }
    public int Dy { get; set; }
    public int MaxFight { get; set; }

    [ForeignKey("SkillTemplate")]
    public int SkillTemplateId { get; set; }
    public SkillTemplate SkillTemplate { get; set; } = default!;

    public List<SkillOption> SkillOptions { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
