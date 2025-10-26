using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 深红强袭 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {

        if (!SMNQt.GetQt("深红强袭"))
        {
            return -233;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() != 29667u)
        {
            return -5;
        }
        if (!(29668u).GetSpell().IsReadyWithCanCast())
        {
            return -6;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(41483u, 25) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, SkillDecision.技能变化(29668u), 25);
    }
}
