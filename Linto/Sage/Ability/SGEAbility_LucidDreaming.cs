using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;


namespace Linto.Sage.Ability;

public class SGEAbility_LucidDreaming : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!SpellsDefine.LucidDreaming.GetSpell().IsReadyWithCanCast())
            return -1;
        if (Core.Me.CurrentMp > 8000) return -2;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.LucidDreaming.GetSpell());
    }
}