using CombatRoutine;

namespace Linto.Wilr.DarkKnight.Ability;

public class 嗜血 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(3625u))
		{
			return -1;
		}
		if (Data.DkSetting.NoOgcdFar && Data.Get到目标距离() > (float)Data.攻击距离)
		{
			return -6;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (!Qt.GetQt("CD"))
		{
			return -1;
		}
		if (Data.爆发120 <= 20000.0 && !SpellHelper.CoolDownInGCDs(SpellHelper.GetSpell(16472u), 3))
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(3625u));
	}
}
