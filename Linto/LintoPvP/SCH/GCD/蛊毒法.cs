using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.GCD;

public class 蛊毒法 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if(!SCHQt.GetQt("蛊毒法"))
		{
			return -233;
		}
		if(PvPSCHSettings.Instance.仅秘策使用)
		{
			if(!Core.Me.HasAura(3094u))
			{ 
				return -10;
			}
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.距离大于25米远程范围())
		{
			return -5;
		}
		if (!SpellHelper.IsReady(29233u))
		{
			return -2;
		}
		if (PvPSCHSettings.Instance.自动扩毒)
		{
			if (Core.Me.ObjectId==PVPHelper.Get最佳毒目标().ObjectId)
			{
				return -87;
			}
		}
		if (!PvPSCHSettings.Instance.自动扩毒)
		{
			if(Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3089u,6000))
			{
				return -444;
			}
		}
		return 0;
	}
	
	public void Build(Slot slot)
	{
		if (PvPSCHSettings.Instance.自动扩毒)
		{
			slot.Add(new Spell(29233u,PVPHelper.Get最佳毒目标()));
		}
		else
		{
			slot.Add(SpellHelper.GetSpell(29233u));
		}

	}
}

