using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.GCD;

public class DRKGCD_BaseGCD : ISlotResolver
{
	public SlotMode SlotMode { get; }

	private Spell GetSpell()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerOptions.Instance.AOE)
		{
			int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
			if (aoeCount >= 2 && (Core.Get<IMemApiSpell>().GetLastComboSpellId() == 3621 || (Core.Get<IMemApiSpell>().GetLastComboSpellId() != 3617 && Core.Get<IMemApiSpell>().GetLastComboSpellId() != 3623)))
			{
				if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == 3621)
				{
					return SpellHelper.GetSpell(16468u);
				}
				return SpellHelper.GetSpell(3621u);
			}
		}
		if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == 3623)
		{
			return SpellHelper.GetSpell(3632u);
		}
		if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == 3617)
		{
			return SpellHelper.GetSpell(3623u);
		}
		return SpellHelper.GetSpell(3617u);
	}

	public int Check()
	{
		if (Data.Get到目标距离() > (float)Data.攻击距离)
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		Spell spell = GetSpell();
		if (spell != null)
		{
			slot.Add(spell);
		}
	}
}
