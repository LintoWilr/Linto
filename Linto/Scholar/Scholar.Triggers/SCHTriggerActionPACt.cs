using CombatRoutine.TriggerModel;
using Common.Helper;
using Common.Language;

namespace Linto.Scholar.Scholar.Triggers;

public class SCHTriggerActionPACt : ITriggerAction, ITriggerBase
{
	public string DisplayName => LanguageHelper.Loc("SCH/小仙女自身脚下");

	string ITriggerBase.Remark { get; set; }

	public void Check()
	{
	}

	public bool Draw()
	{
		return true;
	}

	public bool Handle()
	{
		ChatHelper.SendMessage("/pac 移动 <me>");
		return true;
	}
}
