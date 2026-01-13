using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.GCD;

public class 动物彩绘 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 动物彩绘变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物彩绘);
	}
	public uint 动物构想变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物构想);
	}
	public int Check() 
	{
		if(!PvPPCTOverlay.PCTQt.GetQt("动物彩绘"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (Core.Me.IsMoving())
		{
			return -4;
		}
		// if (Core.Resolve<MemApiSpell>().GetCharges(动物构想变化())<0.7f)
		// {
		// 	return -5;
		// }
		if (动物彩绘变化()==PCTSkillID.技能动物彩绘)
		{
			return -7;
		}
		if (Core.Me.HasLocalPlayerAura(PCTSkillID.buff莫古力标识)||Core.Me.HasLocalPlayerAura(PCTSkillID.buff马蒂恩标识))
		{
			return -6;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		slot.Add(PVPHelper.等服务器Spell(动物彩绘变化(), Core.Me));
	}
}
