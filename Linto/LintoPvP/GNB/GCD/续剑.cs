using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 续剑 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check() //29106 续剑
	{
		if(!PvPGNBOverlay.GNBQt.GetQt("续剑"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(5))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(Core.Resolve<MemApiSpell>().CheckActionChange(29106u),5)==null)
		{
			return -5;
		}
		if (!Core.Me.HasLocalPlayerAura(3041) && !Core.Me.HasLocalPlayerAura(2002)  &&
		    !Core.Me.HasLocalPlayerAura(2003) && !Core.Me.HasLocalPlayerAura(2004)&&
		    !Core.Me.HasLocalPlayerAura(4293))
			//3041 超高速预备 2002-2004 后面几个Buff 4293 命运之印
		{
			return -6;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		if(Core.Me.HasLocalPlayerAura(4293))
			slot.Add(SpellHelper.GetSpell(Core.Resolve<MemApiSpell>().CheckActionChange(29106u),SpellTargetType.Self));
		PVPHelper.通用技能释放(slot,Core.Resolve<MemApiSpell>().CheckActionChange(29106u),5);
	}
	
}
