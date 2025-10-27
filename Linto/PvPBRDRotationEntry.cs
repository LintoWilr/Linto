using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.BRD;
using Linto.LintoPvP.BRD.Ability;
using Linto.LintoPvP.BRD.GCD;
using Linto.LintoPvP.BRD.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.UI;

namespace Linto;

public class PvPBRDRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPBRDOverlay _lazyOverlay = new PvPBRDOverlay();
    private PvPBRDSettingUI settingUI = new();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(), SlotMode.Always),
        new (new 药(), SlotMode.Always),
        new (new 光阴神(), SlotMode.Always),
        new (new 速度之星(), SlotMode.Always),
        new (new 勇气(), SlotMode.Always),
        new (new 九天连箭(), SlotMode.Always),
        new (new 英雄的返场余音(), SlotMode.Always),
        new (new 默者的夜曲(), SlotMode.Always),
        new (new 爆破箭(), SlotMode.Always),
        new (new 绝峰箭(), SlotMode.Always),
        new (new 完美音调(), SlotMode.Always),
        new (new 强劲射击(), SlotMode.Always),
        new (new 冲刺(), SlotMode.Always),
    };

    public string OverlayTitle { get; } = "巴德";

    public Rotation Build(string settingFolder)
    {
        PvPBRDSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Bard,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        rot.SetRotationEventHandler(new PvPBRDRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
        return rot;
    }

    private void BuildQt()
    {
        JobViewWindow = new JobViewWindow(PvPBRDSettings.Instance.JobViewSave, PvPBRDSettings.Instance.Save, OverlayTitle);
        JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        JobViewWindow.AddQt("和弦箭", true);
        JobViewWindow.AddQt("光阴神", true);
        JobViewWindow.AddQt("沉默", true);
        JobViewWindow.AddQt("爆破箭", true);
        JobViewWindow.AddQt("绝峰箭", true);
        JobViewWindow.AddQt("强劲射击", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("职能技能", true);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("LB", new HotkeyData.诗人LB());
        JobViewWindow.AddHotkey("后跳", new HotkeyData.后射());
    }

    private void Build监控窗口()
    {
        // 初始化窗口（内部已注册独立绘制事件）
        监控窗口 = new NewWindow(() => PvPSettings.Instance.Save());
        监控窗口.SetUpdateAction(On监控Update);
    }
    public void On监控Update()
    {
        //窗口的更新逻辑
    }
    private void BuildCompositeUI()
    {
        _compositeUI = new CompositeRotationUI(new List<IRotationUI>
        {
            监控窗口,
            JobViewWindow
        });
    }

    public IRotationUI GetRotationUI()
    {
        return _compositeUI;
    }

    public void OnDrawSetting()
    {
        settingUI.Draw();
    }

    public void Dispose()
    {
        监控窗口?.Dispose();
    }
}