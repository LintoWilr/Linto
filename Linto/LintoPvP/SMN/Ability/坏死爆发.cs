using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 坏死爆发 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
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
        if (SpellHelper.GetSpell(41483u).Charges < 1.1)
        {
            return -1;
        }

        if (PvPSMNSettings.Instance.毁绝不重复)
        {
            if (Core.Me.HasLocalPlayerAura(4399u))
            {
                return -1;
            }
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
        PVPHelper.通用技能释放(slot, 41483u, 25);
    }
}
