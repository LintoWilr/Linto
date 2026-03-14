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
	private const uint SkillId = 29670u;
	private const float MaxCastDistance = 30f;

	private static float 获取守护阈值()
	{
		return PvPSMNSettings.Instance.守护之光血量 / 100f;
	}

	public int Check()
	{
		bool 守护队友 = PvPSMNSettings.Instance.守护队友;
		float hpThreshold = 获取守护阈值();
		if (!SMNQt.GetQt("守护之光")) return -3;
		if (!PVPHelper.CanActive()) return -1;
		if (!SkillId.GetSpell().IsReadyWithCanCast()) return -2;
		if (守护队友)
		{
			if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() > hpThreshold 
			    && Core.Me.CurrentHpPercent() > 
			    hpThreshold) return -93;
			if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() <= hpThreshold &&
			    PartyHelper.Party[PvPSMNSettings.Instance.守护对象].DistanceToPlayer() > MaxCastDistance)
				return -91;
			IBattleChara? member = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= hpThreshold);
			if (member == null) return -92;
			if (member.DistanceToPlayer() > MaxCastDistance) return -6;
		}
		else
		{
			if (Core.Me.CurrentHpPercent() > hpThreshold) return -10;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		bool 守护队友 = PvPSMNSettings.Instance.守护队友;
		bool 守护播报 = PvPSMNSettings.Instance.守护播报;
		float hpThreshold = 获取守护阈值();
		IBattleChara? target = null;
		if (守护队友)
		{
			if (PartyHelper.Party[PvPSMNSettings.Instance.守护对象].CurrentHpPercent() <= hpThreshold &&
			    PartyHelper.CastableParty.Contains(PartyHelper.Party[PvPSMNSettings.Instance.守护对象]))
				target = PartyHelper.Party[PvPSMNSettings.Instance.守护对象];
			else
				target = PartyHelper.CastableParty.FirstOrDefault(chara => chara.CurrentHpPercent() <= hpThreshold);
			if (target == null)
			{
				return;
			}
		}
		else
		{
			target = Core.Me;
		}
		slot.Add(new Spell(SkillId, target));
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
