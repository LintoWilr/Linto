using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 野火 : MCHSlotResolverBase
{
    protected override string QtKey => "野火";
    protected override uint SkillId => 29409u;
    protected override int SkillRange => 12;

    protected override int CheckSpecific()
    {
        if (PvPMCHSettings.Instance.过热野火 && !Core.Me.HasAura(3149u))
        {
            return -9;
        }
        return 0;
    }
}
