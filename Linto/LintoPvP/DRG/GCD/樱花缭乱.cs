using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.GCD;

public class 樱花缭乱 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check() //29490 樱花缭乱
    {
        if (!DRGQt.GetQt("樱花缭乱"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (!DRGSkillID.樱花缭乱.GetSpell().IsReadyWithCanCast())
        {
            return -9;
        }
        if (GCDHelper.GetGCDCooldown() > 600)
        {
            return -3;
        }
        if (PvPDRGSettings.Instance.樱花缭乱龙血)
        {
            if (!Core.Me.HasAura(3177))
            {
                return -8;
            }
        }
        if (PVPHelper.通用距离检查(DRGSkillID.樱花缭乱距离))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(DRGSkillID.樱花缭乱, DRGSkillID.樱花缭乱距离) == null)
        {
            return -6;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, DRGSkillID.樱花缭乱, DRGSkillID.樱花缭乱距离);
    }
}
