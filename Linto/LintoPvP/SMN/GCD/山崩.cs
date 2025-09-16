using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.GCD;

public class 山崩 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;

	public int Check()
	{
		if(!SMNQt.GetQt("山崩"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}

		if (!(29671u).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPTargetHelper.Check目标罩子(Core.Me.GetCurrTarget()))
		{
			return -6;
		}
		if (PVPHelper.通用距离检查(8))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41483u,8)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29671u,8);
	}
}
