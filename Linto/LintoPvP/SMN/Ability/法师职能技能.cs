using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 法师职能技能 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    private const uint 彗星禁止Aura = 4492u;
    private const uint 幻影弹Aura = 4516u;
    private const uint 幻影弹Action = 43291u;
    private const uint 武装削弱Aura = 4494u;
    private const uint 武装削弱Action = 43254u;
    private const int SkillRange = 25;

    public int Check()//43259 职能技能
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!SMNQt.GetQt("职能技能"))
        {
            return -9;
        }
        //彗星 buff:4492
        if (Core.Me.HasLocalPlayerAura(彗星禁止Aura))//不使用彗星
        {
            return -2;
        }
        //幻影弹 buff:4516 action:43291
        if (Core.Me.HasLocalPlayerAura(幻影弹Aura))
        {
            var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(幻影弹Action);
            if (!changedAction.GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            if (PVPHelper.通用距离检查(SkillRange))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(幻影弹Action, SkillRange) == null)
            {
                return -5;
            }
        }
        //武装削弱 buff:4494 action:43254
        if (Core.Me.HasLocalPlayerAura(武装削弱Aura))
        {
            var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(武装削弱Action);
            if (!changedAction.GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            if (PVPHelper.通用距离检查(SkillRange))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(武装削弱Action, SkillRange) == null)
            {
                return -5;
            }
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        if (Core.Me.HasLocalPlayerAura(幻影弹Aura))
        {
            var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(幻影弹Action);
            PVPHelper.通用技能释放(slot, changedAction, SkillRange);
        }
        else  if (Core.Me.HasLocalPlayerAura(武装削弱Aura))
        {
            var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(武装削弱Action);
            PVPHelper.通用技能释放(slot, changedAction, SkillRange);
        }
    }
}
