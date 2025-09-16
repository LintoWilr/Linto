using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 高跳 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	
	static bool 龙血()
	{
		return PvPDRGSettings.Instance.高跳龙血;
	}

	public int Check()//29493 高跳
	{
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (!DRGQt.GetQt("高跳"))
		{
			return -9;
		}
		if (!29493u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(DRGSkillID.高跳距离))
		{
			return -5 ;
		}
		if (龙血())
		{
			if (!Core.Me.HasAura(3177))
			{
				return -8;
			}
		}
		if (PVPHelper.通用技能释放Check(DRGSkillID.死者之岸,DRGSkillID.死者之岸距离)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,DRGSkillID.高跳,DRGSkillID.高跳距离);
	}
}
