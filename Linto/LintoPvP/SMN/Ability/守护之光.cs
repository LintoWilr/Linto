using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 守护之光 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        bool 守护队友 = PvPSMNSettings.Instance.守护队友;
        if (!SMNQt.GetQt("守护之光")) return -3;
        if (!PVPHelper.CanActive()) return -1;
        if (!29670u.GetSpell().IsReadyWithCanCast()) return -2;
        if (守护队友)
        {
            if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() > PvPSMNSettings.Instance.守护之光血量 / 100f
                && Core.Me.CurrentHpPercent() >
                PvPSMNSettings.Instance.守护之光血量 / 100f) return -93;
            if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() <= PvPSMNSettings.Instance.守护之光血量 / 100f &&
                PartyHelper.Party[PvPSMNSettings.Instance.守护对象].DistanceToPlayer() > 30)
                return -91;
            IBattleChara member = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= PvPSMNSettings.Instance.守护之光血量 / 100f);
            if (member == null) return -92;
            if (member.DistanceToPlayer() > 30) return -6;
        }
        else
        {
            if (Core.Me.CurrentHpPercent() > PvPSMNSettings.Instance.守护之光血量 / 100f) return -10;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        bool 守护队友 = PvPSMNSettings.Instance.守护队友;
        bool 守护播报 = PvPSMNSettings.Instance.守护播报;
        IBattleChara target;
        if (守护队友)
        {
            if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() <= PvPSMNSettings.Instance.守护之光血量 / 100f &&
                PartyHelper.CastableParty.Contains(PartyHelper.Party[PvPSMNSettings.Instance.守护对象]))
                target = PartyHelper.Party[PvPSMNSettings.Instance.守护对象];
            else
                target = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= PvPSMNSettings.Instance.守护之光血量 / 100f);
            if (target == null)
            {
                return;
            }
        }
        else
        {
            target = Core.Me;
        }
        slot.Add(new Spell(29670u, target));
        if (守护播报) LogHelper.Print($"守护目标:{target.Name}");
    }
    /*public int Check()
	{
		
		if (!SMNQt.GetQt("守护之光"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!29670u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (Core.Me.CurrentHp <= PvPSMNSettings.Instance.守护之光血量 / 100f)
		{
			return 0;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29670u,SpellTargetType.Self));
	}*/
}
