using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.Ability;
public class 切换减色 : ISlotResolver
{
	public uint 减色状态()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能减色混合);
	}
	public int Check()
	{
		if (!PvPPCTOverlay.PCTQt.GetQt("切换减色"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (GCDHelper.GetGCDCooldown()<600)
		{
			return -3;
		}
		if(PCTSkillID.技能减色混合.RecentlyUsed(PvPPCTSettings.Instance.减色切换)||PCTSkillID.技能解除减色混合.RecentlyUsed(PvPPCTSettings.Instance.减色切换))
		{
			return -5;
		}
		if (Core.Me.HasLocalPlayerAura(PCTSkillID.buff减色混合)) 
		{
			if (Core.Me.IsMoving())
				return 1;
			return -9;
		}
		if (!Core.Me.HasLocalPlayerAura(PCTSkillID.buff减色混合)) 
		{
			if (!Core.Me.IsMoving())
				return 1;
			return -9;
		}
		return -9;
	}
	public void Build(Slot slot)
	{
		if (PvPSettings.Instance.技能自动选中)
		{
			slot.Add(PVPHelper.等服务器Spell(减色状态(),Core.Me));
		}
		else
		{
			slot.Add(PVPHelper.等服务器Spell(减色状态(),Core.Me));
		}
	}
}
