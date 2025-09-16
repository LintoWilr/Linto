using CombatRoutine;

namespace Linto.Wilr.DarkKnight.Ability;

public class 腐蚀大地 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!Qt.GetQt("腐蚀大地"))
		{
			return -3;
		}
		if (!SpellHelper.IsReady(3639u))
		{
			return -2;
		}
		if (Data.HasBuff(749u))
		{
			return -2;
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
		if (Data.Get到目标距离() <= (float)(Data.攻击距离 + 2))
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(3639u));
	}
}
