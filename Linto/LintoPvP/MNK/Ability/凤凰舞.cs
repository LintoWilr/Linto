using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MNK.Ability;

public class 凤凰舞 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!MNKQt.GetQt("凤凰舞"))
		{
			return -9;
		}
		if ((double)SpellHelper.GetSpell(29481u).Charges < 1) 
		{
			return -2;
		}
		if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
		{
			return -5;
		}
		if (!SpellHelper.IsReady(29478u))
		{
			return -2;
		}
		if (Core.Get<IMemApiSpell>().GetLastComboSpellId() != 29477)
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29481u));
	}
}
