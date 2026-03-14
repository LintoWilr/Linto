using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SMN.GCD;

public class 毁荡 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29664u;
	private const uint 毁绝Buff = 4399u;
	private const int SpellRange = 5;

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
		if (!Core.Me.HasLocalPlayerAura(毁绝Buff))
		{
			if(MoveHelper.IsMoving())
			{
				return -10;
			}
		}
		var currentTarget = Core.Me.GetCurrTarget();
		if (currentTarget == null || Core.Me.Distance(currentTarget) > 22+SettingMgr.GetSetting<GeneralSettings>().AttackRange)
		{
			return -5;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillDecision.技能变化(SkillId),SpellRange);
	}
}

