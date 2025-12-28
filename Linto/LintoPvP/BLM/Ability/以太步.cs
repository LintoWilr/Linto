using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.Ability;

public class 以太步 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("以太步"))
        {
            return -9;
        }
        if (MoveHelper.IsMoving())
        {
            return -10;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (Core.Me.HasAura(3222u))
        {
            return -2;
        }
        if (Core.Resolve<MemApiSpellCastSuccess>().LastAbility == 29660u)
        {
            return -4;
        }
        if (SpellHelper.GetSpell(29660u).Charges > 1.9)
        {
            return 0;
        }
        return -3;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(29660u, SpellTargetType.Self));
    }
}
