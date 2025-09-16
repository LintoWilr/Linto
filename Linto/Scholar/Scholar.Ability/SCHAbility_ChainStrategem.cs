
using CombatRoutine;
using Common;

namespace Linto.Scholar.Scholar.Ability;

public class SCHAbility_ChainStrategem : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (!ScholarOverlay.SCHQt.GetQt("连环计"))
		{
			return -3;
		}
		if (!SpellHelper.IsReady(7436u))
		{
			return -1;
		}
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
		if (aoeCount >= 4)
		{
			return -4;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(7436u));
	}
}
