using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.LintoPvP.VPR;
using Linto.LintoPvP.VPR.Ability;
using Linto.LintoPvP.VPR.GCD;
using Linto.LintoPvP.VPR.Triggers;


namespace Linto;



public class PvP双刀小子Entry : IRotationEntry
{
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "PvP双刀小子";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 蛇续剑(),SlotMode.Always),
        new (new 飞蛇之魂(),SlotMode.Always),
        new (new 祖灵之牙连击(),SlotMode.Always),
        new (new BUFF连击(),SlotMode.Always),
        new (new 飞蛇之尾(),SlotMode.Always),
        new (new 齿牙体势连击(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPVPRSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Viper,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
      //  rot.AddTriggerAction(new LintoPvPVPRQt());
        rot.SetRotationEventHandler(new PvPVPRRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public static JobViewWindow JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return PvP双刀小子Entry.JobViewWindow;
    }
    private PvPVPRSettingUI settingUI = new();
    public void OnDrawSetting()
    {
       settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPVPRSettings.Instance.JobViewSave, PvPVPRSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPVPROverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPVPROverlay.DrawGeneral);
        JobViewWindow.AddTab("监控",PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        JobViewWindow.AddQt("连击", true);
        JobViewWindow.AddQt("蛇续剑", true);
        JobViewWindow.AddQt("飞蛇之魂", true);
        JobViewWindow.AddQt("飞蛇之尾", true);
        JobViewWindow.AddQt("BUFF连击", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑",new HotKeyResolver_NormalSpell(29057U,SpellTargetType.Self,false));
        JobViewWindow.AddHotkey("龟壳",new HotKeyResolver_NormalSpell(29054U,SpellTargetType.Self,false));
        JobViewWindow.AddHotkey("热水",new HotKeyResolver_NormalSpell(29711U,SpellTargetType.Self,false));
        JobViewWindow.AddHotkey("LB",new HotkeyData.蛇LB());
    }
    private IOpener? GetOpener(uint level)
    {
        return null;
    }
}
        //if (ImGui.CollapsingHeader("插入技能状态"))
        //{
        //    if (ImGui.Button("清除队列"))
        //    {
        //        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
        //        AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
        //    }

        //    ImGui.VPReLine();
        //    if (ImGui.Button("清除一个"))
        //    {
        //        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
        //        AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
        //    }

        //    ImGui.Text("-------能力技-------");
        //    if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
        //        foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
        //            ImGui.Text(spell.Name);
        //    ImGui.Text("-------GCD-------");
        //    if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        //        foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
        //            ImGui.Text(spell.Name);
        //}
