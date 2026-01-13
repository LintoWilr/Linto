using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.Ability;

public class 九天连箭 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public int Check()
	{
		if (!PvPBRDOverlay.BRDQt.GetQt("和弦箭"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (SpellHelper.GetSpell(41464u).Charges < PvPBRDSettings.Instance.和弦箭 )
		{
			return -1;
		}
		if (SpellHelper.GetSpell(41464u).Charges < 1 )
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(41464u,25)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41464u,25);
	}
}
public class 英雄的返场余音 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!Core.Me.HasAura(4312u))
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(41467u,25)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,41467u,25);
	}
}