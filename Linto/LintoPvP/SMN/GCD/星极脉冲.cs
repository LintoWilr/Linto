using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.GCD;

public class 星极脉冲 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{

		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!SMNQt.GetQt("毁荡"))
		{
			return -233;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!(29665u).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if(!Core.Me.HasAura(3228u))
		{
			return -10;
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
		PVPHelper.通用技能释放(slot,29665u,25);
	}
}

