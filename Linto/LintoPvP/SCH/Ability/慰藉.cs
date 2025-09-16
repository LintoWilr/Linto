using CombatRoutine;
using Common;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability
{
	public class 慰藉 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;


		public int Check()
		{
			if(!PVPHelper.通用权限())
			{
				return -999999;
			}
			if (!SCHQt.GetQt("炽天召唤"))
			{
				return -9;
			}
			if (SpellHelper.IsReady(29238u))
			{
			//	return 1;
			}
			return -3;
		}

		public void Build(Slot slot)
		{
			slot.Add(SpellHelper.GetSpell(29238u));
		}
	}
}
