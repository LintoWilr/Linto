using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 象式浮空炮塔 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!PvPMCHOverlay.MCHQt.GetQt("浮空炮"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!29412u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (GCDHelper.GetGCDCooldown() < 200)
        {
            return -3;
        }
        if (PVPHelper.通用技能释放Check(29412, 25) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29412, 25);
    }
}
