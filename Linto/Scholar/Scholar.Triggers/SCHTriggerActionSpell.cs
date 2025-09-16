using CombatRoutine.TriggerModel;
using Common;
using Common.Language;
using ImGuiNET;

namespace Linto.Scholar.Scholar.Triggers;

public class SCHTriggerActionSpell : ITriggerAction, ITriggerBase
{
	private bool clear;

	public string DisplayName => LanguageHelper.Loc("SCH/插入技能");

	public SpellConfig SpellConfig { get; set; } = new SpellConfig();


	public bool Clear { get; set; }

	string ITriggerBase.Remark { get; set; }

	public void Check()
	{
	}

	public bool Draw()
	{
		if (Clear)
		{
			clear = Clear;
		}
		if (ImGui.Checkbox("是否清除队列", ref clear))
		{
			Clear = clear;
		}
		if (!clear)
		{
			SpellConfig.OnGUI();
		}
		return true;
	}

	public bool Handle()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Invalid comparison between Unknown and I4
		if (Clear)
		{
			SCHBattleData.Instance.SpellQueueoGCD.Clear();
			SCHBattleData.Instance.SpellQueueGCD.Clear();
			return true;
		}
		if ((int)Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) == 3)
		{
			SCHBattleData.Instance.SpellQueueoGCD.Enqueue(SpellConfig.Create());
		}
		if ((int)Core.Get<IMemApiSpell>().GetSpellType(SpellConfig.Create().Id) != 3)
		{
			SCHBattleData.Instance.SpellQueueGCD.Enqueue(SpellConfig.Create());
		}
		return true;
	}
}
