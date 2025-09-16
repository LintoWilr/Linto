using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 螺旋气流 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if (!SMNQt.GetQt("螺旋气流"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!(29669u).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41483u,25)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillDecision.技能变化(29669u),25);
	}
}
