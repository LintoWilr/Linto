
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 烈牙连击 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()//29102 烈牙
    {
        if (!PvPGNBOverlay.GNBQt.GetQt("烈牙连击"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(29102).GetSpell().IsReadyWithCanCast())
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(5))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(Core.Resolve<MemApiSpell>().CheckActionChange(29102), 5) == null)
        {
            return -5;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, Core.Resolve<MemApiSpell>().CheckActionChange(29102), 5);
    }
}
