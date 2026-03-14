using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.Ability;
public class 蛇续剑 : ISlotResolver
{
	private const uint 技能变化起点 = 39183u;
	private const uint 起手技能 = 39183u;
	private const uint 远程技能 = 39177u;
	private const int 远程距离 = 20;
	public uint 蛇续剑id()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(技能变化起点);
	}

	public SlotMode SlotMode { get; } = SlotMode.Always;
	private int 技能距离 => 2 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
	public int Check()
	{
		var changedSkill = 蛇续剑id();
		if (!PvPVPROverlay.VPRQt.GetQt("蛇续剑"))
		{
			return -9;
		}
		if (changedSkill==起手技能)
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		
		if (changedSkill!=远程技能&&PVPHelper.通用距离检查(技能距离))
		{
			return -5 ;
		}
		if (changedSkill==远程技能&&PVPHelper.通用距离检查(远程距离))
		{
			return -5 ;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		var changedSkill = 蛇续剑id();
		PVPHelper.通用技能释放(slot,changedSkill,技能距离);
	}
}
