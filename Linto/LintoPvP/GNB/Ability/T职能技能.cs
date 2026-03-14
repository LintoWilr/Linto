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
	private const uint ActionId = 43259u;
	
	public int Check()//43259 职能技能
	{
		var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(ActionId);
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPGNBOverlay.GNBQt.GetQt("职能技能"))
		{
			return -9;
		}
		if (!changedAction.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (changedAction==43243u)
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
		if (changedAction==43245u)
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
		var changedAction = Core.Resolve<MemApiSpell>().CheckActionChange(ActionId);
		if (changedAction==43244u)
		{
			slot.Add(new Spell(changedAction, Core.Me));
			
		}
		else if(changedAction==43243u)
		{
			PVPHelper.通用技能释放(slot,changedAction,10);
		}
		else
		{
			PVPHelper.通用技能释放(slot,changedAction,5);
		}
	}
}
