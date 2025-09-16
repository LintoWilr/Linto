using System.Numerics;
using System.Security;
using CombatRoutine;
using CombatRoutine.View.Other;
using Common;
using Common.Define;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.GCD;

public class 炽天召唤 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if(!SCHQt.GetQt("炽天召唤"))
		{
			return -3;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if(!Core.Me.InCombat)
		{
			return -10;
		}
		if (!SpellHelper.IsReady(29237u))
		{
			return -4;
		}
		if (PVPHelper.LB槽() == 3000)
		{
			return 1;
		}
		return -1;
	}
	public void Build(Slot slot)
	{
		slot.Add(new Spell(29237u,Core.Me.Pos));
	}
}
