using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using CombatRoutine;
using Common;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.GCD;

public class LB : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if(!SMNQt.GetQt("灵魂共鸣"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.LB槽() == 2000)
		{
			return 1;
		}
		if(Core.Me.HasAura(3169u))
		{
			return 0;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29662u));
	}
}
