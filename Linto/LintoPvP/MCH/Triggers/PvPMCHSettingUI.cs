namespace Linto.LintoPvP.MCH.Triggers;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPMCHSettingUI : ISettingUI
{
    public string Name => "机工";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入

    public void Draw()
    {
        ImGui.Text("机工机工机工");
        ImGui.Text("机工机工机工");
    }
}
