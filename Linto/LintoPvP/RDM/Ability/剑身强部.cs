using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability
{
	public class 剑身强部 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;
		private const uint SkillId = 41496u;
		public int Check()
		{
			if (!PvPRDMOverlay.RDMQt.GetQt("剑身强部"))
			{
				return -9;
			}
			if (!PVPHelper.CanActive())
			{
				return -3;
			}
			if (!SkillId.GetSpell().IsReadyWithCanCast())
			{
				return -2;
			}
			int aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 1,PvPRDMSettings.Instance.护盾距离);
			if (aoeCount < PvPRDMSettings.Instance.护盾人数)
			{
				return -5;
			}
			return 1;
		}

		public void Build(Slot slot)
		{
			slot.Add(PVPHelper.等服务器Spell(SkillId,Core.Me));
		}
	}
	public class 荆棘环绕 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;
		private const uint SkillId = 41493u;
		private const uint RequiredAura = 4321u;
		private const int SkillRange = 25;
		public int Check()
		{
			if (!PvPRDMOverlay.RDMQt.GetQt("剑身强部"))
			{
				return -9;
			}
			if (!PVPHelper.CanActive())
			{
				return -3;
			}
			if (!Core.Me.HasAura(RequiredAura))
			{
				return -3;
			}
			if (PVPHelper.通用距离检查(SkillRange))
			{
				return -5 ;
			}
			if (PVPHelper.通用技能释放Check(SkillId,SkillRange)==null)
			{
				return -6 ;
			}
			return 1;
		}
		public void Build(Slot slot)
		{
			PVPHelper.通用技能释放(slot,SkillId,SkillRange);
		}
	}
}
