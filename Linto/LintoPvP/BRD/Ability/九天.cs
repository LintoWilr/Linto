using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.Ability;

public class 九天连箭 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 41464u;
	private const int SkillRange = 25;
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
		if (SpellHelper.GetSpell(SkillId).Charges < PvPBRDSettings.Instance.和弦箭 )
		{
			return -1;
		}
		if (SpellHelper.GetSpell(SkillId).Charges < 1 )
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
public class 英雄的返场余音 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 41467u;
	private const int SkillRange = 25;
	private const uint RequiredAura = 4312u;
	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!Core.Me.HasAura(RequiredAura))
		{
			return -2;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}
