namespace Linto.LintoPvP.SAM.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPSamSettingUI : ISettingUI
{
    public string Name => "崩破大王";
#pragma warning disable CS0414 // 保留字段用于未来功能扩展
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
#pragma warning restore CS0414
    
    public void Draw()
    {
        ImGui.Text("成敗いたAAAAAす！");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
