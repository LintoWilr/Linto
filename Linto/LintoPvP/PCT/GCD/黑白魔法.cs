using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.GCD;

public class 黑白魔法 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public uint 技能神圣之白 = 39198u;
	public uint 技能彗星之黑 = 39199u;
	public uint buff减色混合 = 4102u;
	public int Check()
	{
		if(!PvPPCTOverlay.PCTQt.GetQt("黑白Aoe"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -9;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(技能神圣之白,25)==null)
		{
			return -6 ;
		}
		if (Core.Resolve<MemApiSpell>().GetCharges(技能神圣之白)<1f)
		{
			return -10;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		if (Core.Me.HasLocalPlayerAura(buff减色混合))
		{
			PVPHelper.通用技能释放(slot,技能彗星之黑,25);
		}
		else
		{
			PVPHelper.通用技能释放(slot,技能神圣之白,25);
		}
	}
}
