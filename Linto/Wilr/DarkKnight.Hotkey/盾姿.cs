using CombatRoutine;
using Common.Define;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class 盾姿 : IHotkeySlotResolver
{
	public bool UseMoTarget => false;

	public uint SpellId => Spell.CreateShield().Id;

	public string ImgPath => "";

	public int Check(HotkeyControl hotkey)
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(Spell.CreateShield()))
		{
			return -1;
		}
		return 0;
	}

	public void Build(Slot slot, HotkeyControl hotkey)
	{
		slot.Add(SpellHelper.GetSpell(SpellId));
	}
}
