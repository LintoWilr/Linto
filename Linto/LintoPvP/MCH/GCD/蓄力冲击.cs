using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 蓄力冲击 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!PvPMCHOverlay.MCHQt.GetQt("蓄力冲击"))
        {
            return -233;
        }
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(29402u).GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 50)
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29402u, 25) == null)
        {
            return -6;
        }
        if (Core.Me.HasAura(3149u))
        {
            return -9;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29402u, 25);
    }
}
public class 热冲击 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public uint 烈焰弹id()
    {
        if (PvPMCHSettings.Instance.热冲击)
            return 29403u;
        else return 41468u;
    }
    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!PvPMCHOverlay.MCHQt.GetQt("野火"))
        {
            return -233;
        }
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(烈焰弹id()).GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 200)
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(烈焰弹id(), 25) == null)
        {
            return -6;
        }
        if (!Core.Me.HasAura(3149u))
        {
            return -9;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 烈焰弹id(), 25);
    }
}
