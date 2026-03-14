using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability
{
	public class 鼓励 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;
		private const uint SkillId = 41494u;
		public int Check()
		{

			if (!PvPRDMOverlay.RDMQt.GetQt("鼓励"))
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
			var aoeCount = PartyHelper.CastableAlliesWithin30.Count;
			if (aoeCount >= PvPRDMSettings.Instance.鼓励人数)
			{
				return 1;
			}
			return -25;
		}

		public void Build(Slot slot)
		{
			slot.Add(PVPHelper.等服务器Spell(SkillId,Core.Me));
		}
	}
	public class 光芒四射 : ISlotResolver
	{
		public SlotMode SlotMode { get; } = SlotMode.Always;
		private const uint SkillId = 41495u;
		private const uint RequiredAura = 4322u;
		private const uint 鼓励来源Aura = 2282u;
		private const int SkillRange = 25;
		public int Check()
		{
			if (!PvPRDMOverlay.RDMQt.GetQt("光芒四射"))
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
			if(PvPRDMSettings.Instance.鼓励光芒四射&&!Core.Me.HasLocalPlayerAura(鼓励来源Aura))
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
