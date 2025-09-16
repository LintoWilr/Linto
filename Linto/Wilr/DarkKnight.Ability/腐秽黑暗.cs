using CombatRoutine;

namespace Linto.Wilr.DarkKnight.Ability;

public class 腐秽黑暗 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsUnlock(25755u))
		{
			return -1;
		}
		if (!SpellHelper.IsReady(25755u))
		{
			return -2;
		}
		if (!Data.HasBuff(749u))
		{
			return -2;
		}
		if (SpellHelper.RecentlyUsed(3639u, 2200))
		{
			return -1;
		}
		if (Data.Get距离(Data.GetTarget()) <= (float)(Data.攻击距离 + 2))
		{
			return 0;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(25755u));
	}
}
