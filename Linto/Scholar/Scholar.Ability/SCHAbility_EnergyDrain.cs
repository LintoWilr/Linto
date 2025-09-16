
using CombatRoutine;
using Common;

namespace Linto.Scholar.Scholar.Ability;

public class SCHAbility_EnergyDrain : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		if (!ScholarOverlay.SCHQt.GetQt("能量吸收"))
		{
			return -2;
		}
		if (AI.Instance.GetGCDCooldown() <= 700)
		{
			return -7;
		}
		if (Core.Get<IMemApiScholar>().Aetherflow() <= SCHBattleData.Instance.AethorflowReserve)
		{
			return -3;
		}
		if (!SpellHelper.IsReady(167u))
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(167u));
	}
}
