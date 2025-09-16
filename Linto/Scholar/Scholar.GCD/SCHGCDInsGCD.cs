using CombatRoutine;

namespace Linto.Scholar.Scholar.GCD;

public class SCHGCDInsGCD : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if (SCHBattleData.Instance.SpellQueueGCD.Count > 0)
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SCHBattleData.Instance.SpellQueueGCD.Peek());
	}
}
