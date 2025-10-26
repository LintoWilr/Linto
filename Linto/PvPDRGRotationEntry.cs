using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.DRG;
using Linto.LintoPvP.DRG.Ability;
using Linto.LintoPvP.DRG.GCD;
using Linto.LintoPvP.DRG.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto;


public class PvPDRGRotationEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public IRotationUI GetRotationUI()
    {
        return PvPDRGRotationEntry.JobViewWindow;
    }
    private PvPDRGSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public static JobViewWindow? JobViewWindow;
    private PvPDRGOverlay _lazyOverlay = new PvPDRGOverlay();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 高跳(),SlotMode.Always),
        new (new 后跳(),SlotMode.Always),
        new (new 天龙点睛(),SlotMode.Always),
        new (new 恐惧咆哮(),SlotMode.Always),
        new (new 武神枪(),SlotMode.Always),
        new (new 死者之岸(),SlotMode.Always),
        new (new 樱花缭乱(),SlotMode.Always),
        new (new 苍穹刺(),SlotMode.Always),
        new (new 云蒸龙变(),SlotMode.Always),
        new (new 龙尾大回旋(),SlotMode.Always),
        new (new 龙牙龙爪(),SlotMode.Always),
        new (new 龙眼雷电(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };

    public string OverlayTitle { get; } = "泽塔";

    public void DrawOverlay()
    {
    }
    public string AuthorName { get; set; } = "Linto PvP";
    public string Description { get; } = "干啥";
    public Rotation Build(string settingFolder)
    {
        PvPDRGSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Dragoon,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //  rot.AddTriggerAction(new LintoPvPDRGQt());
        rot.SetRotationEventHandler(new PvPDRGRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public void BuildQt()
    {
        JobViewWindow = new JobViewWindow(PvPDRGSettings.Instance.JobViewSave, PvPDRGSettings.Instance.Save, OverlayTitle);
        //   jobViewWindow.AddTab("看你的", _lazyOverlay.Draw目标监控窗口);
        JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        JobViewWindow.AddQt("基础连击", true);
        JobViewWindow.AddQt("武神枪", true, "阿尔贝斯之枪");
        JobViewWindow.AddQt("死者之岸", true, "无尽奇观");
        JobViewWindow.AddQt("樱花缭乱", true, "万千烈火");
        JobViewWindow.AddQt("高跳", false, "狂想曲");
        JobViewWindow.AddQt("苍穹刺", true, "愤怒之雨");
        JobViewWindow.AddQt("后跳", false, "怒火共鸣");
        JobViewWindow.AddQt("天龙点睛", true, "日珥俯冲");
        JobViewWindow.AddQt("恐惧咆哮", true, "爆裂·震天·咆哮");
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("镜头方向后跳", new HotkeyData.后跳());
    }

    private IOpener? GetOpener(uint level)
    {
        if (level < 90)
            return null;
        else
        {
            return null;
        }

        return null;
    }

}
