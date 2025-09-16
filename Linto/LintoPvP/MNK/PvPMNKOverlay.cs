using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Helper;
using Common.Language;
using ImGuiNET;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.MNK;

public class PvPMNKOverlay
{
    private bool isHorizontal;
    private bool ShowWhy;
    private bool ShowWhy2;
    private bool Potion;
    private bool 原神;
    private bool 米卫兵;
    private bool 阅读使用说明 ;

    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        if (PVPHelper.通用权限())
        { 
            if (ImGui.Button(LanguageHelper.Loc("点我开启自动攻击")))
            {
                Share.Pull = true;
            } 
            PvPMNKSettings.Instance.药血量 = Math.Clamp(PvPMNKSettings.Instance.药血量, 1, 100);
            if (ImGui.InputInt("热水阈值", ref PvPMNKSettings.Instance.药血量, 1,5)) 
            {
                PvPMNKSettings.Instance.Save();
            }
            if (ImGui.SliderFloat("金刚阈值", ref PvPMNKSettings.Instance.金刚阈值设置, 0.0f, 1.0f))
            {
                PvPMNKSettings.Instance.Save();
            }
            if (ImGui.SliderFloat("金刚回血阈值", ref PvPMNKSettings.Instance.金刚回血阈值, 0.0f, 1.0f))
            {
                PvPMNKSettings.Instance.Save();
            }
        }
        else
            ImGui.Text("无权限");
    }
    

    // public void 更新日志(JobViewWindow jobViewWindow)
    // {
    // }
    private string 正确密码 = "迷跡波";
    private string 输入密码 = "";
    private string 更新密码 = "潜在表明";
    public void DrawDev(JobViewWindow jobViewWindow)
    {
        PVPHelper.获取权限();
        if (PVPHelper.通用权限())
        { 
            PVPHelper.更新窗口("武僧");
        }
    }
    
}
public static class MNKQt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return PvPMNKRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return PvPMNKRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return PvPMNKRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        PvPMNKRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        PvPMNKRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return PvPMNKRotationEntry.JobViewWindow.GetQtArray();
    }
}
