using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.Ability;

public class 暗黑锋 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	private Spell GetSpell()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (!Qt.GetQt("AOE"))
		{
			return SpellHelper.GetSpell(16467u);
		}
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
		if (aoeCount < 3)
		{
			return SpellHelper.GetSpell(16467u);
		}
		return SpellHelper.GetSpell(16466u);
	}

	public int Check()
	{
		CharacterAgent me = Core.Me;

		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(GetSpell());
	}
}
