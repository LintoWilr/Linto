
using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Scholar.Scholar.GCD;

public class SCHGCDBaseGCD : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public Spell GetSpell()
	{
		if (ScholarOverlay.SCHQt.GetQt("AOE"))
		{
			int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
			if (aoeCount >= 2)
			{
				return SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(16539u).Id));
			}
		}
		if (ScholarOverlay.SCHQt.GetQt("煤球") && Core.Get<IMemApiMove>().IsMoving())
		{
			CharacterAgent me = Core.Me;
			if (!me.HasAura(167u) && AI.Instance.GetGCDDuration() == 0)
			{
				return SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(17870u).Id));
			}
		}
		return SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(3584u).Id));
	}

	public int Check()
	{
		if (ScholarOverlay.SCHQt.GetQt("小停一下"))
		{
			return -100;
		}
		if (GetSpell() == SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(17870u).Id)) || GetSpell() == SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(16539u).Id)))
		{
			return 1;
		}
		if (Core.Get<IMemApiMove>().IsMoving())
		{
			CharacterAgent me = Core.Me;
			if (!me.HasAura(167u))
			{
				if (ScholarOverlay.SCHQt.GetQt("AOE") && GetSpell() == SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(16539u).Id)))
				{
					return 3;
				}
				if (ScholarOverlay.SCHQt.GetQt("煤球") && GetSpell() == SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(17870u).Id)))
				{
					return 2;
				}
				return -1;
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(GetSpell());
	}
}
