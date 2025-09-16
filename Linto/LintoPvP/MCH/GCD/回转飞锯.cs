using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 回转飞锯 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 机工变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(29408u);
	}
	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!PvPMCHOverlay.MCHQt.GetQt("回转飞锯"))
		{
			return -233; 
		}
		if (GCDHelper.GetGCDCooldown()>200)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(机工变化(),25)==null)
		{
			return -6 ;
		}
		if (机工变化()!=29408u||!Core.Me.HasAura(3153)||!29408u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PvPMCHSettings.Instance.回转飞锯分析)
		{
			if (!Core.Me.HasAura(3158))
			{
				if (SpellHelper.GetSpell(29414u).Charges > 0.5)
				{
					return -23;
				}
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29408u,25);
	}
}

