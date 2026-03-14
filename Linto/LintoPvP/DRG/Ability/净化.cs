using AEAssist;
using AEAssist.CombatRoutine.Module;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG;

public class 净化 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 净化技能 = 29056u;


	public int Check()
	{
		if (!DRGQt.GetQt("自动净化"))
		{
			return -9;
		}
		if (PVPHelper.净化判断())
		{
			return 1;
		}
		return -3;
	}

	public void Build(Slot slot)
	{
		slot.Add(PVPHelper.等服务器Spell(净化技能,Core.Me));
	}
}
