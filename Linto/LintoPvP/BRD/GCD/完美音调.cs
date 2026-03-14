using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.GCD;

public class 完美音调 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29392u;
	private const uint RequiredAura = 3137u;
	private const int SkillRange = 25;
	public int Check()
	{
		if (!PvPBRDOverlay.BRDQt.GetQt("强劲射击"))
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
		if (!Core.Me.HasAura(RequiredAura))
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6;
		}
		
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}

