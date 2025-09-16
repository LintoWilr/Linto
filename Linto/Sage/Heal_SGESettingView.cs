using AEAssist.CombatRoutine.View;
using ImGuiNET;

namespace Linto.Sage;

public class HealSgeSettingView : ISettingUI
{
    public string Name => "贤者";
    public string? opener;
    public void Draw()
    {
        /*ImGui.Checkbox(Language.Instance.ToggleWildFireFirst,
            ref MCHSettings.Instance.WildfireFirst);

        ImGuiHelper.LeftInputInt(Language.Instance.InputStrongGcdCheckTime,
            ref MCHSettings.Instance.StrongGCDCheckTime, 1000, 10000, 1000);*/
        ImGui.Text("起手设置");
        if (SGESettings.Instance.opener == 0)
        {
            opener = "常规起手";
        }
        if (SGESettings.Instance.opener == 1)
        {
            opener = "贤炮起手";
        }
        if (SGESettings.Instance.opener == 2)
        {
            opener = "绝亚起手";
        }
        if (SGESettings.Instance.opener == 3)
        {
            opener = "70级起手";
        }
        if (ImGui.BeginCombo("", opener))
        {
            if (ImGui.Selectable("常规起手"))
            {
                SGESettings.Instance.opener = 0;
            }
            if (ImGui.Selectable("贤炮起手"))
            {
                SGESettings.Instance.opener = 1;
            }
            if (ImGui.Selectable("绝亚起手"))
            {
                SGESettings.Instance.opener = 2;
            }
            if (ImGui.Selectable("70级起手"))
            {
                SGESettings.Instance.opener = 3;
            }
            ImGui.EndCombo();
        }

        ImGui.Checkbox("失衡走位",ref SGESettings.Instance.useDyskrasia);
        
        if (ImGui.Button("保存设置")) SGESettings.Instance.Save();
    }
}