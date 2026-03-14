using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 回转飞锯 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29408u;
	private const int SkillRange = 25;
	private const uint RequiredAura = 3153u;
	private const uint 分析Buff = 3158u;
	private const uint 分析技能 = 29414u;
	public uint 机工变化()
	{
		return Core.Resolve<MemApiSpell>().CheckActionChange(SkillId);
	}
	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!PvPMCHOverlay.MCHQt.GetQt("回转飞锯"))
		{
			return -233; 
		}
		if (GCDHelper.GetGCDCooldown()>200)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		var changedSkill = 机工变化();
		if (PVPHelper.通用技能释放Check(changedSkill,SkillRange)==null)
		{
			return -6 ;
		}
		if (changedSkill!=SkillId||!Core.Me.HasAura(RequiredAura)||!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (PvPMCHSettings.Instance.回转飞锯分析)
		{
			if (!Core.Me.HasAura(分析Buff))
			{
				if (SpellHelper.GetSpell(分析技能).Charges > 0.5)
				{
					return -23;
				}
			}
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,SkillId,SkillRange);
	}
}

