using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 空气锚 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29407u;
	private const uint RequiredAura = 3152u;
	private const int SkillRange = 25;
	private const uint 分析Buff = 3158u;
	private const uint 分析技能 = 29414u;

	public int Check()
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if(!PvPMCHOverlay.MCHQt.GetQt("空气锚"))
		{
			return -233; 
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (GCDHelper.GetGCDCooldown()>200)
		{
			return -3;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		if (!Core.Me.HasAura(RequiredAura))
		{
			return -2;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast()||Core.Resolve<MemApiSpell>().CheckActionChange(SkillId)!=SkillId)
		{
			return -2;
		}
		if (PvPMCHSettings.Instance.空气锚分析)
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
