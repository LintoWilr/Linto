using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SMN;

namespace Linto.LintoPvP.GNB.Ability;

public class 刚玉之心 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	
	public int Check()//41443 刚玉之心
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPGNBOverlay.GNBQt.GetQt("刚玉之心"))
		{
			return -9;
		}
		if (!41443u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		bool 刚玉队友 = PvPGNBSettings.Instance.刚玉队友;
		if (刚玉队友)
		{
			if (PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].CurrentHpPercent() > PvPGNBSettings.Instance.刚玉血量/100f 
			    && Core.Me.CurrentHpPercent() > 
			    PvPSMNSettings.Instance.守护之光血量/100f) return -93;
			if (PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].CurrentHpPercent() <= PvPGNBSettings.Instance.刚玉血量/100f &&
			    PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].DistanceToPlayer() > 30)
				return -91;
			IBattleChara member = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= PvPGNBSettings.Instance.刚玉血量/100f);
			if (member == null) return -92;
			if (member.DistanceToPlayer() > 30) return -6;
		}
		else
		{
			if (Core.Me.CurrentHpPercent() > PvPSMNSettings.Instance.守护之光血量/100f) return -10;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		bool 刚玉队友 = PvPGNBSettings.Instance.刚玉队友;
		bool 刚玉播报 = PvPGNBSettings.Instance.刚玉播报;
		IBattleChara target;
		if (刚玉队友)
		{
			if (PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].CurrentHpPercent() <= PvPGNBSettings.Instance.刚玉血量/100f &&
			    PartyHelper.CastableParty.Contains(PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象]))
				target = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象];
			else
				target = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= PvPGNBSettings.Instance.刚玉血量/100f);
			if (target == null)
			{
				return;
			}
		}
		else
		{
			target = Core.Me;
		}
		slot.Add(new Spell(41443u, target));
		if (刚玉播报) LogHelper.Print($"刚玉目标:{target.Name}");
	}
}
