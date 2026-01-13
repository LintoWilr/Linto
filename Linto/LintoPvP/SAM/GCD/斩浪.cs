using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.GCD;

public class 斩浪 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能斩浪 = 29530u;
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
		if (PVPHelper.通用距离检查(8))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29530,8)==null)
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
		PVPHelper.通用技能释放(slot,技能斩浪,8);
	}
}
public class 回返斩浪 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能斩浪 = 29530u;
	public uint 技能回返斩浪 = 29531u;
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
		if (PVPHelper.通用距离检查(8))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29531,8)==null)
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
		PVPHelper.通用技能释放(slot,技能回返斩浪,8);
	}
}