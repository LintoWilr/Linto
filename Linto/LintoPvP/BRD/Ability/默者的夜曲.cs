using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.Ability;

public class 默者的夜曲 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29395u;
	private const int SkillRange = 15;

	public int Check()
	{
		if (!PvPBRDOverlay.BRDQt.GetQt("沉默"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		if (PvPSettings.Instance.技能自动选中)
		{
			var bestTarget = PVPTargetHelper.TargetSelector.Get最合适目标(SkillRange + PvPSettings.Instance.长臂猿,SkillId);
			if (PvPSettings.Instance.最合适目标 &&
			    (bestTarget != null && bestTarget != Core.Me))
			{
				if (PVPTargetHelper.Check目标免控(bestTarget))
				{
					return -3;
				}
			}
			
			var nearestTarget = PVPTargetHelper.TargetSelector.Get最近目标();
			if ((nearestTarget != null && nearestTarget != Core.Me))
			{
				if (PVPTargetHelper.Check目标免控(nearestTarget))
				{
					return -3;
				}
			}
		}
		if (!PvPSettings.Instance.技能自动选中)
		{
			if (PVPTargetHelper.Check目标免控(Core.Me.GetCurrTarget())) 
			{
				return -3;
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
