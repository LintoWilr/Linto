using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.GCD;

public class BUFF连击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能猛袭灵蛇= 39166u;
	public uint 技能疾速盘蛇= 39167u;
	public uint 技能祖灵大蛇牙= 39173u;
	public uint 技能祖灵之蛇四式= 39182u;
	public uint 技能祖灵之牙四式= 39172u;
	private int 技能距离 => 3 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
	public uint 灵蛇连击()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(39166u);
	}
	public int Check() 
	{
		if(!PvPVPROverlay.VPRQt.GetQt("BUFF连击"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(5))
		{
			return -5 ;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if(Core.Me.HasAura(4094u))//开大
		{
			if(技能祖灵之牙四式.RecentlyUsed(12000)&技能祖灵之蛇四式.RecentlyUsed(12000))//开大
			{
				return 1;
			}
			return -3;
		}

		if (灵蛇连击() == 技能疾速盘蛇)
		{
			return 1;
		}
		if (Core.Resolve<MemApiSpell>().GetCharges(技能猛袭灵蛇)<1f)
		{
			return -4;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,灵蛇连击(),5);
	}
}
