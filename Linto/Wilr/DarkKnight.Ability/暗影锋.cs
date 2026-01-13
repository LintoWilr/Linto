using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.Ability;

public class 暗影锋 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	private Spell GetSpell()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (!Qt.GetQt("AOE"))
		{
			return SpellHelper.GetSpell(16470u);
		}
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
		if (aoeCount < 3)
		{
			return SpellHelper.GetSpell(16470u);
		}
		return SpellHelper.GetSpell(16469u);
	}

	public int Check()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(16470u))
		{
			return -1;
		}
		if (SpellHelper.RecentlyUsed(16470u, 2200))
		{
			return -1;
		}
		int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
		if (aoeCount < 3)
		{
			if (Data.Get到目标距离() > (float)Data.攻击距离)
			{
				return -2;
			}
		}
		else if (Data.Get到目标距离() > (float)(Data.攻击距离 + 7))
		{
			return -2;
		}
		if (Qt.GetQt("倾泻资源"))
		{
			return 5;
		}
		if (Data.Get暗黑剩余时间() < 3000)
		{
			return 1;
		}
		if (Core.Get<IMemApiDarkKnight>().HasDarkArts())
		{
			return 2;
		}
		if (Data.GetMP() >= 8500)
		{
			return 1;
		}
		if (Data.Get弗雷剩余时间() > 0)
		{
			if (SpellHelper.GetSpell(3643u).Cooldown == new TimeSpan(0, 0, 0))
			{
				return -3;
			}
			if (SpellHelper.GetSpell(3639u).Cooldown == new TimeSpan(0, 0, 0))
			{
				return -3;
			}
			if (SpellHelper.GetSpell(3640u).Charges == 2f)
			{
				return -3;
			}
			if (SpellHelper.GetSpell(25757u).Charges == 2f)
			{
				return -3;
			}
			if (Data.GetMP() >= 6000)
			{
				return 1;
			}
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		slot.Add(GetSpell());
	}
}
