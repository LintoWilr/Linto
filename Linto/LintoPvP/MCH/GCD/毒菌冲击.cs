using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 毒菌冲击 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!PvPMCHOverlay.MCHQt.GetQt("毒菌冲击"))
        {
            return -233;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (PVPHelper.通用距离检查(12))
        {
            return -5;
        }
        if (GCDHelper.GetGCDCooldown() > 200)
        {
            return -3;
        }
        if (PVPHelper.通用技能释放Check(29406, 12) == null)
        {
            return -6;
        }
        if (!Core.Me.HasAura(3151))
        {
            return -9;
        }
        if (PvPMCHSettings.Instance.毒菌分析)
        {
            if (!Core.Me.HasAura(3158))
            {
                if (SpellHelper.GetSpell(29414u).Charges > 0.5)
                {
                    return -23;
                }
            }
        }
        if (!29406u.GetSpell().IsReadyWithCanCast() || Core.Resolve<MemApiSpell>().CheckActionChange(29406u) != 29406u)
        {
            return -2;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29406u, 12);
    }
}