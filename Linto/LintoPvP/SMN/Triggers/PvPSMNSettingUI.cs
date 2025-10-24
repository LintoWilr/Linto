namespace Linto.LintoPvP.SMN.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPSMNSettingUI : ISettingUI
{
    public string Name => "召";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("召");
    }
}