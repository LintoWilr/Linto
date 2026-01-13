using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 野火 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("野火"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!29409u.GetSpell().IsReadyWithCanCast())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(12))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(29409,12)==null)
		{
			return -6 ;
		}
		if (PvPMCHSettings.Instance.过热野火)
		{
			if (!Core.Me.HasAura(3149u))
			{
				return -9;
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29409u,12);
	}
}
