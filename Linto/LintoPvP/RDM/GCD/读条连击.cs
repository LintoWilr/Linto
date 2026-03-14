using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.GCD;

public class 激荡 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 41486u;
	private const uint 显贵前置Aura = 1393u;
	private const int SkillRange = 25;
	public int Check()
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("读条连击")) return -1;
		if (!PVPHelper.CanActive()) return -2;
		if (Core.Me.HasLocalPlayerAura(显贵前置Aura)) return -3;
		if (GCDHelper.GetGCDCooldown()>0) return -4;
		if (!SkillId.GetSpell().IsReadyWithCanCast()) return -3;
		if (Core.Me.IsMoving()) return -5;
		if (PVPHelper.通用距离检查(SkillRange)) return -5;
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null) return -6 ;

		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
public class 显贵冲击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 41487u;
	private const uint RequiredAura = 1393u;
	private const int SkillRange = 25;
	public int Check()
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("读条连击")) return -1;
		if (!PVPHelper.CanActive()) return -2;
		if (!Core.Me.HasLocalPlayerAura(RequiredAura)) return -3;
		if (GCDHelper.GetGCDCooldown()>0) return -5;
		if (!SkillId.GetSpell().IsReadyWithCanCast()) return -6;
		if (PVPHelper.通用距离检查(SkillRange)) return -8;
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null) return -9 ;
		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
