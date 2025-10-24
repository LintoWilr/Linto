using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using Dalamud.Bindings.ImGui;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SMN;

public class PvPSMNOverlay
{ 
    public static void DrawGeneral(JobViewWindow jobViewWindow)
    {
        Share.Pull = true;
        {
            职业配置 召唤 = new 职业配置();
            召唤.配置召唤技能();
            if (ImGui.CollapsingHeader("调试窗口"))
            {
                PVPHelper.PvP调试窗口();
            }
        }
    }
    
}
public static class SMNQt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return PvPSMNRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return PvPSMNRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return PvPSMNRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        PvPSMNRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        PvPSMNRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return PvPSMNRotationEntry.JobViewWindow.GetQtArray();
    }
}
