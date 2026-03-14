using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN.Ability;

public class 溃烂爆发 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 41483u;
	private const int SkillRange = 25;
	public int Check()
	{
		if (!SMNQt.GetQt("坏死爆发"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (SpellHelper.GetSpell(SkillId).Charges < 1 )
		{
			return -1;
		}
		var currentTarget = Core.Me.GetCurrTarget();
		if (currentTarget == null || currentTarget.CurrentHpPercent() > PvPSMNSettings.Instance.溃烂阈值)
		{
			return -5;
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
