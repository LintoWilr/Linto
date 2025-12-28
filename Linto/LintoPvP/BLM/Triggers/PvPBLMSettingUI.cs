namespace Linto.LintoPvP.BLM;

using AEAssist.CombatRoutine.View;
using Dalamud.Bindings.ImGui;

public class PvPBLMSettingUI : ISettingUI
{
    public string Name => "黑魔";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入

    public void Draw()
    {
        ImGui.Text("喵");
        ImGui.Text("仅供7.1技改之前摸鱼使用");
    }
}
