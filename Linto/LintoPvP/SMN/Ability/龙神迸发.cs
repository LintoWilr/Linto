using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 龙神迸发 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if (!SMNQt.GetQt("死星核爆"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!Core.Me.HasAura(3228u))
		{
			return -2;
		}
		if (!(41484u).GetSpell().IsReadyWithCanCast())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41484u,25)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41484u,25);
	}
}
