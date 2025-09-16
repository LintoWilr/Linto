using AEAssist;
using AEAssist.MemoryApi;

namespace Linto.LintoPvP.PVPApi.PVPApi.Target;

public class SkillDecision
{
    public static uint 技能变化(uint SkillID)
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SkillID);
    }
}