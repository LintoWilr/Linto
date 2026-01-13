using CombatRoutine;
using Common;

namespace Linto.Wilr.DarkKnight.Ability;

public class 跳斩 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(3640u))
		{
			return -2;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (!Qt.GetQt("CD"))
		{
			return -3;
		}
		if (!Qt.GetQt("突进"))
		{
			return -4;
		}
		if (Data.Get暗黑剩余时间() == 0)
		{
			return -5;
		}
		if (SpellHelper.RecentlyUsed(3640u, 2200))
		{
			return -6;
		}
		if (Data.DkSetting.NoOgcdFar && Data.Get到目标距离() > (float)Data.攻击距离)
		{
			return -7;
		}
		if (Qt.GetQt("保留1突进") && SpellHelper.GetSpell(3640u).MaxCharges > 1 && (double)SpellHelper.GetSpell(3640u).Charges <= 1.9)
		{
			return -8;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(3640u).Id)));
	}
}
