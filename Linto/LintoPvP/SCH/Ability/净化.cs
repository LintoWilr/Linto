using CombatRoutine;
using Common;
using Common.Define;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability
{
	public class 净化 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;


		public int Check()
		{
			if(!PVPHelper.通用权限())
			{
				return -999999;
			}
			if (!SCHQt.GetQt("自动净化"))
			{
				return -9;
			}
			if (!SpellHelper.IsReady(29056u))
			{
				return -2;
			}
			if (Core.Me.HasAura(1347u))
			{
				return 1;
			}
			if (Core.Me.HasAura(1343u))
			{
				return 2;
			}
			return -3;
		}

		public void Build(Slot slot)
		{
			slot.Add(SpellHelper.GetSpell(29056u,SpellTargetType.Self));
		}
	}
}
