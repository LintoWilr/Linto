using CombatRoutine.TriggerModel;
using Common;
using Common.Language;
using ImGuiNET;

namespace Linto.Scholar.Scholar.Triggers;

public class TriggerCondCheckSch : ITriggerCond, ITriggerBase
{
	private int AetherflowCheck;

	public string DisplayName => LanguageHelper.Loc("SCH/检测豆子");

	string ITriggerBase.Remark { get; set; }

	public void Check()
	{
	}

	public bool Draw()
	{
		if (ImGui.InputInt(LanguageHelper.Loc("检测豆子剩余多少及以上"), ref AetherflowCheck))
		{
			AetherflowCheck = Math.Clamp(AetherflowCheck, 0, 3);
		}
		return true;
	}

	public void Remark()
	{
	}

	public bool Handle(ITriggerCondParams triggerCondParamas)
	{
		if (Core.Get<IMemApiScholar>().Aetherflow() >= AetherflowCheck)
		{
			return true;
		}
		return false;
	}
}
