using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 爆发击 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check() //29101 爆发击
	{
		if(!PvPGNBOverlay.GNBQt.GetQt("爆发击连击"))
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
		if (PVPHelper.通用技能释放Check(29101u,5)==null)
		{
			return -5;
		}
		if (Core.Resolve<MemApiSpell>().CheckActionChange(29102)!=29102u)
		{
			return -4;
		}
		if (PVPHelper.通用技能释放Check(29101u,5)==null)
		{
			return -5;
		}
		if (!Core.Resolve<MemApiSpell>().CheckActionChange(29101).GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		if (Core.Resolve<MemApiSpell>().GetLastComboSpellId()!=29100u)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29101u,5);
	}
}
