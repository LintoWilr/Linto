#region

using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.View;
using ECommons.LanguageHelpers;
using Dalamud.Bindings.ImGui;

#endregion

namespace Linto.LintoPvP.GNB.Triggers;

public class PvPGNBSettingUI : ISettingUI
{
    public string Name => "绝枪";
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
    
    public void Draw()
    {
        ImGui.Text("仅供摸鱼使用");
    }
}