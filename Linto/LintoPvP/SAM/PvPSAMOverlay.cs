using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using ImGuiNET;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM;

public class PvPSAMOverlay
{
    private static bool 调试窗口 = false;
    private static bool 更新日志 = false;
    public static void DrawGeneral(JobViewWindow jobViewWindow)
    {
        
        Share.Pull = true;
        {
            职业配置 盘子 = new 职业配置();
            盘子.配置武士技能();
            if (ImGui.CollapsingHeader("调试窗口"))
            {
                PVPHelper.PvP调试窗口();
            }
            /*ImGui.SameLine();
            if (ImGui.Button("查看更新日志(10/4)"))
            {
                更新日志= true;
            } 
            if (更新日志)
            { 
                ImGui.Begin("更新日志");
                ImGui.Text("尝试优化了发呆的原因，推测与fate发呆原理相同\n人多导致服务器不回包");
                if (ImGui.Button(("关闭窗口")))
                {
                    更新日志 = false;
                }
                ImGui.End();
            }*/
            /*if (调试窗口)
            { 
                ImGui.Begin("调试窗口");
                ImGui.Text($"gcd:{GCDHelper.GetGCDCooldown()}");
                //	ImGui.Text($"绿吸:{PVPHelper.Get绝枪绿吸取目标().Name},{PVPHelper.Get绝枪绿吸取目标().Pos}");
                //	ImGui.Text($"红吸:{PVPHelper.Get绝枪红吸取目标().Name},{PVPHelper.Get绝枪红吸取目标().Pos}");
                ImGui.Text($"自己：{Core.Me.Name},{Core.Me.Position},{Core.Me.Position}");
                ImGui.Checkbox("斩铁日志调试模式", ref PvPSAMSettings.Instance.斩铁调试);
                if (PVPHelper.Get斩铁目标() == Core.Me)
                {
                    ImGui.Text("斩铁目标：无");
                }
                if (PVPHelper.Get多斩Target(PvPSAMSettings.Instance.多斩人数) == Core.Me)
                {
                    ImGui.Text("多斩目标：无");
                }
                if (PVPHelper.Get多斩Target(PvPSAMSettings.Instance.多斩人数) != Core.Me)
                {
                    ImGui.Text($"多斩目标：{PVPHelper.Get多斩Target(PvPSAMSettings.Instance.多斩人数)}");
                }
                if (PVPHelper.Get斩铁目标() != Core.Me)
                {
                    ImGui.Text($"斩铁目标：{PVPHelper.Get斩铁目标().Name}");
                }
                ImGui.Text($"血量百分比：{Core.Me.CurrentHpPercent()}");
                ImGui.Text($"盾值百分比：{Core.Me.ShieldPercentage/100f}");
                ImGui.Text($"血量百分比：{Core.Me.CurrentHpPercent()+Core.Me.ShieldPercentage/100f <= 1.0f}");
                ImGui.Text($"万能：{PVPHelper.Get最合适目标(10).Position}");
                //ImGui.Text($"目标：{Core.Me.GetCurrTarget().Name},{Core.Me.GetCurrTarget().DataId},{Core.Me.GetCurrTarget().Position}");
                ImGui.Text($"是否移动：{MoveHelper.IsMoving()}");
                ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
                ImGui.Text($"25米内敌方人数：{TargetHelper.GetNearbyEnemyCount(Core.Me, 25, 25)}");
                ImGui.Text($"20米内小队人数：{PartyHelper.CastableAlliesWithin20.Count}");
                ImGui.Text($"目标5米内人数：{TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 5)}");
                ImGui.Text($"LB槽当前数值：{Core.Me.LimitBreakCurrentValue()}");
                ImGui.Text($"上个技能：{Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
                ImGui.Text($"上个GCD：{Core.Resolve<MemApiSpellCastSuccess>().LastGcd}");
                ImGui.Text($"上个能力技：{Core.Resolve<MemApiSpellCastSuccess>().LastAbility}");
                ImGui.Text($"上个连击技能：{Core.Resolve<MemApiSpell>().GetLastComboSpellId()})");
                ImGui.Text($"最近的目标：{PVPHelper.Get最近目标().Name},{PVPHelper.Get最近目标().DataId})");
                if (ImGui.Button(("关闭窗口")))
                {
                    调试窗口 = false;
                }
                ImGui.End();
            }*/
        }
    }

    public static class SAMQt
    {
        /// 获取指定名称qt的bool值
        public static bool GetQt(string qtName)
        {
            return PvP崩破大王Entry.JobViewWindow.GetQt(qtName);
        }

        /// 反转指定qt的值
        /// <returns>成功返回true，否则返回false</returns>
        public static bool ReverseQt(string qtName)
        {
            return PvP崩破大王Entry.JobViewWindow.ReverseQt(qtName);
        }

        /// 设置指定qt的值
        /// <returns>成功返回true，否则返回false</returns>
        public static bool SetQt(string qtName, bool qtValue)
        {
            return PvP崩破大王Entry.JobViewWindow.SetQt(qtName, qtValue);
        }

        /// 给指定qt设置新的默认值
        public static void NewDefault(string qtName, bool newDefault)
        {
            PvP崩破大王Entry.JobViewWindow.NewDefault(qtName, newDefault);
        }

        /// 将当前所有Qt状态记录为新的默认值，
        /// 通常用于战斗重置后qt还原到倒计时时间点的状态
        public static void SetDefaultFromNow()
        {
            PvP崩破大王Entry.JobViewWindow.SetDefaultFromNow();
        }

        /// 返回包含当前所有qt名字的数组
        public static string[] GetQtArray()
        {
            return PvP崩破大王Entry.JobViewWindow.GetQtArray();
        }
    }
}
