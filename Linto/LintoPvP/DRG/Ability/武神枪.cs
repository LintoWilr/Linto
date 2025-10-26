using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 武神枪 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()//29491 武神枪 
    {
        if (!DRGQt.GetQt("武神枪"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!29491u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (PVPHelper.通用距离检查(15))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(DRGSkillID.武神枪, DRGSkillID.武神枪距离) == null)
        {
            return -6;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, DRGSkillID.武神枪, DRGSkillID.武神枪距离);
    }
}