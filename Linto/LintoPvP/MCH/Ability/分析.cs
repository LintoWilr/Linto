using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 分析 : MCHSlotResolverBase
{
    protected override string QtKey => "分析";
    protected override uint SkillId => 29414u;
    protected override int SkillRange => 25;
    protected override bool UseSharedTargeting => false;

    protected override int CheckSpecific()
    {
        if (SpellHelper.GetSpell(SkillId).Charges < 1)
        {
            return -1;
        }
        if (PvPMCHSettings.Instance.分析可用)
        {
            if (!29405u.GetSpell().IsReadyWithCanCast())
            {
                return -8;
            }
        }
        if (Core.Me.HasAura(3158u))
        {
            return -99;
        }
        if (Core.Resolve<MemApiSpellCastSuccess>().LastSpell == SkillId)
        {
            return -99;
        }
        if (!PVPHelper.MCH.CanUseTargetedSkill(SkillId, SkillRange))
        {
            return -1;
        }
        return 0;
    }

    protected override void BuildSpecific(Slot slot) => slot.Add(PVPHelper.等服务器Spell(SkillId, Core.Me));
}
