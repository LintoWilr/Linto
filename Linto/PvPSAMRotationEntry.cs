using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.LintoPvP.SAM;
using Linto.LintoPvP.SAM.Ability;
using Linto.LintoPvP.SAM.GCD;
using Linto.LintoPvP.SAM.Triggers;

namespace Linto;



public class PvP崩破大王Entry : IRotationEntry
{
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "PvP崩破大王";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 斩铁剑(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 残心(),SlotMode.Always),
        new (new 刀背击打(),SlotMode.Always),
        new (new 斩浪(),SlotMode.Always),
        new (new 回返斩浪(),SlotMode.Always),
        new (new 回返雪月花(),SlotMode.Always),
        new (new 明镜(),SlotMode.Always),
        new (new 地天(),SlotMode.Always),
        new (new 雪月花(),SlotMode.Always),
        new (new 花车连击(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPSAMSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Samurai,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //   rot.AddTriggerAction(new LintoPvPSAMQt());
        rot.SetRotationEventHandler(new PvPSAMRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public static JobViewWindow? JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return PvP崩破大王Entry.JobViewWindow;
    }
    private PvPSamSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPSAMSettings.Instance.JobViewSave, PvPSAMSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPSAMOverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPSAMOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        //JobViewWindow.AddTab("解锁", PvPSAMOverlay.DrawDev);
        JobViewWindow.AddQt("斩铁剑", true, "对能斩掉的敌人斩铁");
        JobViewWindow.AddQt("地天", true, "人群中开地天");
        JobViewWindow.AddQt("残心", true);
        JobViewWindow.AddQt("连击", true);
        JobViewWindow.AddQt("刀背击打", true);
        JobViewWindow.AddQt("明镜", true);
        JobViewWindow.AddQt("斩浪", true);
        JobViewWindow.AddQt("雪月花", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("手动斩铁", new HotkeyData.武士LB());
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

//    ImGui.SameLine();
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
