using System.Numerics;
using System.Runtime.CompilerServices;
using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Helper;
using Common.Language;
using ECommons.DalamudServices;
using ImGuiNET;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.SCH;

public class PvPSCHOverlay
{
    
    private bool isHorizontal;
    private bool ShowWhy;
    private bool ShowWhy2;
    private bool Potion;
    private bool 原神;
    private bool 阅读使用说明 ;
    bool 技能配置;
    bool 扩散选择;
    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        if (PVPHelper.通用权限())
        {
            ImGui.Text("最优毒扩散测试中 版本:");
            ImGui.SameLine();
            ImGui.TextColored(new Vector4(42f / 255f, 215f / 255f, 57f / 255f, 0.8f), "3/19");
            {
                ImGui.Text($"最少上毒/扩散数量:{PvPSCHSettings.Instance.自动扩散数量}");
                if(PVPHelper.Get最佳毒目标().Name!=Core.Me.Name)
                {
                    ImGui.Text($"毒目标:{PVPHelper.Get最佳毒目标().Name},{PVPHelper.Get最佳毒目标().Pos}");
                    ImGui.Text($"毒目标计数:{PVPHelper.Get毒目标计数()}");
                }
                if(PVPHelper.Get最佳毒目标().Name==Core.Me.Name)
                {
                    ImGui.Text($"毒目标:无符合条件的目标");
                }
                if(PVPHelper.Get最佳扩散目标().Name!=Core.Me.Name)
                {
                    ImGui.Text($"扩散目标:{PVPHelper.Get最佳扩散目标().Name},{PVPHelper.Get最佳扩散目标().Pos}");
                }
                if(PVPHelper.Get最佳毒目标().Name==Core.Me.Name)
                {
                    ImGui.Text($"扩散目标:无符合条件的目标");
                }
                ImGui.Text($"当前选择目标:{Core.Me.GetCurrTarget().Name},{Core.Me.GetCurrTarget().Pos}");
                //   ImGui.Text($"毒目标2测试:{PVPHelper.Get最佳毒目标测试().Name},{PVPHelper.Get最佳毒目标测试().Pos}");
            }
            if (ImGui.Button(LanguageHelper.Loc("开启自动攻击并进行技能配置")))
            {
                技能配置 = true;
                Share.Pull = true;
                LogHelper.Info($"{技能配置}");
            }
            if (技能配置)
            {
                ImGui.Begin("技能配置");
                ImGui.Text("说明:部分选项为强制开启");
                ImGui.Separator();
                PVPHelper.技能图标(29711u);
                ImGui.SameLine();
                ImGui.Text("喝热水");
                ImGui.InputInt("热水阈值", ref PvPSCHSettings.Instance.药血量, 5, 4);
                ImGui.Separator();
                PVPHelper.技能图标(29234u);
                ImGui.SameLine();
                ImGui.Text("扩散");
                // 上毒优先开启
                if (ImGui.Checkbox("上毒优先", ref PvPSCHSettings.Instance.上毒优先开启))
                {
                        // 当上毒优先开启时，关闭鼓励优先
                    PvPSCHSettings.Instance.鼓励优先开启 = false;
                }

                // 鼓励优先开启
                if (ImGui.Checkbox("鼓励优先", ref PvPSCHSettings.Instance.鼓励优先开启))
                {
                    // 当鼓励优先开启时，关闭上毒优先
                    PvPSCHSettings.Instance.上毒优先开启 = false;
                }
                if (PvPSCHSettings.Instance.上毒优先开启)
                {
                    ImGui.Indent(); // 缩进
                    PvPSCHSettings.Instance.扩散敌人数量 = Math.Clamp(PvPSCHSettings.Instance.扩散敌人数量, 1, 48);
                    ImGui.InputInt("扩散敌人数量(目标扩散)", ref PvPSCHSettings.Instance.扩散敌人数量);
                    ImGui.Checkbox("仅秘策上毒", ref PvPSCHSettings.Instance.仅秘策使用);
                    ImGui.Checkbox("最优自动上毒/扩散", ref PvPSCHSettings.Instance.自动扩毒);
                    PvPSCHSettings.Instance.扩毒剩余时间 = Math.Clamp(PvPSCHSettings.Instance.扩毒剩余时间, 1, 15);
                    PvPSCHSettings.Instance.自动扩散数量 = Math.Clamp(PvPSCHSettings.Instance.自动扩散数量, 1, 48);
                    if (PvPSCHSettings.Instance.自动扩毒)
                    {
                        ImGui.InputInt("毒BUFF剩余时间/秒", ref PvPSCHSettings.Instance.扩毒剩余时间);
                        ImGui.InputInt("自动上毒/扩散最少人数", ref PvPSCHSettings.Instance.自动扩散数量);
                    }
                    ImGui.Unindent(); // 恢复缩进
                }

                if (PvPSCHSettings.Instance.鼓励优先开启)
                {
                    ImGui.Indent(); // 缩进
                    // 只有鼓励优先开启时才显示相关设置
                    PvPSCHSettings.Instance.鼓励队友数量 = Math.Clamp(PvPSCHSettings.Instance.鼓励队友数量, 1, 8);
                    ImGui.InputInt("扩散队友数量", ref PvPSCHSettings.Instance.鼓励队友数量);
                    ImGui.Unindent(); // 恢复缩进
                }

                ImGui.Separator();
                PVPHelper.技能图标(29236u);
                ImGui.SameLine();
                ImGui.Text("疾风怒涛");
                PvPSCHSettings.Instance.跑快快队友数量 = Math.Clamp(PvPSCHSettings.Instance.跑快快队友数量, 1, 8);
                ImGui.InputInt("附近小队成员人数", ref PvPSCHSettings.Instance.跑快快队友数量, 1, 2);
                ImGui.Separator();
                PVPHelper.技能图标(29235u);
                ImGui.SameLine();
                ImGui.Text("枯骨法");
                PvPSCHSettings.Instance.枯骨法数量 = Math.Clamp(PvPSCHSettings.Instance.枯骨法数量, 1, 48);
                ImGui.InputInt("范围内数量", ref PvPSCHSettings.Instance.枯骨法数量, 1, 2);
                ImGui.Separator();
                if (ImGui.Button(LanguageHelper.Loc("保存设置")))
                {
                    PvPSCHSettings.Instance.Save();
                    技能配置 = false;
                    LogHelper.Info($"{技能配置}");
                }
                ImGui.End();
            }
            
        }
        else
            ImGui.Text("无权限");
    }

    /*public void Draw目标监控窗口(JobViewWindow jobViewWindow)
    {
        ImGui.SetNextWindowSize(new Vector2(180f, 325f));
        ImGui.Begin("###targetMe_Window", (ImGuiWindowFlags)43);
        List<CharacterAgent> targetMe = PVPTargetHelper.Get看着目标的人(Group.敌人, Core.Me);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Resources\\Images\\Number\\");
        defaultInterpolatedStringHandler.AppendFormatted((targetMe.Count <= 4) ? ((object)targetMe.Count) : "4+");
        defaultInterpolatedStringHandler.AppendLiteral(".png");
        string imgPath = defaultInterpolatedStringHandler.ToStringAndClear();
        TextureWrap texture = default(TextureWrap);
        if (Core.Get<IMemApiIcon>().TryGetTexture(imgPath, out texture, true))
        {
            ImGui.Text("    ");
            ImGui.SameLine();
            ImGui.Image(texture.ImGuiHandle, new Vector2(125f, 200f));
        }
        if (SCHQt.GetQt("自动警报") && targetMe.Count >= 3)
        {
            ChatHelper.SendMessage("/e 你被集火了！<se.6><se.6><se.6><se.6>");
            PVPHelper.延时();
        }
        if (targetMe.Count > 0)
        {
            int i = 1;
            TextureWrap textureJob = default(TextureWrap);
            foreach (CharacterAgent item in targetMe)
            {
                CharacterAgent v = item;
                if (i > 6)
                {
                    break;
                }
                uint job = (uint)(int)v.CurrentJob;
                if (Core.Get<IMemApiIcon>().TryGetTexture($"Resources\\jobs\\{job}.png", out textureJob, true))
                {
                    ImGui.Image(textureJob.ImGuiHandle, new Vector2(50f, 50f));
                    if (i != 3)
                    {
                        ImGui.SameLine(0f, 5f);
                    }
                    i++;
                }
            }
        }
        ImGui.End();
        if (Core.Me.HasAura(895u) && Core.Me.HasAura(1342u) && MoveHelper.IsMoving())
        {
            Core.Get<IMemApiSpell>().Cast(29057u, Core.Me);
        }
    }*/
    // public void 更新日志(JobViewWindow jobViewWindow)
    // {
    //    
    // }
    private string 正确密码 = "迷跡波";
    private string 输入密码 = "";
    private string 更新密码 = "潜在表明";
    public void DrawDev(JobViewWindow jobViewWindow)
    {
        PVPHelper.获取权限();
        if (PVPHelper.通用权限())
        {
            ImGui.Text("2023-11-19:添加鼓舞");
            ImGui.Text("2024-3-18:添加最优炒股目标,优化跑快快和蛊毒法的释放逻辑");
        }
    }
    
}
public static class SCHQt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return PvPSCHRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return PvPSCHRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return PvPSCHRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        PvPSCHRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        PvPSCHRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return PvPSCHRotationEntry.JobViewWindow.GetQtArray();
    }
}
