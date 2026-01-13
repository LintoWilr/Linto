using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Linto.JobView;

namespace Linto.Wilr.DarkKnight.Hotkey;

public class 爆发药 : IHotkeySlotResolver
{
	public bool UseMoTarget => false;

	public uint SpellId => 0u;

	public string ImgPath => "Resources\\Spells\\Potion.png";

	public int Check(HotkeyControl hotkey)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		PotionSetting setting = SettingMgr.GetSetting<PotionSetting>();
		CharacterAgent me = Core.Me;
		uint potionRawId = setting.GetPotionId(me.CurrentJob);
		double 爆发药cd = Core.Get<IMemApiInventory>().GetItemCoolDown(potionRawId).TotalMilliseconds;
		uint 爆发药数量 = Core.Get<IMemApiInventory>().GetItemCount(potionRawId, true);
		if (爆发药cd != 0.0)
		{
			return -1;
		}
		if (爆发药数量 == 0)
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot, HotkeyControl hotkey)
	{
		slot.Add(Spell.CreatePotion());
	}
}
