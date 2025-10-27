using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.GNB.Ability;
using Linto.LintoPvP.GNB.GCD;
using Linto.LintoPvP.GNB.Triggers;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.UI;

namespace Linto;

public class PvPGNBRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Linto PvP";
    public static JobViewWindow JobViewWindow { get; private set; }
    private NewWindow 监控窗口; // 监控
    private CompositeRotationUI _compositeUI; // 组合 UI
    private PvPGNBOverlay _lazyOverlay = new PvPGNBOverlay();
    private PvPGNBSettingUI settingUI = new();
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

    public string OverlayTitle { get; } = "绝枪";

    public Rotation Build(string settingFolder)
    {
        PvPGNBSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        Build监控窗口(); // 初始化 监控窗口 
        BuildCompositeUI(); // 组合 UI
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Gunbreaker,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈",
        };
        rot.SetRotationEventHandler(new PvPGNBRotationEventHandler());
        // 如果需要起手，添加 GetOpener 逻辑
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