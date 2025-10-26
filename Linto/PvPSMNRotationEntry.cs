using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SMN;
using Linto.LintoPvP.SMN.Ability;
using Linto.LintoPvP.SMN.GCD;
using Linto.LintoPvP.SMN.Triggers;
using 冲刺 = Linto.LintoPvP.SMN.Ability.冲刺;


namespace Linto;



public class PvPSMNRotationEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "PvPSMN";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
        new (new 龙神迸发(),SlotMode.Always),
        new (new 溃烂爆发(),SlotMode.Always),
        new (new 守护之光(),SlotMode.Always),
        new (new 山崩(),SlotMode.Always),
        new (new 深红旋风(),SlotMode.Always),
        new (new 深红强袭(),SlotMode.Always),
        new (new 螺旋气流(),SlotMode.Always),
        new (new 星极脉冲(),SlotMode.Always),
        new (new 毁荡(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPSMNSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Summoner,
            AcrType = AcrType.PVP,
            MinLevel = 0,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //  rot.AddTriggerAction(new LintoPvPSMNQt());
        rot.SetRotationEventHandler(new PvPSMNRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public static JobViewWindow? JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return PvPSMNRotationEntry.JobViewWindow;
    }
    private PvPSMNSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPSMNSettings.Instance.JobViewSave, PvPSMNSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPSMNOverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPSMNOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        //JobViewWindow.AddTab("解锁", PvPSMNOverlay.DrawDev);
        JobViewWindow.AddQt("深红旋风", true);
        JobViewWindow.AddQt("深红强袭", true);
        JobViewWindow.AddQt("山崩", true);
        JobViewWindow.AddQt("螺旋气流", true);
        JobViewWindow.AddQt("坏死爆发", true);
        JobViewWindow.AddQt("守护之光", true, "50血以下对自己使用");
        JobViewWindow.AddQt("毁荡", true, "没事干就打1111吧");
        JobViewWindow.AddQt("龙神迸发", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        //   JobViewWindow.AddHotkey("龙神召唤(选中目标)",new 龙神召唤());
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
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

//    ImGui.SMNeLine();
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
