using CombatRoutine;

namespace Linto.Wilr.DarkKnight.Ability;

public class 弗雷 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (!SpellHelper.IsReady(16472u))
		{
			return -1;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (!Qt.GetQt("CD"))
		{
			return -2;
		}
		if (Data.Get暗血() >= 50)
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(16472u));
	}
}
