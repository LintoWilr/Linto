using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 霰弹枪 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PvPMCHOverlay.MCHQt.GetQt("霰弹枪"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!29404u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 200)
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(12))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29404, 12) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29404u, 12);
    }
}
