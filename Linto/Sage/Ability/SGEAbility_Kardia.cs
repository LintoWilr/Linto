using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace Linto.Sage.Ability;

public class SGEAbility_Kardia : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!Qt.GetQt("自动心关")) return -2;
        if (SGESettings.Instance.关心最低血量T) return -3;
        if (!SpellsDefine.Kardia.GetSpell().IsReadyWithCanCast()) return -1;
        if (Core.Me.GetCurrTargetsTarget().HasMyAuraWithTimeleft(AurasDefine.Kardion)) return -3;
        if (!Core.Me.GetCurrTargetsTarget().IsTank()) return -4;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Kardia, Core.Me.GetCurrTargetsTarget()));
    }
}
public class SGEAbility_Kardia2 : ISlotResolver
{
    public static IBattleChara Get心关T()
    {
        if (PartyHelper.CastableTanks.Count <= 0) 
            return Core.Me;
        IBattleChara[] partyMembers = 
        {
            PartyHelper.CastableTanks[1], 
            PartyHelper.CastableTanks[0],
        };

        foreach (var member in partyMembers)
        {
            if (PartyHelper.CastableTanks.Count<= 0)return Core.Me;
            if (member.CurrentHpPercent()<=SGESettings.Instance.最低血量T)
            {
                if(PartyHelper.CastableTanks[1].CurrentHpPercent()<=PartyHelper.CastableTanks[0].CurrentHpPercent())
                    return PartyHelper.CastableTanks[1];
                return PartyHelper.CastableTanks[0];
            }
        }
        return Core.Me.GetCurrTargetsTarget();
    }
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!Qt.GetQt("自动心关")) return -2;
        if (!SGESettings.Instance.关心最低血量T) return -3;
        if (!SpellsDefine.Kardia.GetSpell().IsReadyWithCanCast()) return -1;
        if (Get心关T().HasMyAuraWithTimeleft(AurasDefine.Kardia)) return -3;
      //  if (!Core.Me.GetCurrTargetsTarget().IsTank()) return -4;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Kardia, Get心关T()));
    }
}