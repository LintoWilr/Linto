
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 残暴弹 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()//29099
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
		if (Core.Resolve<MemApiSpell>().CheckActionChange(29102)!=29102u)
		{
			return -4;
		}
		if (!Core.Resolve<MemApiSpell>().CheckActionChange(29099).GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		if (PVPHelper.通用技能释放Check(29099u,5)==null)
		{
			return -5;
		}
		if (Core.Resolve<MemApiSpell>().GetLastComboSpellId()!=29098u)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29099u,5);
	}
}