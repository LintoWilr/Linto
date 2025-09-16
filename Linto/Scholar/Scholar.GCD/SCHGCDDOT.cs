
using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;

namespace Linto.Scholar.Scholar.GCD;

public class SCHGCDDOT : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public Spell GetSpell()
	{
		return SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(SpellHelper.GetSpell(17864u).Id));
	}

	public int Check()
	{
		
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
		if (aoeCount >= 4)
		{
			return -4;
		}
		if (AuraHelper.HasMyAuraWithTimeleft(UnitHelper.GetCurrTarget(Core.Me), 179u, 3000) || AuraHelper.HasMyAuraWithTimeleft(UnitHelper.GetCurrTarget(Core.Me), 189u, 3000) || AuraHelper.HasMyAuraWithTimeleft(UnitHelper.GetCurrTarget(Core.Me), 1895u, 3000))
		{
			return -1;
		}
		if (DotBlacklistHelper.IsBlackList(UnitHelper.GetCurrTarget(Core.Me)))
		{
			return -10;
		}
		if (!ScholarOverlay.SCHQt.GetQt("DOT"))
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(GetSpell());
	}
}
