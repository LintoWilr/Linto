
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.GCD;

public class 冰1 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("冰魔法"))
		{
			return -233;
		}
		if (GCDHelper.GetGCDCooldown()>PvPBLMSettings.Instance.冰时间)
		{
			return -3;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29653,25)==null)
		{
			return -6 ;
		}
		if(Core.Me.HasAura(3214))//灵极冰
			return -6;    
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29653u,25);
	}
}
public class 冰2 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("冰魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>PvPBLMSettings.Instance.冰时间)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(30897U,25)==null)
		{
			return -6 ;
		}
		if(Core.Me.HasAura(3214u))
		{
			return 0;
		}
		return -6;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,30897u,25);
	}
}
public class 冰3 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("冰魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>PvPBLMSettings.Instance.冰时间)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29654U,25)==null)
		{
			return -6 ;
		}
		if(Core.Me.HasAura(3215u))
		{
			return 0;
		}
		return -6;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29654U,25);
	}
}
public class 冰4 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
		{
			if(!PvPBLMOverlay.BLMQt.GetQt("冰魔法"))
			{
				return -233;
			}
			if (!PVPHelper.CanActive())
			{
				return -1;
			}
			if (GCDHelper.GetGCDCooldown()>PvPBLMSettings.Instance.冰时间)
			{
				return -3;
			}
			if (PVPHelper.通用距离检查(25))
			{
				return -5 ;
			}
			if (PVPHelper.通用技能释放Check(41474U,25)==null)
			{
				return -6 ;
			}
			if(Core.Me.HasAura(3382u))
			{
				return 0;
			}
			return -6;
		}

		public void Build(Slot slot)
		{
			PVPHelper.通用技能释放(slot,41474U,25);
		}
}
public class 玄冰 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PvPBLMOverlay.BLMQt.GetQt("冰魔法"))
		{
			return -233;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (GCDHelper.GetGCDCooldown()>PvPBLMSettings.Instance.冰时间)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29654U,25)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29654u,25);
	}
}