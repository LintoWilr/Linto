using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 蓄力冲击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29402u;
	private const uint 过热光环 = 3149u;
	private const int SkillRange = 25;

	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!PvPMCHOverlay.MCHQt.GetQt("蓄力冲击"))
		{
			return -233; 
		}
		if (!Core.Resolve<MemApiSpell>().CheckActionChange(SkillId).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>50)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		if (Core.Me.HasAura(过热光环))
		{
			return -9;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
public class 热冲击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 热冲击技能 = 29403u;
	private const uint 烈焰弹技能 = 41468u;
	private const uint 过热光环 = 3149u;
	private const int SkillRange = 25;

	public uint 烈焰弹id()
	{
		if (PvPMCHSettings.Instance.热冲击)
			return 热冲击技能;
		else return 烈焰弹技能;
	}
	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!PvPMCHOverlay.MCHQt.GetQt("野火"))
		{
			return -233; 
		}
		if (!Core.Resolve<MemApiSpell>().CheckActionChange(烈焰弹id()).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>200)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		var currentSkill = 烈焰弹id();
		if (PVPHelper.通用技能释放Check(currentSkill,SkillRange)==null)
		{
			return -6 ;
		}
		if (!Core.Me.HasAura(过热光环))
		{
			return -9;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		var currentSkill = 烈焰弹id();
		PVPHelper.通用技能释放(slot,currentSkill,SkillRange);
	}
}
