
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 烈牙连击 : ISlotResolver
{
	public SlotMode SlotMode { get; }

	public int Check()//29102 烈牙
	{
		var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(29102);
		if(!PvPGNBOverlay.GNBQt.GetQt("烈牙连击"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!changedAction.GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(5))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(changedAction,5)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(29102);
		PVPHelper.通用技能释放(slot,changedAction,5);
	}
}
