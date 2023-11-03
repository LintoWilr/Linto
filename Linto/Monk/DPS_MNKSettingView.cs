#region

using CombatRoutine.View;
using Common.GUI;
using ImGuiNET;

#endregion

namespace Linto.Monk;

public class DPSMNKSettingView : ISettingUI
{
    public string Name => "日随专用盲僧";
    public void Draw()
    {
        /*ImGuiHelper.LeftInputInt("突进时间(倒数多少ms突进)",
            ref PLDSettings.Instance.Time, 100, 10000, 100);*/
        /*ImGuiHelper.LeftInputInt("倒数圣灵时间",
            ref PLDSettings.Instance.HolyspritTime, 1500, 2000, 10);
        /*ImGui.Checkbox("绝欧起手".Loc(),
            ref PLDSettings.Instance.OmegaOpener);#1#*/
        {
            ImGuiHelper.LeftInputFloat("内丹阈值", ref MNKSettings.Instance.内丹阈值设置, 0.3f);
            {
                MNKSettings.Instance.Save();
            }
            ImGuiHelper.LeftInputFloat("浴血阈值", ref MNKSettings.Instance.浴血阈值设置, 0.4f);
            {
                MNKSettings.Instance.Save();
            }
            ImGuiHelper.LeftInputFloat("金刚阈值", ref MNKSettings.Instance.金刚阈值设置, 0.7f);
            {
                MNKSettings.Instance.Save();
            }
            ImGuiHelper.LeftInputFloat("真言阈值", ref MNKSettings.Instance.真言阈值设置, 0.5f);
            {
                MNKSettings.Instance.Save();
            }
            if (ImGui.Checkbox("真言队友", ref MNKSettings.Instance.真言队友));
            {
                if (MNKSettings.Instance.真言队友)
                { 
                    ImGui.SliderFloat("队友血量阈值", ref MNKSettings.Instance.真言队友阈值设置, 0.0f, 1.0f);
                    MNKSettings.Instance.Save();
                }
            }
            if (ImGui.TreeNode("参数调整"))
            {
                ImGui.SliderFloat("功力时间设定", ref MNKSettings.Instance.功力时间, 4000f, 5000f);
                MNKSettings.Instance.Save();
                ImGui.TreePop();
            }
            ImGuiHelper.ToggleButton("脱战使用演武（待优化）", ref MNKSettings.Instance.脱战演武);
            {
                MNKSettings.Instance.Save();
            }
        }
    }
}