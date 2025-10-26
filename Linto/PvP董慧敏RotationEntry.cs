using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.PCT;
using Linto.LintoPvP.PCT.Ability;
using Linto.LintoPvP.PCT.GCD;
using Linto.LintoPvP.PCT.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;


namespace Linto;



public class PvP董慧敏Entry : IRotationEntry
{
    public void Dispose()
    {
    }

    public string OverlayTitle { get; } = "PvP董慧敏";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 法师职能技能(),SlotMode.Always),
        new (new 坦培拉涂层(),SlotMode.Always),
        new (new 天星棱光(),SlotMode.Always),
        new (new 莫古力炮(),SlotMode.Always),
        new (new 动物构想(),SlotMode.Always),
        new (new 黑白魔法(),SlotMode.Always),
        new (new 切换减色(),SlotMode.Always),
        new (new 动物彩绘(),SlotMode.Always),
        new (new 瞬发连击(),SlotMode.Always),
        new (new 读条连击(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPPCTSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Pictomancer,
            AcrType = AcrType.PVP,
            MinLevel = 0,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.3适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //  rot.AddTriggerAction(new LintoPvPPCTQt());
        rot.SetRotationEventHandler(new PvPPCTRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }


    public static JobViewWindow JobViewWindow { get; private set; } = null!;

    public IRotationUI GetRotationUI()
    {

        return PvP董慧敏Entry.JobViewWindow;
    }
    private PvPPCTSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPPCTSettings.Instance.JobViewSave, PvPPCTSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPPCTOverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPPCTOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        //JobViewWindow.AddTab("解锁", PvPPCTOverlay.DrawDev);
        JobViewWindow.AddQt("瞬发连击", true);
        JobViewWindow.AddQt("读条连击", true);
        JobViewWindow.AddQt("动物彩绘", true);
        JobViewWindow.AddQt("动物构想", true);
        JobViewWindow.AddQt("黑白Aoe", true);
        JobViewWindow.AddQt("莫古力炮", true);
        JobViewWindow.AddQt("坦培拉涂层", true);
        JobViewWindow.AddQt("切换减色", false);
        JobViewWindow.AddQt("天星棱光", true);
        JobViewWindow.AddQt("职能技能", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("LB", new HotkeyData.画家LB());
        JobViewWindow.AddHotkey("镜头方向速滑", new HotkeyData.速涂());
        //JobViewWindow.AddHotkey("监控",new HotkeyData.监控());
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
