
using CombatRoutine;
using Common;

namespace Linto.Scholar.Scholar.Ability;

public class SCHAbility_Aethorflow : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (!ScholarOverlay.SCHQt.GetQt("以太超流"))
		{
			return -3;
		}
		if (!SpellHelper.IsReady(166u))
		{
			return -4;
		}
		if (Core.Get<IMemApiScholar>().Aetherflow() == 0)
		{
			return 0;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(166u));
	}
}
