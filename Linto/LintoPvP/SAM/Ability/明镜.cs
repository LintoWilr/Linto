using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.Ability;

public class 明镜 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 技能明镜 = 29536u;

	public int Check()//29536 明镜
	{
		if (!PvPSAMOverlay.SAMQt.GetQt("明镜"))
		{
			return -9;
		}
		if (!技能明镜.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (!Core.Me.InCombat())
		{
			return -8;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		var currentTarget = Core.Me.GetCurrTarget();
		if (currentTarget == null || !currentTarget.CanAttack())
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(PVPHelper.等服务器Spell(技能明镜,Core.Me));
	}
}
