using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.GCD;

public class 魔三连 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 魔三连变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(41488);
	}
	public int Check() 
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("魔四连"))
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
		if (魔三连变化() != 41491)
		{
			if (PVPHelper.通用距离检查(5))
			{
				return -5 ;
			}
			if (PVPHelper.通用技能释放Check(魔三连变化(),5)==null)
			{
				return -6 ;
			}
		}
		if (!魔三连变化().GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,魔三连变化(),5);
	}
}
public class 焦热 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 魔三连变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(41488);
	}
	public int Check() 
	{
		if(!PvPRDMOverlay.RDMQt.GetQt("魔四连"))
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
		if (魔三连变化() != 41491) return -77;
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(魔三连变化(),25)==null)
		{
			return -6 ;
		}
		return 1;
	}
	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,魔三连变化(),25);
	}
}