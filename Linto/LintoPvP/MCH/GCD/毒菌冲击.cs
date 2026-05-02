using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 毒菌冲击 : MCHSlotResolverBase
{
    protected override string QtKey => "毒菌冲击";
    protected override uint SkillId => 29406u;
    protected override int SkillRange => 12;
    protected override int? MaxGcdCooldownMs => 200;
    protected override uint? RequiredAura => 3151u;

    protected override int CheckSpecific()
    {
        if (PvPMCHSettings.Instance.毒菌分析)
        {
            if (!Core.Me.HasAura(3158u))
            {
                if (SpellHelper.GetSpell(29414u).Charges > 0.5)
                {
                    return -23;
                }
            }
        }
        if (PVPHelper.MCH.GetChangedAction(SkillId) != SkillId)
        {
            return -2;
        }
        return 0;
    }
}
