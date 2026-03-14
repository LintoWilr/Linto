using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.GCD;

public class 星极脉冲 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29665u;
	private const uint RequiredAura = 3228u;
	private const uint CheckSkillId = 41483u;
	private const int SkillRange = 25;

	public int Check()
	{

		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!SMNQt.GetQt("毁荡"))
		{
			return -233;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if(!Core.Me.HasAura(RequiredAura))
		{
			return -10;
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

