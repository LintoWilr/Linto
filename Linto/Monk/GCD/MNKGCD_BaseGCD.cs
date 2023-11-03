#region

using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

#endregion

namespace Linto.Monk.GCD;

public class MNKGCD_BaseGCD : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (Core.Me.HasMyAura(AurasDefine.PerfectBalance))
        {
            return -1;  //震脚走震脚逻辑
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = MNKSpellHelper.GetBaseGCDSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}