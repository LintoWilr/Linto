using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability;

public class 跑快快 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;

	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!SCHQt.GetQt("跑快快"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(Core.Me.HasAura(3094u))
		{ 
			return -10;
		}
		var 队友数量 = PartyHelper.CastableAlliesWithin15.Count;
		if (!SpellHelper.IsReady(29236u))
		{
			return -2;
		}
		if(队友数量 >= PvPSCHSettings.Instance.跑快快队友数量)
		{
			if (SpellHelper.GetSpell(29233u).Cooldown.TotalMilliseconds < 4000)
			{
				return 1;
			}
		}
		return -2;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29236,SpellTargetType.Self));
	}
}
