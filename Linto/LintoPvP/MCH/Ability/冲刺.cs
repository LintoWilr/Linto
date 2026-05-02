using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 冲刺 : MCHSlotResolverBase
{
    protected override string QtKey => "冲刺";
    protected override uint SkillId => 29057u;
    protected override bool UseSharedTargeting => false;
    protected override bool BlockInMarksmanPreAnim => false;
    protected override bool RequireReady => false;

    protected override int CheckSpecific()
    {
        if (Core.Me.HasAura(1342u))
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() != 0)
        {
            return -4;
        }
        if (Core.Resolve<MemApiSpellCastSuccess>().LastSpell == 29057u)
        {
            return -99;
        }
        return 0;
    }
}
