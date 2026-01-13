using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.Ability;
public class 冲刺 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public int Check()
	{
		if (!PvPBLMOverlay.BLMQt.GetQt("冲刺"))
		{
			return -9;
		}
		if (Core.Me.HasAura(1342u))
		{
			return -8;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown() != 0)
		{
			return -4;
		}
		if (PVPHelper.check坐骑())
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(new Spell(29057u, Core.Me));
	}
}