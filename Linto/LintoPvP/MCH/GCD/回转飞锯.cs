using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 回转飞锯 : MCHSlotResolverBase
{
    protected override string QtKey => "回转飞锯";
    protected override uint SkillId => 29408u;
    protected override int SkillRange => 25;
    protected override int? MaxGcdCooldownMs => 200;
    protected override uint? RequiredAura => 3153u;

    public static uint 机工变化() => PVPHelper.MCH.GetChangedAction(29408u);

    protected override int CheckSpecific()
    {
        var changedSkill = 机工变化();
        if (changedSkill != SkillId)
        {
            return -2;
        }
        if (PvPMCHSettings.Instance.回转飞锯分析 && !Core.Me.HasAura(3158u))
        {
            if (SpellHelper.GetSpell(29414u).Charges > 0.5)
            {
                return -23;
            }
        }
        return 0;
    }
}
