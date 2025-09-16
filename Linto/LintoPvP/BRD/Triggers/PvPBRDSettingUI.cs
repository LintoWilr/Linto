namespace Linto.LintoPvP.BRD.Triggers;

using AEAssist.CombatRoutine.View;
using ImGuiNET;

public class PvPBRDSettingUI : ISettingUI
{
    public string Name => "诗人";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("爱唱拦不住");
     //   ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
