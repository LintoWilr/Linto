using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.Ability;

public class 昏沉: ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;

	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPBLMOverlay.BLMQt.GetQt("昏沉"))
		{
			return -9;
		}
		if (!41510u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()<PvPBLMSettings.Instance.昏沉)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(41510U,25)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41510U,25);
	}
}
