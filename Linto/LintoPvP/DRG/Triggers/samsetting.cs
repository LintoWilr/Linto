using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

namespace Linto.LintoPvP.DRG.Triggers;

public class PvPDRGSettingUI : ISettingUI
{
    public string Name => "龙骑骑";
#pragma warning disable CS0414 // 保留字段用于未来功能扩展
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
#pragma warning restore CS0414
    
    public void Draw()
    {
        ImGui.Text("龙骑骑");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
