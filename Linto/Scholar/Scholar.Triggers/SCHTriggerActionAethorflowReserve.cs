using CombatRoutine.TriggerModel;
using Common.Language;
using ImGuiNET;

namespace Linto.Scholar.Scholar.Triggers;

public class SCHTriggerActionAethorflowReserve : ITriggerAction, ITriggerBase
{
	public int AetherflowReserve;

	public string DisplayName => LanguageHelper.Loc("SCH/豆子保留层数");

	string ITriggerBase.Remark { get; set; }

	public void Check()
	{
		if (AetherflowReserve < 0 || AetherflowReserve > 3)
		{
			throw new Exception("AethorflowReserve must be 0 to 3");
		}
	}

	public bool Draw()
	{
		if (ImGui.InputInt(LanguageHelper.Loc("豆子保留层数"), ref AetherflowReserve))
		{
			AetherflowReserve = Math.Clamp(AetherflowReserve, 0, 3);
		}
		return true;
	}

	public bool Remark()
	{
		return true;
	}

	public bool Handle()
	{
		SCHBattleData.Instance.AethorflowReserve = AetherflowReserve;
		return true;
	}
}
