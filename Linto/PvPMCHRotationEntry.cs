using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.MCH.Ability;
using Linto.LintoPvP.MCH.GCD;
using Linto.LintoPvP.MCH.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.UI;

namespace Linto;

public class PvPMCHRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPMCHOverlay _lazyOverlay = new PvPMCHOverlay();
    private PvPMCHSettingUI settingUI = new();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 勇气(),SlotMode.Always),
        new (new 分析(),SlotMode.Always),
        new (new 全金属爆发(),SlotMode.Always),
        new (new 野火(),SlotMode.Always),
        new (new 热冲击(),SlotMode.Always),
        new (new 空气锚(),SlotMode.Always),
        new (new 回转飞锯(),SlotMode.Always),
        new (new 毒菌冲击(),SlotMode.Always),
        new (new 钻头(),SlotMode.Always),
        new (new 速度之星(),SlotMode.Always),
        new (new 象式浮空炮塔(),SlotMode.Always),
        new (new 霰弹枪(),SlotMode.Always),
        new (new 蓄力冲击(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };

    public string OverlayTitle { get; } = "绝枪";

    public Rotation Build(string settingFolder)
    {
        PvPMCHSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Machinist,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈",
        };
        rot.SetRotationEventHandler(new PvPMCHRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
        return rot;
    }

    public void BuildQt()
    {
        JobViewWindow = new JobViewWindow(PvPMCHSettings.Instance.JobViewSave, PvPMCHSettings.Instance.Save, OverlayTitle);
        //   jobViewWindow.AddTab("看你的", _lazyOverlay.Draw目标监控窗口);
        JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        JobViewWindow.AddQt("蓄力冲击", false);
        JobViewWindow.AddQt("钻头", true, "");
        JobViewWindow.AddQt("空气锚", true, "");
        JobViewWindow.AddQt("毒菌冲击", true, "");
        JobViewWindow.AddQt("回转飞锯", true, "");
        JobViewWindow.AddQt("霰弹枪", false, "");
        JobViewWindow.AddQt("野火", true, "");
        JobViewWindow.AddQt("分析", true, "");
        JobViewWindow.AddQt("职能技能", true, "");
        JobViewWindow.AddQt("浮空炮", true, "");
        JobViewWindow.AddQt("全金属爆发", true, "");
        JobViewWindow.AddQt("喝热水", true);
        JobViewWindow.AddQt("自动净化", true);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("智能目标:LB最低范围内最低血量,请确保没有高低差视野阻挡\n未启用智能目标:选中目标", new HotkeyData.机工LB());
        JobViewWindow.AddHotkey("对当前目标释放霰弹枪", new HotkeyData.霰弹枪());
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