using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nso.Server.Models;

public class Character
{
    [Key]
    public int Id { get; set; }
    public short SlotIndex { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public short Gender { get; set; }
    public string ClassName { get; set; } = null!;
    public short Level { get; set; }
    public short PartHead { get; set; }
    public short PartWeapon { get; set; }
    public short PartBody { get; set; }
    public short PartLeg { get; set; }

    [ForeignKey("Account")]
    public int AccountId { get; set; }
    public Account Account { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
}
