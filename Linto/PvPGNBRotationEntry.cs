using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.GNB.Ability;
using Linto.LintoPvP.GNB.GCD;
using Linto.LintoPvP.GNB.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto;

public class PvPGNBRotationEntry : IRotationEntry
{
    public void Dispose()
    {
    }
        public IRotationUI GetRotationUI()
        {
            if (JobViewWindow == null)
            {
                JobViewWindow = new JobViewWindow(PvPGNBSettings.Instance.JobViewSave, PvPGNBSettings.Instance.Save, OverlayTitle);
            }
            return JobViewWindow;
        }
    private PvPGNBSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public static JobViewWindow? JobViewWindow;
    private PvPGNBOverlay _lazyOverlay = new PvPGNBOverlay();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 刚玉之心(),SlotMode.Always),
        new (new T职能技能(),SlotMode.Always),
        new (new 续剑(),SlotMode.Always),
        new (new 粗分斩(),SlotMode.Always),
        new (new 爆破领域(),SlotMode.Always),
        new (new 命运之环(),SlotMode.Always),
        new (new 烈牙连击(),SlotMode.Always),
        new (new 爆发击(),SlotMode.Always),
        new (new 残暴弹(),SlotMode.Always),
        new (new 迅连斩(),SlotMode.Always),
        new (new 利刃斩(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };

    public string OverlayTitle { get; } = "绝枪战士";

    public void DrawOverlay()
    {
    }
    public string AuthorName { get; set; } = "Linto PvP";
    public Rotation Build(string settingFolder)
    {
        PvPGNBSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Gunbreaker,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "绝枪",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //    rot.AddTriggerAction(new LintoPvPMCHQt());
        rot.SetRotationEventHandler(new PvPGNBRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public void BuildQt()
    {
        JobViewWindow = new JobViewWindow(PvPGNBSettings.Instance.JobViewSave, PvPGNBSettings.Instance.Save, OverlayTitle);
        //   jobViewWindow.AddTab("看你的", _lazyOverlay.Draw目标监控窗口);
        JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        JobViewWindow.AddQt("爆发击连击", true);
        JobViewWindow.AddQt("烈牙连击", true);
        JobViewWindow.AddQt("续剑", true);
        JobViewWindow.AddQt("粗分斩", true);
        JobViewWindow.AddQt("命运之环", true);
        JobViewWindow.AddQt("爆破领域", true);
        JobViewWindow.AddQt("刚玉之心", true);
        JobViewWindow.AddQt("职能技能", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("连续剑", new HotkeyData.绝枪LB());

    }
    private IOpener? GetOpener(uint level)
    {
        if (level < 90)
            return null;
        else
        {
            return null;
        }


    }
}
