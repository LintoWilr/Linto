#region

using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using ImGuiNET;
using Linto.Sage.Ability;

#endregion

namespace Linto.Sage;

public  class SageOverlay
{
    private bool ShowWhy;
    private bool ShowWhy2;
    private bool isHorizontal;
    private bool 阅读使用说明 ;
    public static void DrawGeneral(JobViewWindow jobViewWindow)
    {
        /*if (!阅读使用说明&&SGESettings.Instance.使用说明)
        { 
            if (ImGui.BeginTable("使用说明", 1))
            { 
                ImGui.TableSetupColumn("使用说明", ImGuiTableColumnFlags.None); 
                ImGui.TableHeadersRow();
                
                ImGui.TableNextRow(); 
                ImGui.TableNextColumn(); 
                ImGui.Text("本Acr主要针对绝亚进行了优化适配\n有需求可联系作者"); 
                ImGui.EndTable();
            }
            if (!ShowWhy) 
            { 
                if (ImGui.SmallButton("我已经阅读过使用说明了！")) 
                { 
                    ShowWhy2 = true; 
                    ShowWhy = true; 
                } 
                if (ImGui.IsItemHovered()) { ImGui.SetTooltip("真的阅读了吗？"); } 
            } 
            if (ShowWhy2) 
            { 
                if (ShowWhy2) 
                { 
                    if (ImGui.SmallButton("不再显示使用说明")) 
                    { 
                        阅读使用说明 = true; 
                        ShowWhy2 = false; 
                        SGESettings.Instance.使用说明 = false; 
                        SGESettings.Instance.Save();
                    } 
                } 
            } 
        }*/
        if (ImGui.CollapsingHeader("起手&技能设置"))
        {
            ImGui.Text("起手设置");
            var opener = "";
            if (SGESettings.Instance.opener == 0)
                opener = "常规起手";
            else if (SGESettings.Instance.opener == 1) opener = "贤炮起手";
            else if (SGESettings.Instance.opener == 2) opener = "绝亚起手";
            else if (SGESettings.Instance.opener == 3) opener = "70级起手";
            if (ImGui.BeginCombo("", opener))
            {
                if (ImGui.Selectable("常规起手"))
                {
                    SGESettings.Instance.opener = 0;
                    SGESettings.Instance.Save();
                }
                if (ImGui.Selectable("贤炮起手"))
                {
                    SGESettings.Instance.opener = 1;
                    SGESettings.Instance.Save();
                }
                if (ImGui.Selectable("绝亚起手"))
                {
                    SGESettings.Instance.opener = 2;
                    SGESettings.Instance.Save();
                }
                if(ImGui.Selectable("70级起手"))
                {
                    SGESettings.Instance.opener = 3;
                    SGESettings.Instance.Save();
                }
                ImGui.EndCombo();
            }
            bool a = SGESettings.Instance.康复tea;
            if (ImGui.Checkbox("绝亚康复(默认H2顺序)", ref SGESettings.Instance.康复tea))
            {
                SGESettings.Instance.Save(); 
                a = SGESettings.Instance.康复tea; 
            }
            if (a)
            {
                if (ImGui.Checkbox("使用H1顺序", ref SGESettings.Instance.H1)) SGESettings.Instance.Save(); 
            }
            SGESettings.Instance.time = Math.Clamp(SGESettings.Instance.time, 1000, 1600);
            bool b = SGESettings.Instance.关心最低血量T;
            if (ImGui.Checkbox("关心最低血量T", ref SGESettings.Instance.关心最低血量T)) SGESettings.Instance.Save();
            {
                SGESettings.Instance.Save();
                b = SGESettings.Instance.关心最低血量T; 
            }
            if (b)
            {
                if (ImGui.SliderFloat("当有T少于血时触发换绑", ref SGESettings.Instance.最低血量T, 0.01f, 1f)) SGESettings.Instance.Save(); 
            }
            if (ImGui.Checkbox($"少于2豆时使用根素", ref SGESettings.Instance.防根素溢出))
            {
                SGESettings.Instance.Save(); 
                a = SGESettings.Instance.防根素溢出; 
            }
            if (ImGui.InputInt("起手注药预读时间", ref SGESettings.Instance.time)) SGESettings.Instance.Save();
        }
        if (ImGui.CollapsingHeader("插入技能状态"))
        {
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }
            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }
            ImGui.Text("-------能力技-------");
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                    ImGui.Text(spell.ToString());
            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(spell.ToString());
        }
        // if (ImGui.CollapsingHeader("杂项设置"))
        // {
        //     if (ImGui.InputInt("P12S康复小队列表", ref SGESettings.Instance.Esuna)) SGESettings.Instance.Save();
        //     if (ImGui.InputInt("P12S康复层数", ref SGESettings.Instance.stack)) SGESettings.Instance.Save();
        //     if (ImGui.Checkbox("自动治疗", ref SGESettings.Instance.AutoHeal)) SGESettings.Instance.Save();
        // }
        /*if (ImGui.CollapsingHeader("调试"))
        {
            ImGui.Begin("调试窗口");
            var sageApi = Core.Resolve<JobApi_Sage>();
            if (sageApi != null && 20000 - sageApi.AddersgallTimer < 20000)
                ImGui.Text($"豆子进度：{20 - sageApi.AddersgallTimer}秒");
            if (sageApi != null && 20000 - sageApi.AddersgallTimer >= 20000)
                ImGui.Text($"豆子进度：已满");
            if (PartyHelper.CastableTanks != null && PartyHelper.CastableTanks.Count != 0)
            {
                var kardiaT = SGEAbility_Kardia2.Get心关T();
                if (kardiaT != null)
                    ImGui.Text($"心关目标T：{kardiaT.Name}");
            }
            var lowestHpT = SageTargetHelper.Get最低血量T();
            if (lowestHpT != null && lowestHpT.Name != Core.Me.Name)
                ImGui.Text($"最低血量T：{lowestHpT.Name}");
            if (PartyHelper.CastableParty != null && PartyHelper.CastableParty.Count() > 1)
            {
                ImGui.Text($"MT：{PartyHelper.CastableTanks.ElementAtOrDefault(0)?.Name}");
                ImGui.Text($"ST：{PartyHelper.CastableTanks.ElementAtOrDefault(1)?.Name}");
                ImGui.Text($"H1：{PartyHelper.CastableHealers.ElementAtOrDefault(0)?.Name}");
                ImGui.Text($"H2：{PartyHelper.CastableHealers.ElementAtOrDefault(1)?.Name}");
                ImGui.Text($"D1：{PartyHelper.CastableDps.ElementAtOrDefault(0)?.Name}");
                ImGui.Text($"D2：{PartyHelper.CastableDps.ElementAtOrDefault(1)?.Name}");
                ImGui.Text($"D3：{PartyHelper.CastableDps.ElementAtOrDefault(2)?.Name}");
                ImGui.Text($"D4：{PartyHelper.CastableDps.ElementAtOrDefault(3)?.Name}");
                ImGui.Text($"窒息H2康复目标：{SageTargetHelper.GetH2康复目标()?.Name}");
                ImGui.Text($"窒息H1康复目标：{SageTargetHelper.GetH1康复目标()?.Name}");
            }

            ImGui.Text($"自己：{Core.Me?.Name},{Core.Me?.DataId},{Core.Me?.Position}");
            var currentTarget = Core.Me?.GetCurrTarget();
            if (currentTarget != null)
                ImGui.Text($"目标：{currentTarget.Name},{currentTarget.DataId},{currentTarget.Position}");

            ImGui.Text($"是否移动：{MoveHelper.IsMoving()}");
            ImGui.Text($"小队人数：{PartyHelper.CastableParty?.Count ?? 0}");
            ImGui.Text($"25米内敌方人数：{TargetHelper.GetNearbyEnemyCount(Core.Me, 25, 25)}");
            ImGui.Text($"20米内小队人数：{PartyHelper.CastableAlliesWithin20?.Count ?? 0}");
            ImGui.Text($"gcd是否可用：{GCDHelper.CanUseGCD()}");
            ImGui.Text($"gcd剩余时间：{GCDHelper.GetGCDCooldown()}");
            var memApiSpell = Core.Resolve<MemApiSpell>();
            if (memApiSpell != null)
                ImGui.Text($"gcd总时间：{memApiSpell.GetGCDDuration()}");

            var lastSpellCast = Core.Resolve<MemApiSpellCastSuccess>();
            if (lastSpellCast != null)
            {
                ImGui.Text($"上个技能：{lastSpellCast.LastSpell}");
                ImGui.Text($"上个GCD：{lastSpellCast.LastGcd}");
                ImGui.Text($"上个能力技：{lastSpellCast.LastAbility}");
            }

            if (memApiSpell != null)
                ImGui.Text($"上个连击技能：{memApiSpell.GetLastComboSpellId()}");

            ImGui.End();
        }*/

    }
    
}

public static class Qt
{
    public static bool GetQt(string qtName)
    {
        return SageRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return SageRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return SageRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        SageRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        SageRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        SageRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return SageRotationEntry.JobViewWindow.GetQtArray();
    }
}