using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.GCD;

public class 火1 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("火魔法"))
		{
			return -233;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if(Core.Me.HasAura(3382u))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29649,25)==null)
		{
			return -6 ;                              
		}
		if(Core.Me.HasAura(3212))//星极火
			return -6;    
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29649u,25);
	}
}
public class 火2 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("火魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(30896u,25)==null)
		{
			return -6 ;
		}
		if(Core.Me.HasAura(3212))//星极火
			return 0;    
		return -6;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,30896u,25);
	}
}
public class 火3 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("火魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29650u,25)==null)
		{
			return -6 ;
		}
		if(!Core.Me.HasAura(3213u))
		{
			return -2;
		}
		if(Core.Me.HasAura(3213))//星极火II
			return 0;    
		return -6;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29650u,25);
	}
}
public class 火4 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(PvPBLMOverlay.BLMQt.GetQt("火魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(41473u,25)==null)
		{
			return -6 ;
		}
		if (!29660u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if(!Core.Me.HasAura(3213u))
		{
			return -2;
		}
		if(Core.Me.HasAura(3214))//星极火III
			return 0;    
		return -6;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41473u,25);
	}
}
public class 核爆 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPBLMOverlay.BLMQt.GetQt("火魔法"))
		{
			return -233;
		}
		if (MoveHelper.IsMoving())
		{
			return -10;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (!PvPBLMSettings.Instance.最佳AOE)
		{
			if (PVPHelper.通用距离检查(25))
			{
				return -5 ;
			}
			if (PVPHelper.通用技能释放Check(29651,25)==null)
			{
				return -6 ;
			}
		}
		if (PvPBLMSettings.Instance.最佳AOE)
		{
			if((TargetHelper.GetMostCanTargetObjects(29651u, PvPBLMSettings.Instance.最佳人数))==null)
			{
				return -99;
			}
			if (Core.Me.Distance(TargetHelper.GetMostCanTargetObjects(29651u, PvPBLMSettings.Instance.最佳人数)) > 25+PvPSettings.Instance.长臂猿)
			{
				return -5;
			}
		}
		if (!29651u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		if (PvPBLMSettings.Instance.最佳AOE)
		{
			slot.Add(new Spell(29651u, TargetHelper.GetMostCanTargetObjects(29651u, PvPBLMSettings.Instance.最佳人数)));
		}
		else PVPHelper.通用技能释放(slot,29651u,25);
	}
}
         