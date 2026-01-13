using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.GCD;

public class 齿牙体势连击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能连击1咬噬尖齿 = 39157u;
	public uint 技能连击2猛袭利齿 = 39159u;
	public uint 技能连击3咬击獠齿 = 39161u;
	public uint 技能连击4切割尖齿 = 39158u;
	public uint 技能连击5疾速利齿 = 39160u;
	public uint 技能连击6啮噬獠齿 = 39163u;
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
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}

		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -4;
		}
		if(39190u.RecentlyUsed(1500))
		{
			return -44;
		}
		if (PVPHelper.通用距离检查(5))
		{
			return -5 ;
		}
		return 1;
	}
	
	public void Build(Slot slot)
	{
		//普攻连击6
		if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 技能连击5疾速利齿))
		{
			PVPHelper.通用技能释放(slot,技能连击6啮噬獠齿,5);
		}
		//普攻连击5
		else if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 技能连击4切割尖齿))
		{
			PVPHelper.通用技能释放(slot,技能连击5疾速利齿,5);
		}
		//普攻连击4
		else if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 技能连击3咬击獠齿))
		{
			PVPHelper.通用技能释放(slot,技能连击4切割尖齿,5);
		}
		//普攻连击3
		else if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 技能连击2猛袭利齿))
		{
			PVPHelper.通用技能释放(slot,技能连击3咬击獠齿,5);
		}
		//普攻连击2
		else if ((Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 技能连击1咬噬尖齿))
		{
			PVPHelper.通用技能释放(slot,技能连击2猛袭利齿,5);
		}
		//普攻连击1
		else
		{
			PVPHelper.通用技能释放(slot,技能连击1咬噬尖齿,5);
		}
	}
}