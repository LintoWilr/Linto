namespace Linto.LintoPvP.VPR.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPVPRSettingUI : ISettingUI
{
    public string Name => "双刀小子";
#pragma warning disable CS0414 // 保留字段用于未来功能扩展
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
#pragma warning restore CS0414
    
    public void Draw()
    {
        ImGui.Text("测试测试册俄式");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
