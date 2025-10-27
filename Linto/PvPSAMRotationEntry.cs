using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.LintoPvP.SAM;
using Linto.LintoPvP.SAM.Ability;
using Linto.LintoPvP.SAM.GCD;
using Linto.LintoPvP.SAM.Triggers;
using Linto.UI;

namespace Linto;

public class PvP崩破大王Entry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPSAMOverlay _lazyOverlay = new PvPSAMOverlay();
    private PvPSamSettingUI settingUI = new();
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

    public string OverlayTitle { get; } = "绝枪";

    public Rotation Build(string settingFolder)
    {
        PvPSAMSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Samurai,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈",
        };
        rot.SetRotationEventHandler(new PvPSAMRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
        return rot;
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPSAMSettings.Instance.JobViewSave, PvPSAMSettings.Instance.Save, OverlayTitle);
        JobViewWindow.AddTab("职业配置", PvPSAMOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
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
