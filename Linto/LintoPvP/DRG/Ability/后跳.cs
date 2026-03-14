using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 后跳 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;
	private const uint SkillId = 29494u;
	private const uint 龙血Aura = 3177u;

	bool 后跳解控()
	{
		return PvPDRGSettings.Instance.后跳解除控制;
	}
	public int Check()//29494 后跳
	{
		if (!DRGQt.GetQt("后跳"))
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
		if (后跳解控())
		{
			// 命中这些控制类状态时，允许后跳优先做解控用途。
			if(Core.Me.HasAura(2518)||Core.Me.HasAura(3134)||Core.Me.HasAura(3457))
				return 1;
		}
		if (!Core.Me.HasLocalPlayerAura(龙血Aura))
		{
			return -8;
		}
		return 1;
	}

	public void Build(Slot slot)
	{
		if (PvPDRGSettings.Instance.后跳面向)
		{
			Core.Resolve<MemApiMove>().SetRot(PVPHelper.GetCameraRotation反向());
			slot.Add(new Spell(SkillId,PVPHelper.向量位移(Core.Me.Position, PVPHelper.GetCameraRotation(), 15)));
		}
		//	Core.Resolve<MemApiSpell>().Cast(DRGSkillID.后跳, PVPHelper.向量位移(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
		else slot.Add(SpellHelper.GetSpell(SkillId,SpellTargetType.Self));
	}
}
