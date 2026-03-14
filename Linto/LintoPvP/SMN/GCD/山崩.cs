using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.GCD;

public class 山崩 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29671u;
	private const uint CheckSkillId = 41483u;
	private const int SkillRange = 8;

	public int Check()
	{
		if(!SMNQt.GetQt("山崩"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}

		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPTargetHelper.Check目标罩子(Core.Me.GetCurrTarget()))
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
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
