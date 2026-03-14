using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 分析 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29414u;
	private const uint 分析中光环 = 3158u;
	private const uint 分析可用技能 = 29405u;
	private const int SkillRange = 25;
	public int Check()
	{
		if (!PvPMCHOverlay.MCHQt.GetQt("分析"))
		{
			return -9;
		}
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (PVPHelper.通用距离检查(SkillRange))
		{
			return -5 ;
		}
		if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
		{
			return -6 ;
		}
		if (SpellHelper.GetSpell(SkillId).Charges < 1 )
		{
			return -1;
		}
		if (PvPMCHSettings.Instance.分析可用)
		{
			if (!分析可用技能.GetSpell().IsReadyWithCanCast())
			{
				return -8;
			}
		}
		if (Core.Me.HasAura(分析中光环))
		{
			return -99;
		}
		if(Core.Resolve<MemApiSpellCastSuccess>().LastSpell==SkillId)
		{
			return -99;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(PVPHelper.等服务器Spell(SkillId,Core.Me));
	}
}
