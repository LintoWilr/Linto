using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.GCD;

public class 雪月花 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check() //41454 雪月花
	{
		if(!PvPSAMOverlay.SAMQt.GetQt("雪月花"))
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
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -9;
		}
		if (!Core.Me.InCombat())
		{
			return -8;
		}
		if (PVPHelper.通用距离检查(8))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41454,8)==null)
		{
			return -6 ;
		}
		if (!Core.Me.HasAura(3203u))
		{
			return -2;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41454,8);
	}
}
public class 回返雪月花 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能雪月花 = 41454u;
	public uint 技能回返雪月花 = 41455u;
	public int Check()//41455 回返斩浪 
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
		if (Core.Resolve<MemApiSpell>().GetCooldown(技能雪月花).TotalMilliseconds>0&&Core.Resolve<MemApiSpellCastSuccess>().LastGcd==技能雪月花)    
		{
			return 2;
		}
		return -1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,技能回返雪月花,8);
	}
}