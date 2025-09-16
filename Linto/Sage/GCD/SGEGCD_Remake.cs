using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

namespace Linto.Sage.GCD;

public class SGEGCDRemake : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!SpellsDefine.Swiftcast.GetSpell().IsReadyWithCanCast()) return -3;
        if (!Qt.GetQt("拉人")) return -2;
        if (Core.Me.CurrentMp < 2400) return -2;
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        if(skillTarget==null)return -1;
        if(!skillTarget.IsValid()) return -1;
        return 1;
    }

    public void Build(Slot slot)
    {
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(AurasDefine.Raise));
        slot.Add(SpellsDefine.Swiftcast.GetSpell());
        slot.Add(new Spell(SpellsDefine.Egeiro, skillTarget));
    }
}