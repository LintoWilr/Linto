using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 坏死爆发 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    private const uint SkillId = 41483u;
    private const uint 不重复Aura = 4399u;
    private const int SkillRange = 25;
    public int Check()
    {
        if (!SMNQt.GetQt("坏死爆发"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (SpellHelper.GetSpell(SkillId).Charges < 1.1)
        {
            return -1;
        }

        if (PvPSMNSettings.Instance.毁绝不重复)
        {
            if (Core.Me.HasLocalPlayerAura(不重复Aura))
            {
                return -1;
            }
        }
        if (PVPHelper.通用距离检查(SkillRange))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(SkillId, SkillRange) == null)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, SkillId, SkillRange);
    }
}
