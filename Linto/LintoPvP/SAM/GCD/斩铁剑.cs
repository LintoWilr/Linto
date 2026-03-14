using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.GCD;

public class 斩铁剑 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29537u;
	private const float MaxDistance = 25f;
	public int Check() //29537 斩铁
	{
		if(!(PVPHelper.高级码||PVPHelper.通用码权限))
		{
			return -1;
		}
		if(!PvPSAMOverlay.SAMQt.GetQt("斩铁剑"))
		{
			return -1;
		}
		if(PvPSAMSettings.Instance.斩铁检查状态)
		{
			if (!PVPHelper.CanActive())
			{
				return -2;
			}
		}
		if(!PvPSAMSettings.Instance.多斩模式)
		{
			var target = PVPTargetHelper.TargetSelector.Get斩铁目标();
			if (target==null||target.DistanceToPlayer()>MaxDistance)
			{
				return -3;
			}
		}
		if(PvPSAMSettings.Instance.多斩模式)
		{
			var target = PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数);
			if (target==null||target.DistanceToPlayer()>MaxDistance)
			{
				return -3;
			}
		}
		if (Core.Me.LimitBreakCurrentValue()<4000)
		{
			return -1;
		}
		if (!PVPHelper.是否55())
		{
			if (Core.Me.LimitBreakCurrentValue()<4500)
			{
				return -1;
			}
		}
		return 6;
	}

	public void Build(Slot slot)
	{
		if (PvPSAMSettings.Instance.多斩模式)
		{
			var target = PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数);
			if (PvPSAMSettings.Instance.斩铁调试)
			{
				LogHelper.Print($"尝试斩铁目标：{target}");
			}
			slot.Add(PVPHelper.等服务器Spell(SkillId,target));
		}
		else
		{
			var target = PVPTargetHelper.TargetSelector.Get斩铁目标();
			if (PvPSAMSettings.Instance.斩铁调试)
			{
				LogHelper.Print($"尝试斩铁目标：{target}");
			}
			slot.Add(PVPHelper.等服务器Spell(SkillId, target));
		}
	}
}
