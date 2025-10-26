using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 全金属爆发 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!PvPMCHOverlay.MCHQt.GetQt("全金属爆发"))
        {
            return -233;
        }
        if (!41469u.GetSpell().IsReadyWithCanCast())
        {
            return -5;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (GCDHelper.GetGCDCooldown() > 200)
        {
            return -3;
        }
        if (PVPHelper.通用技能释放Check(41469u, 25) == null)
        {
            return -6;
        }

        if (PvPMCHSettings.Instance.金属爆发仅野火)
        {
            if (PVPTargetHelper.TargetSelector.Get野火目标() == null)
                return -9;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 41469u, 25);
    }
}