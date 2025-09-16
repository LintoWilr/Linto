using System.ComponentModel;
using CombatRoutine;
using Common;
using Common.Define;
using Common.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH.Ability;

public class 毒扩散 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!SCHQt.GetQt("扩散"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		var 扩毒数量 = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25,15);
		if (PvPSCHSettings.Instance.上毒优先开启)
		{
			if (PvPSCHSettings.Instance.自动扩毒)
			{
				if (Core.Me.ObjectId!=PVPHelper.Get最佳扩散目标().ObjectId)
				{
					if(SpellHelper.GetSpell(29234u).Charges >=1.4) 
					{ 
						return 4; 
					} 
				}
			}
			else
			{
				if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3089u,6000) 
			     && 扩毒数量 >= PvPSCHSettings.Instance.扩散敌人数量) 
				{ 
					if(SpellHelper.GetSpell(29234u).Charges >=1.3) 
					{ 
						return 0; 
					} 
				}
				
			}
		}
		if (!PvPSCHSettings.Instance.上毒优先开启 && !PvPSCHSettings.Instance.鼓励优先开启)
		{
			if  (!Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3089u,6000) 
			     && 扩毒数量 >= 3)
			{
				if (SpellHelper.GetSpell(29234u).Charges >=1.9)
				{
					return 0;
				}
			}
		}
		return -3;
	}
	public void Build(Slot slot)
	{
		if (PvPSCHSettings.Instance.自动扩毒)
		{
			slot.Add(new Spell(29234u,PVPHelper.Get最佳扩散目标()));
		}
		else
		{
			slot.Add(SpellHelper.GetSpell(29234,SpellTargetType.Target));
		}
	}
}

public class 鼓励扩散 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;


	public int Check()
	{
		if(!PVPHelper.通用权限())
		{
			return -999999;
		}
		if (!SCHQt.GetQt("扩散"))
		{
			return -9;
		}
		if(MoveHelper.IsMoving())
		{
			return -10;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		var 扩毒数量 = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25,15);
		var 鼓励数量 = PartyHelper.CastableAlliesWithin15.Count;
		if (PvPSCHSettings.Instance.鼓励优先开启)
		{
			if (Core.Me.HasMyAuraWithTimeleft(3087u,6000) && 鼓励数量 >= PvPSCHSettings.Instance.鼓励队友数量)
			{
				if ( SpellHelper.GetSpell(29234u).Charges > 1)
				{
					return 0;
				}
			}
		}
		if (!PvPSCHSettings.Instance.上毒优先开启 && !PvPSCHSettings.Instance.鼓励优先开启)
		{
			if (SpellHelper.GetSpell(29234u).Charges >=1.9 )
			{
				if (Core.Me.HasMyAuraWithTimeleft(3087u,6000) && 鼓励数量 >= 3)
				{
					return 0;
				}
			}
		}
		return -3;
	}
	public void Build(Slot slot)
	{
		slot.Add(new Spell(29234,(SpellTargetType)1));
	}
}
