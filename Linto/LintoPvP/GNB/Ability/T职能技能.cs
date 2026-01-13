using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SMN;

namespace Linto.LintoPvP.GNB.Ability;

public class T职能技能 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	
	public int Check()//43259 职能技能
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPGNBOverlay.GNBQt.GetQt("职能技能"))
		{
			return -9;
		}
		if (!Core.Resolve<MemApiSpell>().CheckActionChange(43259u).GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (Core.Resolve<MemApiSpell>().CheckActionChange(43259u)==43243u)
		{
			if (PVPHelper.通用距离检查(10))
			{
				return -5;
			}
			if (PVPHelper.通用技能释放Check(43243u,10)==null)
			{
				return -5;
			}
		}
		if (Core.Resolve<MemApiSpell>().CheckActionChange(43259u)==43245u)
		{
			if (PVPHelper.通用距离检查(5))
			{
				return -5;
			}
			if (PVPHelper.通用技能释放Check(43245u,5)==null)
			{
				return -5;
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		if (Core.Resolve<MemApiSpell>().CheckActionChange(43259u)==43244u)
		{
			slot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(43259u), Core.Me));
			
		}
		else if(Core.Resolve<MemApiSpell>().CheckActionChange(43259u)==43243u)
		{
			PVPHelper.通用技能释放(slot,Core.Resolve<MemApiSpell>().CheckActionChange(43259u),10);
		}
		else
		{
			PVPHelper.通用技能释放(slot,Core.Resolve<MemApiSpell>().CheckActionChange(43259u),5);
		}
	}
}
