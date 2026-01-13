using CombatRoutine;

namespace Linto.JobView;

public interface IHotkeySlotResolver
{
	bool UseMoTarget { get; }

	uint SpellId { get; }

	string ImgPath { get; }

	int Check(HotkeyControl hotkey);

	void Build(Slot slot, HotkeyControl hotkey);
}
