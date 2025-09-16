using CombatRoutine;
using Common;
using Common.Define;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability
{
	public class 药 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;


		public int Check()
		{
			if(!PVPHelper.通用权限())
			{
				return -999999;
			}
			if (!SCHQt.GetQt("喝热水"))
			{
				return -9;
			}
			if (!PVPHelper.CanActive())
			{
				return -3;
			}
			if (!SpellHelper.IsReady(29711))
			{
				return -2;
			}
			if (Core.Me.CurrentHealthPercent <= PvPSCHSettings.Instance.药血量 / 100f)
			{
				return 0;
			}
			
			return -1;
		}

		public void Build(Slot slot)
		{
			slot.Add(SpellHelper.GetSpell(29711,SpellTargetType.Self));
		}
	}
}
