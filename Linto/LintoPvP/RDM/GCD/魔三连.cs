using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.GCD;

public class 魔三连 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 起始技能 = 41488u;
	private const uint 焦热技能 = 41491u;
	private const int 近战距离 = 5;
	public uint 魔三连变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(起始技能);
	}
	public int Check() 
	{
		var changedSkill = 魔三连变化();
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
		if (changedSkill != 焦热技能)
		{
			if (PVPHelper.通用距离检查(近战距离))
			{
				return -5 ;
			}
			if (PVPHelper.通用技能释放Check(changedSkill,近战距离)==null)
			{
				return -6 ;
			}
		}
		if (!changedSkill.GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		return 1;
	}
	public void Build(Slot slot)
	{
		var changedSkill = 魔三连变化();
		PVPHelper.通用技能释放(slot,changedSkill,近战距离);
	}
}
public class 焦热 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 起始技能 = 41488u;
	private const uint 焦热技能 = 41491u;
	private const int 远程距离 = 25;
	public uint 魔三连变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(起始技能);
	}
	public int Check() 
	{
		var changedSkill = 魔三连变化();
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
		if (changedSkill != 焦热技能) return -77;
		if (PVPHelper.通用距离检查(远程距离))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(changedSkill,远程距离)==null)
		{
			return -6 ;
		}
		return 1;
	}
	public void Build(Slot slot)
	{
		var changedSkill = 魔三连变化();
		PVPHelper.通用技能释放(slot,changedSkill,远程距离);
	}
}
