using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.Sage;

namespace 残光.贤者.能力技;

public class SGEAbility_根素 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)2;


	public int Check()
	{
		if (Qt.GetQt("小停一下"))
		{
			return -1;
		}
		if (!SGESettings.Instance.防根素溢出)
		{
			return -100;
		}
		if (((ICharacter)Core.Me).Level < 76)
		{
			return -3;
		}
		if (!SpellExtension.IsUnlockWithCDCheck(24309u))
		{
			return -1;
		}
		if (Core.Resolve<JobApi_Sage>().Addersgall >= 2)
		{
			return -2;
		}
		if (Core.Resolve<JobApi_Sage>().Addersgall < 2)
		{
			return 2;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(24309u));
	}
}
