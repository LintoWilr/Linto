using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 蓄力冲击 : MCHSlotResolverBase
{
    protected override string QtKey => "蓄力冲击";
    protected override uint SkillId => 29402u;
    protected override int SkillRange => 25;
    protected override int? MaxGcdCooldownMs => 50;

    protected override int CheckSpecific()
    {
        if (Core.Me.HasAura(3149u))
        {
            return -9;
        }
        return 0;
    }
}
public class 热冲击 : MCHSlotResolverBase
{
    protected override string QtKey => "野火";
    protected override uint SkillId => 烈焰弹id();
    protected override uint BuildSkillId => 烈焰弹id();
    protected override int SkillRange => 25;
    protected override int? MaxGcdCooldownMs => 200;

    private const uint 热冲击技能 = 29403u;
    private const uint 烈焰弹技能 = 41468u;

    public static uint 烈焰弹id()
    {
        if (PvPMCHSettings.Instance.热冲击)
            return 热冲击技能;
        else return 烈焰弹技能;
    }

    protected override int CheckSpecific()
    {
        if (!Core.Me.HasAura(3149u))
        {
            return -9;
        }
        return 0;
    }
}
