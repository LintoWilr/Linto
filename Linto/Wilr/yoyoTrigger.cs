using System.Numerics;
using CombatRoutine.TriggerModel;
using Common.Language;
using ImGuiNET;
using Linto.Wilr.DarkKnight;

namespace Linto.Wilr;

public class yoyoTrigger : ITriggerAction, ITriggerBase
{
	public enum Job
	{
		DK
	}

	private int 当前combo;

	private int radioType;

	private int radioCheck;

	public string Remark { get; set; }

	public string DisplayName => LanguageHelper.Loc("ＤＫ");

	private Job job { get; set; }

	public string ValueName { get; set; } = new string("");


	public bool Value { get; set; }

	public void Check()
	{
	}

	public bool Draw()
	{
		string[] qtArray = Qt.GetQtArray();
		当前combo = Array.IndexOf(qtArray, ValueName);
		radioCheck = ((!Value) ? 1 : 0);
		/*if (ImGui.BeginTabBar("###TriggerTab"))
		{
			if (ImGui.BeginTabItem("DK"))
			{
				ImGui.BeginChild("###TriggerDK", new Vector2(0f, 0f));
				job = Job.DK;
				ImGui.RadioButton("Qt", ref radioType, 0);
				ImGui.NewLine();
				ImGui.SetCursorPos(new Vector2(0f, 40f));
				if (radioType == 0)
				{
					ImGui.Combo("Qt开关", ref 当前combo, qtArray, qtArray.Length);
					ValueName = qtArray[当前combo];
					ImGui.RadioButton("开", ref radioCheck, 0);
					ImGui.SameLine();
					ImGui.RadioButton("关", ref radioCheck, 1);
					Value = radioCheck == 0;
				}
				ImGui.EndChild();
				ImGui.EndTabItem();
			}
			ImGui.EndTabBar();
		}*/
		return true;
	}

	public bool Handle()
	{
		if (job == Job.DK && radioType == 0)
		{
			Qt.SetQt(ValueName, Value);
		}
		return true;
	}
}
