using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.GCD;

public class 天星棱光 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public int Check()
    {
        if (!PvPPCTOverlay.PCTQt.GetQt("天星棱光"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
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
        if (PVPHelper.通用技能释放Check(PCTSkillID.技能天星棱光, 25) == null)
        {
            return -6;
        }
        if (!PCTSkillID.技能天星棱光.GetSpell().IsReadyWithCanCast())
        {
            return -3;
        }
        return 6;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, PCTSkillID.技能天星棱光, 25);
    }
}
