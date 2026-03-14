using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;
public class 速度之星 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 速度之星u = 43249u;
	private const uint 速度之星buff = 4489u;
	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("职能技能"))
		{
			return -9;
		}
		if (!Core.Me.HasAura(速度之星buff))
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!速度之星u.GetSpell().IsReadyWithCanCast())
		{
			return -1;
		}
		if (PVPHelper.check坐骑())
		{
			return -5;
		}
		return 0;
	}
	public void Build(Slot slot)
	{
		slot.Add(new Spell(速度之星u,Core.Me));
	}
}
public class 勇气 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 勇气u = 43250u;
	private const uint 勇气释放buff = 4490u;
	private const uint 勇气buff = 4479u;
	private const uint 钻头基础技能 = 29408u;
	private const uint 钻头可用变化1 = 29405u;
	private const uint 钻头可用变化2 = 29408u;
	private const uint 勇气前置光环 = 3153u;
	public uint 钻头变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(钻头基础技能);
	}
	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("职能技能"))
		{
			return -9;
		}
		if (!Core.Me.HasAura(勇气释放buff))
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!勇气u.GetSpell().IsReadyWithCanCast())
		{
			return -1;
		}
		if (PVPHelper.check坐骑())
		{
			return -5;
		}
		var changedSkill = 钻头变化();
		if (!(changedSkill==钻头可用变化1||
		      changedSkill==钻头可用变化2))
		{
			return -5;
		}
		if (!Core.Me.HasAura(勇气前置光环)||changedSkill.GetSpell().Charges < 0.5)
		{
			return -2;
		}
		return 0;
	}
	public void Build(Slot slot)
	{
		slot.Add(new Spell(勇气u,Core.Me));
	}
}
