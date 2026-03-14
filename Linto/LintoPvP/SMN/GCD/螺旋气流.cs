using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 螺旋气流 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint ReadySkillId = 29669u;
	private const uint CheckSkillId = 41483u;
	private const int SkillRange = 25;

	public int Check()
	{
		if (!SMNQt.GetQt("螺旋气流"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!ReadySkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
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
