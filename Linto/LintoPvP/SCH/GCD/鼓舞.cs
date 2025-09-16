using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.GCD
{
	public class 鼓舞 : ISlotResolver
	{
		public SlotMode SlotMode { get; }

		public int Check()
		{
			if(!PVPHelper.通用权限())
			{
				return -999999;
			}
			if (!PVPHelper.CanActive())
			{
				return -3;
			}
			if(!SCHQt.GetQt("鼓舞"))
			{
				return -233;
			}
			if (PVPHelper.距离大于30米远程范围())
			{
				return -5;
			}

			if (PvPSCHSettings.Instance.上毒优先开启)
			{
				if (Core.Me.HasMyAura(3094))
				{
					return -44;
				}
			}
			if (PVPHelper.鼓舞充能层数() >=1.5&SpellHelper.GetSpell(29233u).Cooldown.TotalMilliseconds <13000)
			{
				return 2;
			}

			return -1;
		}

		public void Build(Slot slot)
		{
			slot.Add(SpellHelper.GetSpell(29232u,SpellTargetType.Self));
		}
	}
}