using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.GCD;

public class 悖论 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("悖论"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>1200)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29663,25)==null)
		{
			return -6 ;
		}
		if (Core.Me.HasAura(3223))
		{
			return 1;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29663u,25);
	}
}
public class 元素天赋 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 元素天赋变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(41475U);
	}
	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("元素天赋"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()==0)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (!PvPBLMSettings.Instance.寒冰环)
		{
			if(元素天赋变化()==41478U) 
				return -2;
		}
		if (!元素天赋变化().GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if(元素天赋变化()!=41475U) 
			return 0;
		return -1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,元素天赋变化(),25);
	}
}
