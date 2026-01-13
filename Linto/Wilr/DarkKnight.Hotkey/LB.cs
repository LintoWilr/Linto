using CombatRoutine;
using Common.Define;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class LB : IHotkeySlotResolver
{
	public bool UseMoTarget => false;

	public uint SpellId => Spell.CreateLimitBreak().Id;

	public string ImgPath => "Resources\\Spells\\LB.png";

	public int Check(HotkeyControl hotkey)
	{
		if (!SpellHelper.IsReady(Spell.CreateLimitBreak()))
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot, HotkeyControl hotkey)
	{
		slot.Add(Spell.CreateLimitBreak());
	}
}
