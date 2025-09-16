using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MNK.GCD;

public class 基础连2 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if(!MNKQt.GetQt("军体拳"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
		{
			return -5;
		}
		if (!SpellHelper.IsReady(29473u))
		{
			return -2;
		}
		if (Core.Get<IMemApiSpell>().GetLastComboSpellId() != 29472)
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29473u));
	}
}
