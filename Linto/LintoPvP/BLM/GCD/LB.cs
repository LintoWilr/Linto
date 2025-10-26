using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.GCD;

public class LB : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("灵魂共鸣"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (MoveHelper.IsMoving())
        {
            return -3;
        }
        if (Core.Me.LimitBreakCurrentValue() != 2000)
        {
            return -2;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(29662u));
    }
}
public class 异言 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("异言"))
        {
            return -1;
        }
        if (GCDHelper.GetGCDCooldown() > 600)
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (SpellHelper.GetSpell(29658).Charges < 1)
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29658, 25) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29658u, 25);
    }
}
public class 耀星 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("耀星"))
        {
            return -1;
        }
        if (GCDHelper.GetGCDCooldown() > 1200)
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -66;
        }
        if (MoveHelper.IsMoving())
        {
            return -10;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(41480, 25) == null)
        {
            return -6;
        }
        if (!Core.Me.HasAura(4317))
            return -88;
        if (Core.Me.HasAura(3214) || Core.Me.HasAura(3213) || Core.Me.HasAura(3212))
            return 11;
        return -77;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 41480u, 25);
    }
}