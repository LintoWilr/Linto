namespace Linto.LintoPvP.RDM.Triggers;

using AEAssist.CombatRoutine.View;
using ImGuiNET;

public class PvPRDMSettingUI : ISettingUI
{
    public string Name => "董慧敏";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("恶娜娜恶娜娜恶娜娜");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
