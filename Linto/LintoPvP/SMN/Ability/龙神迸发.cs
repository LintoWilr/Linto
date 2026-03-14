using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 龙神迸发 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 41484u;
	private const uint RequiredAura = 3228u;
	private const int SkillRange = 25;


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
		if(!Core.Me.HasAura(RequiredAura))
		{
			return -2;
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
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
