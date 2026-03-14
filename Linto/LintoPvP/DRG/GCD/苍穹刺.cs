using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.GCD;

public class 苍穹刺 : ISlotResolver 
{
	public SlotMode SlotMode { get; }
	private const uint SkillId = 29489u;
	private const uint 苍穹Aura = 3176u;
	private const uint 龙血Aura = 3177u;
	public int Check()//苍穹刺 29489
	{
		if(!DRGQt.GetQt("苍穹刺"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (GCDHelper.GetGCDCooldown()>600)
		{
			return -3;
		}
		if (PVPHelper.通用距离检查(DRGSkillID.苍穹刺距离))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(DRGSkillID.苍穹刺,DRGSkillID.苍穹刺距离)==null)
		{
			return -6 ;
		}
		if (!Core.Me.HasAura(苍穹Aura))
		{
			return -10;
		}
		if (PvPDRGSettings.Instance.苍穹刺龙血)
		{
			if (!Core.Me.HasAura(龙血Aura))
			{
				return -8;
			}
		}
        return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,DRGSkillID.苍穹刺,DRGSkillID.苍穹刺距离);
	}
}
