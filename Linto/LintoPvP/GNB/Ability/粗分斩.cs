using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;
namespace Linto.LintoPvP.GNB.Ability;

public class 粗分斩 : ISlotResolver
{
	public SlotMode SlotMode { get; } = SlotMode.Always;

	public int Check()//29123 粗分斩
	{
		if (!PVPHelper.CanActive())
		{
			return -1;
		}
		if (!PvPGNBOverlay.GNBQt.GetQt("粗分斩"))
		{
			return -9;
		}
		if (SpellHelper.GetSpell(29123u).Charges < 1 )
		{
			return -1;
		}
		if (!29123u.GetSpell().IsReadyWithCanCast())
		{
			return -2;
		}
		if (Core.Me.HasLocalPlayerAura(3042u))//无情检查
		{
			return -3;
		} 
		if (PVPHelper.通用距离检查(PvPGNBSettings.Instance.粗分斩最大距离))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(29123u,PvPGNBSettings.Instance.粗分斩最大距离)==null)
		{
			return -5;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		PVPHelper.通用技能释放(slot,29123u,PvPGNBSettings.Instance.粗分斩最大距离);
	}
}
