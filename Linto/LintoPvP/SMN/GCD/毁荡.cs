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
		if (!Core.Me.HasLocalPlayerAura(4399U))
		{
			if(MoveHelper.IsMoving())
			{
				return -10;
			}
		}
		if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 22+SettingMgr.GetSetting<GeneralSettings>().AttackRange)
		{
			return -5;
		}
		if (!(29664u).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillDecision.技能变化(29664u),5);
	}
}

