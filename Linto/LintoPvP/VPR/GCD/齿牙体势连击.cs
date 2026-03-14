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
	private const uint 技能连击1咬噬尖齿 = 39157u;
	private const uint 技能连击2猛袭利齿 = 39159u;
	private const uint 技能连击3咬击獠齿 = 39161u;
	private const uint 技能连击4切割尖齿 = 39158u;
	private const uint 技能连击5疾速利齿 = 39160u;
	private const uint 技能连击6啮噬獠齿 = 39163u;
	private const uint LB技能 = 39190u;
	private const int 连击距离 = 5;
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
		if(LB技能.RecentlyUsed(1500))
		{
			return -44;
		}
		if (PVPHelper.通用距离检查(连击距离))
		{
			return -5 ;
		}
		return 1;
	}
	
	public void Build(Slot slot)
	{
		var lastCombo = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
		//普攻连击6
		if (lastCombo == 技能连击5疾速利齿)
		{
			PVPHelper.通用技能释放(slot,技能连击6啮噬獠齿,连击距离);
		}
		//普攻连击5
		else if (lastCombo == 技能连击4切割尖齿)
		{
			PVPHelper.通用技能释放(slot,技能连击5疾速利齿,连击距离);
		}
		//普攻连击4
		else if (lastCombo == 技能连击3咬击獠齿)
		{
			PVPHelper.通用技能释放(slot,技能连击4切割尖齿,连击距离);
		}
		//普攻连击3
		else if (lastCombo == 技能连击2猛袭利齿)
		{
			PVPHelper.通用技能释放(slot,技能连击3咬击獠齿,连击距离);
		}
		//普攻连击2
		else if (lastCombo == 技能连击1咬噬尖齿)
		{
			PVPHelper.通用技能释放(slot,技能连击2猛袭利齿,连击距离);
		}
		//普攻连击1
		else
		{
			PVPHelper.通用技能释放(slot,技能连击1咬噬尖齿,连击距离);
		}
	}
}
