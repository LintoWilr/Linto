using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.GCD;

public class 极炎法 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if(!SCHQt.GetQt("极炎法"))
		{
			return -9;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.距离大于25米远程范围())
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29231));
	}
}