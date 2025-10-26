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
        if (Core.Me.HasLocalPlayerAura(4492u))//不使用彗星
        {
            return -2;
        }
        //幻影弹 buff:4516 action:43291
        if (Core.Me.HasLocalPlayerAura(4516u))
        {
            if (!Core.Resolve<MemApiSpell>().CheckActionChange(43291u).GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            if (PVPHelper.通用距离检查(25))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(43291u, 25) == null)
            {
                return -5;
            }
        }
        //武装削弱 buff:4494 action:43254
        if (Core.Me.HasLocalPlayerAura(4494u))
        {
            if (!Core.Resolve<MemApiSpell>().CheckActionChange(43254u).GetSpell().IsReadyWithCanCast())
            {
                return -2;
            }
            if (PVPHelper.通用距离检查(25))
            {
                return -5;
            }
            if (PVPHelper.通用技能释放Check(43254u, 25) == null)
            {
                return -5;
            }
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        if (Core.Me.HasLocalPlayerAura(4516u))
        {
            PVPHelper.通用技能释放(slot, Core.Resolve<MemApiSpell>().CheckActionChange(43291u), 25);
        }
        else  if (Core.Me.HasLocalPlayerAura(4494u))
        {
            PVPHelper.通用技能释放(slot, Core.Resolve<MemApiSpell>().CheckActionChange(43254u), 25);
        }
    }
}
