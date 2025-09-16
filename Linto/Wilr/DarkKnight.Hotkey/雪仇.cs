using CombatRoutine;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class 雪仇 : IHotkeySlotResolver
{
	public bool UseMoTarget => false;

	public uint SpellId => 7535u;

	public string ImgPath => "Resources\\Spells\\TankRoleActions\\Reprisal.png";

	public int Check(HotkeyControl hotkey)
	{
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(SpellId))
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
