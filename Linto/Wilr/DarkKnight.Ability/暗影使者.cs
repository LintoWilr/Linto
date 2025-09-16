using CombatRoutine;

namespace Linto.Wilr.DarkKnight.Ability;

public class 暗影使者 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(25757u))
		{
			return -1;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (!Qt.GetQt("CD"))
		{
			return -1;
		}
		if (Data.Get暗黑剩余时间() == 0)
		{
			return -1;
		}
		if (SpellHelper.RecentlyUsed(25757u, 2200))
		{
			return -1;
		}
		if ((double)SpellHelper.GetSpell(25757u).Charges <= 1.99 && SpellHelper.GetSpell(3640u).Charges == 2f)
		{
			return -2;
		}
		if (Data.Get弗雷剩余时间() == 0 && SpellHelper.GetSpell(25757u).Charges < 2f)
		{
			return -3;
		}
		if (Data.是否延后对齐120())
		{
			return -1;
		}
		if (Data.Get到目标距离() <= (float)(Data.攻击距离 + 7))
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(25757u));
	}
}
