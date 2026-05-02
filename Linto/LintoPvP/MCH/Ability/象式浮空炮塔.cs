using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 象式浮空炮塔 : MCHSlotResolverBase
{
    protected override string QtKey => "浮空炮";
    protected override uint SkillId => 29412u;
    protected override int SkillRange => 25;
    protected override int? MaxGcdCooldownMs => null;

    protected override int CheckSpecific()
    {
        if (GCDHelper.GetGCDCooldown() < 200)
        {
            return -3;
        }

        return 0;
    }
}
