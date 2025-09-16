using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.GCD;

public class 龙尾大回旋 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()//29488 龙尾大回旋
	{
		if(!DRGQt.GetQt("基础连击"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() != 29487u)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(DRGSkillID.龙尾大回旋距离))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(DRGSkillID.龙尾大回旋,DRGSkillID.龙尾大回旋距离)==null)
		{
			return -6 ;
		}
        return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,DRGSkillID.龙尾大回旋,DRGSkillID.龙尾大回旋距离);
	}
}
