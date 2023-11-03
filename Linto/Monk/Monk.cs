#region

using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.View.JobView;
using Common;
using Common.GUI;
using Common.Helper;
using Common.Language;
using ImGuiNET;

#endregion

namespace Linto.Monk;

public class MNKOverlay
{
    private bool isHorizontal;
    private bool ShowWhy;
    private bool ShowWhy2;
    private bool Potion;
    private bool 原神;
    private bool 米卫兵;
    private bool 阅读使用说明 ;
    public bool 设置;
    public void DrawGeneral(JobViewWindow jobViewWindow)
    {
        // if (ImGui.CollapsingHeader("设置"))
        // {
        //     ImGui.Text("勾上也许有用");
        //     if (ImGui.Checkbox("轻身步法（还没加）", ref MNKSettings.Instance.轻身步法)) MNKSettings.Instance.Save();
        //     if (ImGui.Checkbox("猫男模式（没有任何作用）", ref MNKSettings.Instance.猫男模式)) MNKSettings.Instance.Save();
        //     if (ImGui.Checkbox("TP（空的）", ref MNKSettings.Instance.TP)) MNKSettings.Instance.Save();
        //     if (ImGui.Checkbox("日随模式（勾不勾也没区别）", ref MNKSettings.Instance.TP)) MNKSettings.Instance.Save();
        // }
        if (!阅读使用说明&& (MNKSettings.Instance.使用说明))
        { 
            if (ImGui.BeginTable("使用说明", 1))
            { 
                ImGui.TableSetupColumn("使用说明", ImGuiTableColumnFlags.None); 
                ImGui.TableHeadersRow();
                
                ImGui.TableNextRow(); 
                ImGui.TableNextColumn(); 
                ImGui.Text("本ACR主要面对日随练级使用，进行了全等级适配。\n循环逻辑为日随模式\n高难请使用Archetto Monk2\n根据技速不同可能会出现丢GCD，能力技晚放的情况"); 
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
                ImGui.Text("有问题可以群里@Linto处理"); 
                if (ShowWhy2) 
                { 
                    if (ImGui.SmallButton("不再显示使用说明")) 
                    { 
                        阅读使用说明 = true; 
                        ShowWhy2 = false; 
                        MNKSettings.Instance.使用说明 = false; 
                        MNKSettings.Instance.Save();
                    } 
                } 
            } 
        }
        if (ImGui.CollapsingHeader("设置")) 
        {
            if (ImGui.SliderFloat("内丹阈值", ref MNKSettings.Instance.内丹阈值设置, 0.0f, 1.0f))
            {
                MNKSettings.Instance.Save();
            }
            if (ImGui.SliderFloat("浴血阈值", ref MNKSettings.Instance.浴血阈值设置, 0.0f, 1.0f))
            {
                MNKSettings.Instance.Save();
            }
            if (ImGui.SliderFloat("金刚阈值", ref MNKSettings.Instance.金刚阈值设置, 0.0f, 1.0f))
            {
                MNKSettings.Instance.Save();
            }
            if (ImGui.SliderFloat("真言阈值", ref MNKSettings.Instance.真言阈值设置, 0.0f, 1.0f)) 
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
            ImGuiHelper.ToggleButton("脱战使用演武（待优化）", ref MNKSettings.Instance.脱战演武);
            {
            }
            if (ImGui.TreeNode("参数调整"))
            {
                ImGui.SliderFloat("功力时间设定", ref MNKSettings.Instance.功力时间, 4000f, 5000f);
                MNKSettings.Instance.Save();
                ImGui.TreePop();
            }
            if ( !ImGui.TreeNode("DEV"))
            {

                if (ImGui.TreeNode("循环"))
                {
                    ImGui.Text($"爆发药：{Qt.GetQt("爆发药")}");
                    ImGui.Text($"gcd是否可用：{AI.Instance.CanUseGCD()}");
                    ImGui.Text($"gcd剩余时间：{AI.Instance.GetGCDCooldown()}");
                    ImGui.Text($"gcd总时间：{AI.Instance.GetGCDDuration()}");
                    ImGui.TreePop();
                }


                if (ImGui.TreeNode("技能释放"))
                {
                    ImGui.Text($"上个技能：{Core.Get<IMemApiSpellCastSucces>().LastSpell}");
                    ImGui.Text($"上个GCD：{Core.Get<IMemApiSpellCastSucces>().LastGcd}");
                    ImGui.Text($"上个能力技：{Core.Get<IMemApiSpellCastSucces>().LastAbility}");
                    ImGui.TreePop();
                }

                if (ImGui.TreeNode("小队"))
                {
                    ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
                    ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
                    ImGui.TreePop();
                }
            /*if (Qt.GetQt("吃爆发药"))
            {
                if (ImGui.Checkbox("无论如何我都要吃爆发药", ref MNKSettings.Instance.吃爆发药));
                {
                    if (MNKSettings.Instance.吃爆发药) 
                    {
                        ImGui.Text("你吃个屁！不许吃！把QT关了！"); 
                        if (ImGui.SmallButton("我就是要吃！我就是要吃！"))ImGui.SetTooltip("你真的要吃吗");
                        {
                            Potion = true;
                            if (Potion = true);  
                            { 
                                Core.Get<IMemApiSendMessage>().SendMessage("/xlkill");
                            }
                        } 
                    }
                    MNKSettings.Instance.Save();
                }
            }*/
        }
        /*if (ImGui.CollapsingHeader("插入技能状态"))
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
                    ImGui.Text(spell.Name);
            ImGui.Text("-------GCD-------");
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(spell.Name);
    }*/
        

         //   if (ImGui.Button("额外打一个红斩"))
          //  {
         //       MNKBattleData.Instance.额外红斩++;
         //   }

         //   ImGui.Text($"额外红斩：{WARBattleData.Instance.额外红斩}");
        }
        if (ImGui.CollapsingHeader("更新日志")) 
        {
            ImGui.Text("11/1 出生");
            ImGui.Text("11/2 修复演武逻辑");
            ImGui.Text("11/3 悬浮窗简洁化");
        }
    }

    /*public void DrawTimeLine(JobViewWindow jobViewWindow)
    {
        var currTriggerline = AI.Instance.TriggerlineData.CurrTriggerLine;
        var notice = "无";
        if (currTriggerline != null) notice = $"[{currTriggerline.Author}]{currTriggerline.Name}";

        ImGui.Text(notice);
        if (currTriggerline != null)
        {
            ImGui.Text("导出变量:".Loc());
            ImGui.Indent();
            foreach (var v in currTriggerline.ExposedVars)
            {
                var oldValue = AI.Instance.ExposedVars.GetValueOrDefault(v);
                ImGuiHelper.LeftInputInt(v, ref oldValue);
                AI.Instance.ExposedVars[v] = oldValue;
            }

            ImGui.Unindent();
        }
    }*/

    public void DrawDev(JobViewWindow jobViewWindow)
    {
        if (ImGui.BeginTable("嘟嘟噜", 1))
        {
            ImGui.TableSetupColumn("未来可能加入功能", ImGuiTableColumnFlags.None);
            ImGui.TableHeadersRow();

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.Text("自动牵制");
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.Text("接入身位提示(打日随还在意身位？)");
            ImGui.EndTable();
        }
        if (!ImGui.TreeNode("米卫兵转化程序"))
            return;
        if (ImGui.Button("开始转化(有一定概率转化失败)"))
            玩原神();
        if (原神)
        { 
            if (ImGui.Button("我要玩原神！")) 
                装原神();
        }
        ImGui.TreePop();
    }
    
    private void 装原神()
    {
        if (new Random().Next(1, 7) == 1)
        {
            Core.Get<IMemApiSendMessage>().SendMessage("/sh 我要玩原神！我是米哈游的狗！"); 
            LogHelper.Print("你以为我会给你安装原神？");
        }
        else
            LogHelper.Print("很遗憾你玩不了原神，再试一次吧！");
    }
    private void 玩原神()
    {
        if (!米卫兵) 
        {
            if (new Random().Next(1, 7) == 1) 
            { 
                LogHelper.Print("恭喜你变成了米卫兵！获得了原神安装资格！"); 
                原神 = true; 
                米卫兵 = true; 
            }
            else
                LogHelper.Print("转化失败了···");
        }
        else
            LogHelper.Print("你已经是米卫兵了！");
            
    }
    private void 素琴百韵()
    {
        Core.Get<IMemApiSendMessage>().SendMessage("/xlkill");
    }
    
}

public static class Qt
{
    /// 获取指定名称qt的bool值
    public static bool GetQt(string qtName)
    {
        return MonkRotationEntry.JobViewWindow.GetQt(qtName);
    }

    /// 反转指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool ReverseQt(string qtName)
    {
        return MonkRotationEntry.JobViewWindow.ReverseQt(qtName);
    }

    /// 设置指定qt的值
    /// <returns>成功返回true，否则返回false</returns>
    public static bool SetQt(string qtName, bool qtValue)
    {
        return MonkRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
    }

    /// 重置所有qt为默认值
    public static void Reset()
    {
        MonkRotationEntry.JobViewWindow.Reset();
    }

    /// 给指定qt设置新的默认值
    public static void NewDefault(string qtName, bool newDefault)
    {
        MonkRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
    }

    /// 将当前所有Qt状态记录为新的默认值，
    /// 通常用于战斗重置后qt还原到倒计时时间点的状态
    public static void SetDefaultFromNow()
    {
        MonkRotationEntry.JobViewWindow.SetDefaultFromNow();
    }

    /// 返回包含当前所有qt名字的数组
    public static string[] GetQtArray()
    {
        return MonkRotationEntry.JobViewWindow.GetQtArray();
    }
}