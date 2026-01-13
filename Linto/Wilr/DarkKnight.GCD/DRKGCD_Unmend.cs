using CombatRoutine;

namespace Linto.Wilr.DarkKnight.GCD;

public class DRKGCD_Unmend : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if (Data.Get到目标距离() > (float)Data.攻击距离 && Data.Get到目标距离() <= 20f && Qt.GetQt("伤残"))
		{
			return 0;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(3624u));
	}
}
