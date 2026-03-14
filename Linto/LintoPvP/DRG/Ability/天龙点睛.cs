
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 天龙点睛 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29495u;
	private const uint 天龙Aura = 3178u;
	private const uint 龙血Aura = 3177u;
	public static bool 龙血()=>PvPDRGSettings.Instance.天龙龙血;
	public int Check()//29495 天龙点睛
	{
		if (!DRGQt.GetQt("天龙点睛"))
		{
			return -9;
		}
		if (!SkillId.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (!PVPHelper.CanActive())
		{
			return -3;
		}
		if (!Core.Me.HasAura(天龙Aura))
		{
			return -10;
		}
		if (龙血())
		{
			if (!Core.Me.HasAura(龙血Aura))
			{
				return -8;
			}
		}
		if (!PVPHelper.通用距离检查(PvPDRGSettings.Instance.天龙点睛主目标距离))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(DRGSkillID.天龙点睛,DRGSkillID.天龙点睛距离)==null)
		{
			return -6 ;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,DRGSkillID.天龙点睛,DRGSkillID.天龙点睛距离);
	}
}
