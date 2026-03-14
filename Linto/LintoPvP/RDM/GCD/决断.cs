
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability;

public class 决断 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 41492u;
	private const uint 鼓励来源Aura = 2282u;
	private const int SkillRange = 25;
	public int Check()
	{
		if (!PvPRDMOverlay.RDMQt.GetQt("决断")) return -1;
		if (!PVPHelper.CanActive()) return -2;
		if(PvPRDMSettings.Instance.鼓励决断&&!Core.Me.HasLocalPlayerAura(鼓励来源Aura)) return -3;
		if (GCDHelper.GetGCDCooldown()>0) return -5;
		if (!SkillId.GetSpell().IsReadyWithCanCast()) return -6;
		if (PVPHelper.通用距离检查(SkillRange)) return -5 ;
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null) return -6 ;
		return 0;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
