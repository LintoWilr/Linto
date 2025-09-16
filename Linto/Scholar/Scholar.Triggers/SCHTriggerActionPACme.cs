using CombatRoutine.TriggerModel;
using Common.Helper;
using Common.Language;

namespace Linto.Scholar.Scholar.Triggers
{
	public class SCHTriggerActionPACme : ITriggerAction, ITriggerBase
	{
		public string DisplayName => LanguageHelper.Loc("SCH/小仙女目标脚下");

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
			ChatHelper.SendMessage("/pac 移动 <t>");
			return true;
		}
	}
}
