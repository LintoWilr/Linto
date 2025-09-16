using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.Ability;
public class 动物构想 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public uint 动物构想变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物构想);
	}
	public int Check()
	{
		if (!PvPPCTOverlay.PCTQt.GetQt("动物构想"))
		{
			return -1;
		}
		if (动物构想变化()==PCTSkillID.技能动物构想)
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(动物构想变化(),25)==null)
		{
			return -6 ;
		}
		if (Core.Resolve<MemApiSpell>().GetCharges(动物构想变化())<1f)
		{
			return -4;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,动物构想变化(),25);
	}
}
