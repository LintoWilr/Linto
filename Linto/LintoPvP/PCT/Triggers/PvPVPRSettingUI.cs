namespace Linto.LintoPvP.PCT.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPPCTSettingUI : ISettingUI
{
    public string Name => "董慧敏";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("最讨厌的食物是胡萝卜");
    }
}
