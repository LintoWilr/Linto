using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.GCD;

public class 祖灵之牙连击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能连击1祖灵之牙一式= 39169u;
	public uint 技能连击2祖灵之牙二式= 39170u;
	public uint 技能连击3祖灵之牙三式= 39171u;
	public uint 技能连击4祖灵之牙四式= 39172u;
	public uint 技能祖灵之蛇四式= 39182u;
	public uint 技能祖灵之牙四式= 39172u;
	private int 技能距离 => 3 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
	// public uint 技能满月 = 29527u;
	// public uint 技能樱花 = 29528u;
	// public uint 技能雪释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u);
	// public uint 技能月释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29524u);
	// public uint 技能花释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29525u);
	// public bool 单体连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u;
	// public bool Aoe连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29526u;
	// public bool 雪连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29523u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29526u);
	// public bool 月连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29524u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29527u);
	// bool 花连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29525u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29528u);
	// public uint 技能早天 = 29532u;
	public int Check()
	{
		if(!PvPVPROverlay.VPRQt.GetQt("连击"))
		{
			TargetHelper.GetEnemyCountInsideRect(Core.Me, Core.Me.GetCurrTarget(), 5, 5);
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (!Core.Me.HasLocalPlayerAura(4094u))//开大
		{
			return -3;
		}
		if (GCDHelper.GetGCDCooldown()>1000)
		{
			return -4;
		}
		if (PVPHelper.通用距离检查(技能距离))
		{
			return -5 ;
		}
		if(39190u.RecentlyUsed(800))
		{
			return -44;
		}
		if(技能祖灵之牙四式.RecentlyUsed(12000)&&技能祖灵之蛇四式.RecentlyUsed(12000))//开大
		{
			return -5;
		}
		return 1;
	}
	
	public void Build(Slot slot)
	{
		//LB连击4
		if ((Core.Resolve<MemApiSpellCastSuccess>().LastGcd== 技能连击3祖灵之牙三式))
		{
			PVPHelper.通用技能释放(slot,技能连击4祖灵之牙四式,5);
		}
		//LB连击3
		else if (Core.Resolve<MemApiSpellCastSuccess>().LastGcd == 技能连击2祖灵之牙二式)
		{
			PVPHelper.通用技能释放(slot,技能连击3祖灵之牙三式,5);
		}
		//LB连击2
		else if (Core.Resolve<MemApiSpellCastSuccess>().LastGcd==技能连击1祖灵之牙一式)
		{
			PVPHelper.通用技能释放(slot,技能连击2祖灵之牙二式,5);
		}
		//LB连击1
		else
		{
			PVPHelper.通用技能释放(slot,技能连击1祖灵之牙一式,5);
		}
	}
	
}