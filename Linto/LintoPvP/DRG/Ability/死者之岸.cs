using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 死者之岸 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint 龙血Aura = 3177u;
	private const uint 天龙Aura = 3178u;
	private const uint 苍穹Aura = 3176u;
	public int Check()//29492 死者之岸 
	{
		if(!DRGQt.GetQt("死者之岸"))
		{
			return -1;
		}
		if (!Core.Me.HasAura(龙血Aura))
		{
			return -8;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (!DRGSkillID.死者之岸.GetSpell().IsReadyWithCanCast())
		{
			return -3;
		}
		if ((PvPDRGSettings.Instance.死者之岸樱花&&DRGSkillID.樱花缭乱.GetSpell().IsReadyWithCanCast())||
		    (PvPDRGSettings.Instance.死者之岸天龙&&Core.Me.HasAura(天龙Aura))||
		    (PvPDRGSettings.Instance.死者之岸苍穹刺&&Core.Me.HasAura(苍穹Aura)))
		{
			return -8;
		}
		if (PVPHelper.通用距离检查(15))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(DRGSkillID.死者之岸,DRGSkillID.死者之岸距离)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,DRGSkillID.死者之岸,DRGSkillID.死者之岸距离);
	}
}
