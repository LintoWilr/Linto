using CombatRoutine;
using Common.Define;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class 黑盾 : IHotkeySlotResolver
{
	public bool UseMoTarget => true;

	public uint SpellId => 7393u;

	public string ImgPath => "Resources\\Spells\\DarkKnight\\TheBlackestNight.png";

	public int Check(HotkeyControl hotkey)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (AI.Instance.GetGCDCooldown() < Data.OGCDLock && AI.Instance.GetGCDCooldown() != 0)
		{
			return -1;
		}
		if (!SpellHelper.IsReady(7393u))
		{
			return -3;
		}
		if (hotkey.MoTarget.HasValue)
		{
			CharacterAgent target = hotkey.MoTarget.Value;
			if (Data.Get距离(target) > 30f)
			{
				return -2;
			}
		}
		return 0;
	}

	public void Build(Slot slot, HotkeyControl hotkey)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		slot.Add((Spell)((!hotkey.MoTarget.HasValue) ? ((object)SpellHelper.GetSpell(SpellId)) : ((object)new Spell(SpellId, hotkey.MoTarget.Value))));
	}
}
