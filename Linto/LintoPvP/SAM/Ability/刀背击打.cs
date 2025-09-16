using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.Ability;
public class 刀背击打 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public uint 技能刀背击打 = 29535u;
	public int Check()//29535 刀背击打
	{
		if (!PvPSAMOverlay.SAMQt.GetQt("刀背击打"))
		{
			return -9;
		}
		if (!技能刀背击打.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPTargetHelper.Check目标免控(Core.Me.GetCurrTarget()))
		{
			return -5;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(5))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29535,5)==null)
		{
			return -6 ;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,技能刀背击打,5);
	}
}
