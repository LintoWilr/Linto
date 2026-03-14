using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.GCD;

public class 斩浪 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 技能斩浪 = 29530u;
	private const int SkillRange = 8;
	public int Check()//29530 斩浪
	{
		if(!PvPSAMOverlay.SAMQt.GetQt("斩浪"))
		{
			return -1;
		}
		if(PvPSAMSettings.Instance.读条检查)
		{
			if(MoveHelper.IsMoving())
			{
				return -10;
			}
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>300)
		{
			return -9;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(技能斩浪,SkillRange)==null)
		{
			return -6 ;
		}
		if (!技能斩浪.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,技能斩浪,SkillRange);
	}
}
public class 回返斩浪 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint 技能斩浪 = 29530u;
	private const uint 技能回返斩浪 = 29531u;
	private const int SkillRange = 8;
	public int Check()//29531 回返斩浪 
	{
		if(!PvPSAMOverlay.SAMQt.GetQt("斩浪"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(技能回返斩浪,SkillRange)==null)
		{
			return -6 ;
		}
		if (Core.Resolve<MemApiSpell>().GetCooldown(技能斩浪).TotalMilliseconds>0&&Core.Resolve<MemApiSpellCastSuccess>().LastGcd==技能斩浪)    
		{
			return 2;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,技能回返斩浪,SkillRange);
	}
}
