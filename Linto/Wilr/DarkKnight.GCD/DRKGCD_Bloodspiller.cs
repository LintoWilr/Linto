using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.GCD;

public class DRKGCD_Bloodspiller : ISlotResolver
{
	public SlotMode SlotMode { get; }

	private Spell GetSpell()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (Qt.GetQt("AOE"))
		{
			int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
			if (aoeCount >= 3)
			{
				return SpellHelper.GetSpell(7391u);
			}
		}
		return SpellHelper.GetSpell(7392u);
	}

	public int Check()
	{
		if (Qt.GetQt("攒暗血"))
		{
			return -1;
		}
		if (Data.Get弗雷剩余时间() > 0 && Data.Get暗血() >= 50)
		{
			return 1;
		}
		if (Qt.GetQt("倾泻资源") && Data.Get暗血() >= 50)
		{
			return 5;
		}
		if (!Data.HasBuff(1972u) && !Data.HasBuff(742u))
		{
			if (Data.Get暗血() >= 90)
			{
				return 1;
			}
			if (!SpellHelper.CoolDownInGCDs(SpellHelper.GetSpell(16472u), 3) && SpellHelper.CoolDownInGCDs(SpellHelper.GetSpell(7390u), 2) && Data.Get暗血() >= 50)
			{
				return 1;
			}
		}
		if (!Data.HasBuff(1972u) && Data.HasBuff(742u) && Data.Get暗血() >= 70)
		{
			return 1;
		}
		if (Data.HasBuff(1972u) && !Data.HasBuff(742u) && (Data.Get暗血() >= 80 || Data.Buff时间(1972u) <= 8000))
		{
			return 1;
		}
		if (Data.HasBuff(1972u) && Data.HasBuff(742u))
		{
			if (Data.Get暗血() <= 40 && SpellHelper.GetSpell(16472u).Cooldown.TotalMilliseconds <= 5000.0)
			{
				return -1;
			}
			return 1;
		}
		return -1;
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
