using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;

namespace Linto.Wilr.DarkKnight.Ability;

public class 爆发药 : ISlotResolver
{
	public SlotMode SlotMode { get; } = (SlotMode)1;


	public int Check()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!Qt.GetQt("爆发药"))
		{
			return -2;
		}
		if (!Qt.GetQt("CD"))
		{
			return -2;
		}
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
		if (!SpellHelper.CoolDownInGCDs(SpellHelper.GetSpell(16472u), 2))
		{
			return -3;
		}
		return 0;
	}

	public void Build(Slot slot)
	{
		slot.Add(Spell.CreatePotion());
	}
}
