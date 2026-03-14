using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 霰弹枪 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29404u;
	private const int SkillRange = 12;

	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("霰弹枪"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>200)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
