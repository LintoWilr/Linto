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
#pragma warning disable CS0414 // 保留字段用于未来功能扩展
    private bool 设置;
    private string _inputText = ""; // 用于存储文本框的输入
#pragma warning restore CS0414
    
    public void Draw()
    {
        ImGui.Text("仅供摸鱼使用");
    }
}