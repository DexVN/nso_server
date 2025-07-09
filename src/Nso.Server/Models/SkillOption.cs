using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nso.Server.Models;
public class SkillOption
{
    [Key, Column(Order = 0)]
    public int SkillId { get; set; }

    [Key, Column(Order = 1)]
    public int SkillOptionTemplateId { get; set; }

    public int Param { get; set; }

    [ForeignKey("SkillId")]
    public Skill Skill { get; set; } = default!;

    [ForeignKey("SkillOptionTemplateId")]
    public SkillOptionTemplate SkillOptionTemplate { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
