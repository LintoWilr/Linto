using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.Ability;

public class 默者的夜曲 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!PvPBRDOverlay.BRDQt.GetQt("沉默"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!29395u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (PVPHelper.通用距离检查(15))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29395u, 15) == null)
        {
            return -6;
        }
        if (PvPSettings.Instance.技能自动选中)
        {
            if (PvPSettings.Instance.最合适目标 &&
                (PVPTargetHelper.TargetSelector.Get最合适目标(15 + PvPSettings.Instance.长臂猿, 29395u) != null &&
                 PVPTargetHelper.TargetSelector.Get最合适目标(15 + PvPSettings.Instance.长臂猿, 29395u) != Core.Me))
            {
                if (PVPTargetHelper.Check目标免控(PVPTargetHelper.TargetSelector.Get最合适目标(15 + PvPSettings.Instance.长臂猿, 29395u)))
                {
                    return -3;
                }
            }

            if ((PVPTargetHelper.TargetSelector.Get最近目标() != null &&
                 PVPTargetHelper.TargetSelector.Get最近目标() != Core.Me))
            {
                if (PVPTargetHelper.Check目标免控(PVPTargetHelper.TargetSelector.Get最近目标()))
                {
                    return -3;
                }
            }
        }
        if (!PvPSettings.Instance.技能自动选中)
        {
            if (PVPTargetHelper.Check目标免控(Core.Me.GetCurrTarget()))
            {
                return -3;
            }
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29395u, 15);
    }
}
