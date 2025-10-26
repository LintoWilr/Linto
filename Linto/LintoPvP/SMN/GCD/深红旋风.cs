using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 深红旋风 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!SMNQt.GetQt("深红旋风"))
        {
            return -233;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (PVPHelper.通用距离检查(PvPSMNSettings.Instance.火神冲))
        {
            return -5;
        }
        if (!(29667u).GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, SkillDecision.技能变化(29667u), PvPSMNSettings.Instance.火神冲);
    }
}
