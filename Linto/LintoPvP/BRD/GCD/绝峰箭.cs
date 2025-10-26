using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.GCD;

public class 绝峰箭 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!PvPBRDOverlay.BRDQt.GetQt("绝峰箭"))
        {
            return -233;
        }
        if (!29393u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 600)
        {
            return -3;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29393u, 25) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29393u, 25);
    }
}