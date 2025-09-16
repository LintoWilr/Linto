using CombatRoutine.View;
using ImGuiNET;

namespace Linto.Scholar;

public class SCHSettingView : ISettingUI
{
	private bool setting;

	public string? opener;

	public string Name => "学者";

	public void Draw()
	{
		ImGui.Text("起手设置");
		if (SCHSettings.Instance.opener == 0)
		{
			opener = "保留即刻";
		}
		if (SCHSettings.Instance.opener == 1)
		{
			opener = "即刻起手";
		}
		if (SCHSettings.Instance.opener == 2)
		{
			opener = "以太起手";
		}
		if (ImGui.BeginCombo("", opener))
		{
			if (ImGui.Selectable("保留即刻"))
			{
				SCHSettings.Instance.opener = 0;
			}
			if (ImGui.Selectable("即刻起手"))
			{
				SCHSettings.Instance.opener = 1;
			}
			if (ImGui.Selectable("以太起手"))
			{
				SCHSettings.Instance.opener = 2;
			}
			ImGui.EndCombo();
		}
		if (ImGui.Button("保存设置"))
		{
			SCHSettings.Instance.Save();
		}
	}
}
