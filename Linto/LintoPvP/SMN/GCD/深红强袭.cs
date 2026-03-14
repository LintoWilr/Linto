using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 深红强袭 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 前置连击技能 = 29667u;
	private const uint ReadySkillId = 29668u;
	private const uint CheckSkillId = 41483u;
	private const int SkillRange = 25;

	public int Check()
	{

		if(!SMNQt.GetQt("深红强袭"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (Core.Resolve<MemApiSpell>().GetLastComboSpellId()!=前置连击技能)
		{
			return -5;
		}
		if (!ReadySkillId.GetSpell().IsReadyWithCanCast())
		{
			return -6;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(CheckSkillId,SkillRange)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillDecision.技能变化(ReadySkillId),SkillRange);
	}
}
