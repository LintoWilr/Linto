#region

using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

#endregion

namespace Linto.Monk.GCD;

public class MNKGCD_PerfectBalance : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (Core.Me.HasMyAura(AurasDefine.PerfectBalance))
        {
            return 1;  //震脚走震脚逻辑
        }
        return -1;
    }

    public void Build(Slot slot)
    {
        var spell = MNKPerfectBalanceSpellHelper.GetPerfectBalanceSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}