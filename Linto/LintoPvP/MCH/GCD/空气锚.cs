using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 空气锚 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!PvPMCHOverlay.MCHQt.GetQt("空气锚"))
        {
            return -233;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (GCDHelper.GetGCDCooldown() > 200)
        {
            return -3;
        }
        if (PVPHelper.通用技能释放Check(29407u, 25) == null)
        {
            return -6;
        }
        if (!Core.Me.HasAura(3152u))
        {
            return -2;
        }
        if (!29407u.GetSpell().IsReadyWithCanCast() || Core.Resolve<MemApiSpell>().CheckActionChange(29407u) != 29407u)
        {
            return -2;
        }
        if (PvPMCHSettings.Instance.空气锚分析)
        {
            if (!Core.Me.HasAura(3158))
            {
                if (SpellHelper.GetSpell(29414u).Charges > 0.5)
                {
                    return -23;
                }
            }
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29407u, 25);
    }
}