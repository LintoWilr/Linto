using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.GCD;

public class 读条连击 : ISlotResolver
{
	public SlotMode SlotMode { get; }
	public int Check()
	{
		if(!PvPPCTOverlay.PCTQt.GetQt("读条连击"))
		{
			return -1;
		}
		if (!PVPHelper.CanActive())
		{
			return -2;
		}
		if (!Core.Me.HasLocalPlayerAura(PCTSkillID.buff减色混合))
		{
			return -3;
		}
		if (GCDHelper.GetGCDCooldown()>0)
		{
			return -4;
		}
		if (Core.Me.IsMoving())
		{
			return -5;
		}
		if (PVPHelper.通用距离检查(25))
		{
			return -5;
		}
		if (PVPHelper.通用技能释放Check(PCTSkillID.技能连击1火炎之红,25)==null)
		{
			return -6 ;
		}
		return 1;
	}
	
	public void Build(Slot slot)
	{
		//读条3闪雷
		if ((Core.Me.HasLocalPlayerAura(PCTSkillID.buff以太色调II)))
		{
			PVPHelper.通用技能释放(slot,PCTSkillID.技能3闪雷之品红,25);
		}
		//瞬发2疾风
		else if  ((Core.Me.HasLocalPlayerAura(PCTSkillID.buff以太色调)))
		{
			PVPHelper.通用技能释放(slot,PCTSkillID.技能2飞石之纯黄,25);
		}
		//瞬发1火炎
		else
		{
			PVPHelper.通用技能释放(slot,PCTSkillID.技能1冰结之蓝青,25);
		}
	}
}