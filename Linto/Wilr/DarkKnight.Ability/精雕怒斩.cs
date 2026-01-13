using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.Ability;

public class 精雕怒斩 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	private Spell GetSpell()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (!Qt.GetQt("AOE"))
		{
			return SpellHelper.GetSpell(3643u);
		}
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 15, 5);
		if (aoeCount < 4)
		{
			return SpellHelper.GetSpell(3643u);
		}
		return SpellHelper.GetSpell(3641u);
	}

	public int Check()
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(3643u))
		{
			return -1;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (!Qt.GetQt("CD"))
		{
			return -1;
		}
		if (Data.Get暗黑剩余时间() == 0)
		{
			return -1;
		}
		if (Data.是否延后对齐120())
		{
			return -3;
		}
		if (Data.Get到目标距离() <= (float)Data.攻击距离)
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(Core.Get<IMemApiSpell>().CheckActionChange(GetSpell().Id)));
	}
}
