using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Helper;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.MNK.Ability;

public class 金刚极意 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!MNKQt.GetQt("金刚极意"))
		{
			return -9;
		}
		if (!SpellHelper.GetSpell(29482u).GetSpell().IsReadyWithCanCast()) 
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if(TargetHelper.GetNearbyEnemyCount(Core.Me,5,5)<1)
		{
			return -2;
		}
		if(PVPTargetHelper.Get看着目标的人(Group.敌人, Core.Me).Count<1)
		{
			return -2;
		}
		if (PvPMNKSettings.Instance.金刚阈值设置 < Core.Me.CurrentHealthPercent/1.0f)
		{ 
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29482u));
	}
}

public class 金刚神髓 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if (!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!MNKQt.GetQt("金刚极意"))
		{
			return -9;
		}
		if (!Core.Me.HasMyAura(3781u))
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (PvPMNKSettings.Instance.金刚阈值设置 < Core.Me.CurrentHealthPercent/1.0f)
		{ 
			return -4;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(29483u));
	}
}
