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
	private const uint 技能猛袭灵蛇 = 39166u;
	private const uint 技能疾速盘蛇 = 39167u;
	private const uint 技能祖灵大蛇牙 = 39173u;
	private const uint 技能祖灵之蛇四式 = 39182u;
	private const uint 技能祖灵之牙四式 = 39172u;
	private const uint 灵蛇变化起点 = 39166u;
	private const uint 开大Buff = 4094u;
	private const int 连击距离 = 5;
	private int 技能距离 => 3 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
	public uint 灵蛇连击()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(灵蛇变化起点);
	}
	public int Check() 
	{
		var changedSkill = 灵蛇连击();
		if(!PvPVPROverlay.VPRQt.GetQt("BUFF连击"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(连击距离))
		{
			return -5 ;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if(Core.Me.HasAura(开大Buff))//开大
		{
			if(技能祖灵之牙四式.RecentlyUsed(12000)&技能祖灵之蛇四式.RecentlyUsed(12000))//开大
			{
				return 1;
			}
			return -3;
		}

		if (changedSkill == 技能疾速盘蛇)
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
		var changedSkill = 灵蛇连击();
		PVPHelper.通用技能释放(slot,changedSkill,连击距离);
	}
}
