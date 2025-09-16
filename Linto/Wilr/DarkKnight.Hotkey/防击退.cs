using CombatRoutine;
using Common.Define;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class 防击退 : IHotkeySlotResolver
{
	public bool UseMoTarget => false;

	public uint SpellId => 7548u;

	public string ImgPath => "Resources\\Spells\\TankRoleActions\\ArmsLength.png";

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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		slot.Add(new Spell(SpellId, (SpellTargetType)1));
	}
}
