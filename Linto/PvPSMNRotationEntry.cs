using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SMN;
using Linto.LintoPvP.SMN.Ability;
using Linto.LintoPvP.SMN.GCD;
using Linto.LintoPvP.SMN.Triggers;
using Linto.UI;
using 冲刺 = Linto.LintoPvP.SMN.Ability.冲刺;


namespace Linto;



public class PvPSMNRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPSMNOverlay _lazyOverlay = new PvPSMNOverlay();
    private PvPSMNSettingUI settingUI = new();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 龙神迸发(),SlotMode.Always),
        new (new 法师职能技能(),SlotMode.Always),
        new (new 坏死爆发(),SlotMode.Always),
        new (new 守护之光(),SlotMode.Always),
        new (new 山崩(),SlotMode.Always),
        new (new 深红旋风(),SlotMode.Always),
        new (new 深红强袭(),SlotMode.Always),
        new (new 螺旋气流(),SlotMode.Always),
        new (new 星极脉冲(),SlotMode.Always),
        new (new 毁荡(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };

    public string OverlayTitle { get; } = "绝枪";

    public Rotation Build(string settingFolder)
    {
        PvPSMNSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Summoner,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈",
        };
        rot.SetRotationEventHandler(new PvPSMNRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
        return rot;
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
        JobViewWindow.AddQt("职能技能", true);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        //   JobViewWindow.AddHotkey("龙神召唤(选中目标)",new 龙神召唤());
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
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