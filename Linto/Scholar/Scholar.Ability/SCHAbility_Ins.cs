using CombatRoutine;

namespace Linto.Scholar.Scholar.Ability;

public class SCHAbility_Ins : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (SCHBattleData.Instance.SpellQueueoGCD.Count > 0 && SCHBattleData.Instance.SpellQueueoGCD.Peek().Charges >= 1f)
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SCHBattleData.Instance.SpellQueueoGCD.Peek());
	}
}
