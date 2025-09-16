using AEAssist.CombatRoutine.View;
using ImGuiNET;

namespace Linto.Sage;

public class SageUISetting: ISettingUI
{
    public string Name => "崩破大王";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw() 
    {
        if (ImGui.CollapsingHeader("起手&技能设置"))
        {
            ImGui.Text("起手设置");
            var opener = "";
            if (SGESettings.Instance.opener == 0)
                opener = "常规起手";
            else if (SGESettings.Instance.opener == 1) opener = "贤炮起手";
            else if (SGESettings.Instance.opener == 2) opener = "绝亚起手";
            else if (SGESettings.Instance.opener == 3) opener = "70级起手";
            if (ImGui.BeginCombo("", opener))
            {
                if (ImGui.Selectable("常规起手"))
                {
                    SGESettings.Instance.opener = 0;
                    SGESettings.Instance.Save();
                }
                if (ImGui.Selectable("贤炮起手"))
                {
                    SGESettings.Instance.opener = 1;
                    SGESettings.Instance.Save();
                }
                if (ImGui.Selectable("绝亚起手"))
                {
                    SGESettings.Instance.opener = 2;
                    SGESettings.Instance.Save();
                }
                if(ImGui.Selectable("70级起手"))
                {
                    SGESettings.Instance.opener = 3;
                    SGESettings.Instance.Save();
                }
                ImGui.EndCombo();
            }
            bool a = SGESettings.Instance.康复tea;
            if (ImGui.Checkbox("绝亚康复(默认H2顺序)", ref SGESettings.Instance.康复tea))
            {
                SGESettings.Instance.Save(); 
                a = SGESettings.Instance.康复tea; 
            }
            if (a)
            {
                if (ImGui.Checkbox("使用H1顺序", ref SGESettings.Instance.H1)) SGESettings.Instance.Save(); 
            }
            SGESettings.Instance.time = Math.Clamp(SGESettings.Instance.time, 1000, 1600);
            bool b = SGESettings.Instance.关心最低血量T;
            if (ImGui.Checkbox("关心最低血量T", ref SGESettings.Instance.关心最低血量T)) SGESettings.Instance.Save();
            {
                SGESettings.Instance.Save();
                b = SGESettings.Instance.关心最低血量T; 
            }
            if (b)
            {
                if (ImGui.SliderFloat("触发换绑阈值", ref SGESettings.Instance.最低血量T, 0.01f, 1f)) SGESettings.Instance.Save(); 
            }
            if (ImGui.InputInt("预读时间", ref SGESettings.Instance.time)) SGESettings.Instance.Save();
        }
    }
}