using System.Numerics;
using ImGuiNET;

namespace Linto.Sage.Triggers;

public class TriggerQTSetting
{
	public string Key = "爆发药";

	public bool Value = false;

	private int combo;

	private int radioCheck;

	private readonly string[] _qtArray = SageRotationEntry.JobViewWindow.GetQtArray();

	public void draw()
	{
		combo = Array.IndexOf(_qtArray, Key);
		if (combo == -1)
		{
			combo = 0;
		}
		radioCheck = (Value ? 1 : 0);
		ImGui.NewLine();
		ImGui.SetCursorPos(new Vector2(0f, 40f));
		ImGui.Combo("Qt开关", ref combo, _qtArray, _qtArray.Length);
		ImGui.RadioButton("开", ref radioCheck, 1);
		ImGui.SameLine();
		ImGui.RadioButton("关", ref radioCheck, 0);
		Key = _qtArray[combo];
		Value = radioCheck == 1;
	}

	public void action()
	{
		SageRotationEntry.JobViewWindow.SetQt(Key, Value);
	}
}
