using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using ImGuiNET;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR;

public class PvPVPROverlay
{
    private static bool 调试窗口 = false;
    public static void DrawGeneral(JobViewWindow jobViewWindow)
    {
        
        Share.Pull = true;
        {
            PVPHelper.权限获取();
            ImGui.Separator();  
            ImGui.Text("哥哥没我砍的快");
            ImGui.Separator();
            PVPHelper.技能图标(29711u);
            ImGui.SameLine();
            ImGui.Text("喝热水");
            ImGui.InputInt("热水阈值", ref PvPVPRSettings.Instance.药血量, 5, 4);
            ImGui.Separator();
            PvPVPRSettings.Instance.Save();
        }
    } 
    public static class VPRQt
    {
        /// 获取指定名称qt的bool值
        public static bool GetQt(string qtName)
        {
            return PvP双刀小子Entry.JobViewWindow.GetQt(qtName);
        }

        /// 反转指定qt的值
        /// <returns>成功返回true，否则返回false</returns>
        public static bool ReverseQt(string qtName)
        {
            return PvP双刀小子Entry.JobViewWindow.ReverseQt(qtName);
        }

        /// 设置指定qt的值
        /// <returns>成功返回true，否则返回false</returns>
        public static bool SetQt(string qtName, bool qtValue)
        {
            return PvP双刀小子Entry.JobViewWindow.SetQt(qtName, qtValue);
        }

        /// 给指定qt设置新的默认值
        public static void NewDefault(string qtName, bool newDefault)
        {
            PvP双刀小子Entry.JobViewWindow.NewDefault(qtName, newDefault);
        }

        /// 将当前所有Qt状态记录为新的默认值，
        /// 通常用于战斗重置后qt还原到倒计时时间点的状态
        public static void SetDefaultFromNow()
        {
            PvP双刀小子Entry.JobViewWindow.SetDefaultFromNow();
        }

        /// 返回包含当前所有qt名字的数组
        public static string[] GetQtArray()
        {
            return PvP双刀小子Entry.JobViewWindow.GetQtArray();
        }
    }
}
