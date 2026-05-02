using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 速度之星 : MCHSlotResolverBase
{
    protected override string QtKey => "职能技能";
    protected override uint SkillId => 43249u;
    protected override bool UseSharedTargeting => false;
    protected override bool BlockInMarksmanPreAnim => false;
    protected override uint? RequiredAura => 4489u;

    protected override int CheckSpecific()
    {
        if (PVPHelper.check坐骑())
        {
            return -5;
        }
        return 0;
    }
}
public class 勇气 : MCHSlotResolverBase
{
    protected override string QtKey => "职能技能";
    protected override uint SkillId => 43250u;
    protected override bool UseSharedTargeting => false;
    protected override bool BlockInMarksmanPreAnim => false;
    protected override uint? RequiredAura => 4490u;

    protected override int CheckSpecific()
    {
        if (PVPHelper.check坐骑())
        {
            return -5;
        }
        var changedSkill = PVPHelper.MCH.GetChangedAction(29408u);
        if (!(changedSkill == 29405u || changedSkill == 29408u))
        {
            return -5;
        }
        if (!Core.Me.HasAura(3153u) || changedSkill.GetSpell().Charges < 0.5)
        {
            return -2;
        }
        return 0;
    }
}
