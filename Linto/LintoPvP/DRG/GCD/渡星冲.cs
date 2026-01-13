using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.GCD;

public class 渡星冲 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()//41449
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
		if (PVPHelper.通用距离检查(20))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41450,20)==null)
		{
			return -6 ;
		}
        return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41450,20);
	}
}
