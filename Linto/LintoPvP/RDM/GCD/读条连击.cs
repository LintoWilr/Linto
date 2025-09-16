using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.GCD;

public class 激荡 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("读条连击")) return -1;
		if (!PVPHelper.CanActive()) return -2;
		if (Core.Me.HasLocalPlayerAura(1393u)) return -3;
		if (GCDHelper.GetGCDCooldown()>0) return -4;
		if (!41486U.GetSpell().IsReadyWithCanCast()) return -3;
		if (Core.Me.IsMoving()) return -5;
		if (PVPHelper.通用距离检查(25)) return -5;
		if (PVPHelper.通用技能释放Check(41486U,25)==null) return -6 ;

		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41486U,25);
	}
}
public class 显贵冲击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("读条连击")) return -1;
		if (!PVPHelper.CanActive()) return -2;
		if (!Core.Me.HasLocalPlayerAura(1393u)) return -3;
		if (GCDHelper.GetGCDCooldown()>0) return -5;
		if (!41487U.GetSpell().IsReadyWithCanCast()) return -6;
		if (PVPHelper.通用距离检查(25)) return -8;
		if (PVPHelper.通用技能释放Check(41487U,25)==null) return -9 ;
		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41487U,25);
	}
}