using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability;
public class 净化 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 技能净化 = 29056u;
	public int Check()
	{
		if (!PvPRDMOverlay.RDMQt.GetQt("自动净化"))
		{
			return -9;
		}
		if (!技能净化.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PVPHelper.净化判断())
		{
			return 1;
		}
		return -3;
	}
	public void Build(Slot slot)
	{
		// 保持原有行为：这里继续沿用当前项目中的既有技能号。
		slot.Add(PVPHelper.等服务器Spell(20956u,Core.Me));
	}
}
