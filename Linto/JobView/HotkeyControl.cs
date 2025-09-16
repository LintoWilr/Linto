using CombatRoutine;
using Common.Define;

namespace Linto.JobView;

public class HotkeyControl
{
	internal readonly string Name;

	internal IHotkeySlotResolver Slot;

	internal string ToolTip = "";

	internal string img = "";

	internal bool useMo;

	internal bool IsNextSlot;

	public CharacterAgent? MoTarget;

	internal Spell spell
	{
		get
		{
			if (Slot.SpellId != 0)
			{
				return SpellHelper.GetSpell(Slot.SpellId);
			}
			return Spell.CreatePotion();
		}
	}

	internal HotkeyControl(string name)
	{
		Name = name;
	}

	internal void Reset()
	{
		IsNextSlot = false;
	}
}
