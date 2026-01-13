namespace Linto.LintoPvP.VPR.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPVPRSettingUI : ISettingUI
{
    public string Name => "双刀小子";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("测试测试册俄式");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
