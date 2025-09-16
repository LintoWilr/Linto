using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace Linto.Sage.Ability;

public class SGEAbility_心神风息 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)2;


	public int Check()
	{
		if (Qt.GetQt("小停一下"))
		{
			return -1;
		}
		if (((ICharacter)Core.Me).Level < 92)
		{
			return -3;
		}
		if (!SpellExtension.IsUnlockWithCDCheck(37033u))
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(SpellHelper.GetSpell(37033u));
	}
}
