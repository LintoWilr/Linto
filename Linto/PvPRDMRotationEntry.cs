using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.LintoPvP.RDM;
using Linto.LintoPvP.RDM.Ability;
using Linto.LintoPvP.RDM.GCD;
using Linto.LintoPvP.RDM.Triggers;


namespace Linto;



public class PvPRDMEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "PvP赤魔";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 剑身强部(),SlotMode.Always),
        new (new 法师职能技能(),SlotMode.Always),
        new (new 荆棘环绕(),SlotMode.Always),
        new (new 光芒四射(),SlotMode.Always),
        new (new 鼓励(),SlotMode.Always),
        new (new 决断(),SlotMode.Always),
        new (new 焦热(),SlotMode.Always),
        new (new 魔三连(),SlotMode.Always),
        new (new 显贵冲击(),SlotMode.Always),
        new (new 激荡(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPRDMSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.RedMage,
            AcrType = AcrType.PVP,
            MinLevel = 0,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //    rot.AddTriggerAction(new LintoPvPRDMQt());
        rot.SetRotationEventHandler(new PvPRDMRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public static JobViewWindow? JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return PvPRDMEntry.JobViewWindow;
    }
    private PvPRDMSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPRDMSettings.Instance.JobViewSave, PvPRDMSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPPCTOverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPRDMOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        //JobViewWindow.AddTab("解锁", PvPPCTOverlay.DrawDev);
        JobViewWindow.AddQt("读条连击", true);
        JobViewWindow.AddQt("魔四连", true);
        JobViewWindow.AddQt("剑身强部", true);
        JobViewWindow.AddQt("鼓励", true);
        JobViewWindow.AddQt("光芒四射", true);
        JobViewWindow.AddQt("决断", true);
        JobViewWindow.AddQt("职能技能", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("突进", new HotKeyResolver_NormalSpell(29699U, SpellTargetType.Target, false));
        JobViewWindow.AddHotkey("后跳", new HotKeyResolver_NormalSpell(29700U, SpellTargetType.Target, false));
        JobViewWindow.AddHotkey("LB", new HotkeyData.赤魔LB());
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

//    ImGui.PCTeLine();
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
