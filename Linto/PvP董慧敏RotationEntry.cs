using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.PCT;
using Linto.LintoPvP.PCT.Ability;
using Linto.LintoPvP.PCT.GCD;
using Linto.LintoPvP.PCT.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.UI;


namespace Linto;


public class PvP董慧敏Entry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPPCTOverlay _lazyOverlay = new PvPPCTOverlay();
    private PvPPCTSettingUI settingUI = new();
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

    public string OverlayTitle { get; } = "绝枪";

    public Rotation Build(string settingFolder)
    {
        PvPPCTSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Pictomancer,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈",
        };
        rot.SetRotationEventHandler(new PvPPCTRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
        return rot;
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
        // JobViewWindow.AddHotkey("镜头方向速滑", new HotkeyData.速涂());
        //JobViewWindow.AddHotkey("监控",new HotkeyData.监控());
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