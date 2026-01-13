using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.GCD;

public class 完美音调 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if (!PvPBRDOverlay.BRDQt.GetQt("强劲射击"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!29392u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (!Core.Me.HasAura(3137u))
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(29392u,25)==null)
		{
			return -6;
		}
		
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29392u,25);
	}
}

