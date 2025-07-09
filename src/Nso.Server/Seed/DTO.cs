using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nso.Server.Seed;

public class SkillOptionTemplateSeed
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class NClassSeed
{
    public int ClassId { get; set; }
    public string Name { get; set; } = default!;
    public List<SkillTemplateSeed> SkillTemplate { get; set; } = new();
}

public class SkillTemplateSeed
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int MaxPoint { get; set; }
    public int Type { get; set; }
    public int IconId { get; set; }
    public string Description { get; set; } = default!;
    public List<SkillSeed> Skill { get; set; } = new();
}

public class SkillSeed
{
    public int SkillId { get; set; }
    public int Point { get; set; }
    public int Level { get; set; }
    public int ManaUse { get; set; }
    public int CoolDown { get; set; }
    public int Dx { get; set; }
    public int Dy { get; set; }
    public int MaxFight { get; set; }
    public List<SkillOptionSeed> Options { get; set; } = new();
}

public class SkillOptionSeed
{
    public int Param { get; set; }
    public OptionTemplateSeed OptionTemplate { get; set; } = default!;
}

public class OptionTemplateSeed
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
