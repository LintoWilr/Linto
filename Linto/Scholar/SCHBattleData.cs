using Common.Define;

namespace Linto.Scholar;

public class SCHBattleData
{
	public static SCHBattleData Instance = new SCHBattleData();

	public int AethorflowReserve;

	public Queue<Spell> SpellQueueGCD = new Queue<Spell>();

	public Queue<Spell> SpellQueueoGCD = new Queue<Spell>();

	public void UpdateAethorflowTimes()
	{
		AethorflowReserve = 0;
	}

	public void Reset()
	{
		Instance = new SCHBattleData();
		SpellQueueGCD.Clear();
		SpellQueueoGCD.Clear();
	}
}
