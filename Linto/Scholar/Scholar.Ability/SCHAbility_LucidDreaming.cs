using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Scholar.Scholar.Ability;

public class SCHAbility_LucidDreaming : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (!SpellHelper.IsReady(7562u))
		{
			return -1;
		}
		CharacterAgent me = Core.Me;
		if (me.CurrentMana > 8000)
		{
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(7562u));
	}
}
