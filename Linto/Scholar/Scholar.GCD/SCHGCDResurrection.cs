
using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Scholar.Scholar.GCD;

public class SCHGCDResurrection : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
	
		if (!ScholarOverlay.SCHQt.GetQt("拉人"))
		{
			return -3;
		}
		if (!SpellHelper.IsReady(7561u))
		{
			return -4;
		}
		CharacterAgent me = Core.Me;
		if (me.CurrentMana < 2400)
		{
			return -5;
		}
		CharacterAgent skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(148u));
		if (!skillTarget.IsValid)
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot)
	{

		CharacterAgent skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(148u));
		slot.Add(SpellHelper.GetSpell(7561u));
		slot.Add(new Spell(173u, skillTarget));
	}
}
