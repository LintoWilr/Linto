using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 野火 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29409u;
	private const uint RequiredAura = 3149u;
	private const int SkillRange = 12;
	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("野火"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		if (PvPMCHSettings.Instance.过热野火)
		{
			if (!Core.Me.HasAura(RequiredAura))
			{
				return -9;
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
