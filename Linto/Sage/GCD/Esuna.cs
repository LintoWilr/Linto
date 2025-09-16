using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Linto.Sage.GCD;

public class Esuna: ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    
    public int Check()
    {
        if (Core.Me.IsMoving())
        {
            return -1;
        }
        if (PartyHelper.CastableParty.Count<8)
        {
            return -1;
        }
        if (Core.Resolve<MemApiBuff>().GetStack(PartyHelper.CastableParty[SGESettings.Instance.Esuna-1],200)>=SGESettings.Instance.stack)
        {
            return 1;
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Esuna, PartyHelper.CastableParty[SGESettings.Instance.Esuna-1]));
    }
    
}