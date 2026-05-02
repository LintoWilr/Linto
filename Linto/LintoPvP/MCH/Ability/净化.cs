using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 净化 : MCHSlotResolverBase
{
    protected override string QtKey => "自动净化";
    protected override uint SkillId => 29056u;
    protected override bool UseSharedTargeting => false;
    protected override bool BlockInMarksmanPreAnim => false;

    protected override int CheckSpecific()
    {
        if (PVPHelper.净化判断())
        {
            return 1;
        }
        return -3;
    }
}
