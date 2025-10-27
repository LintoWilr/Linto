using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Linto.LintoPvP.BLM;
using Linto.LintoPvP.BLM.Ability;
using Linto.LintoPvP.BLM.GCD;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto;


public class PvPBLMEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "PvP黑魔";
    public string AuthorName { get; set; } = "Linto PvP";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 法师职能技能(),SlotMode.Always),
        new (new 昏沉(),SlotMode.Always),
        new (new 元素天赋(),SlotMode.Always),
        new (new 磁暴(),SlotMode.Always),
        new (new 耀星(),SlotMode.Always),
    //    new (new LB(),SlotMode.Always),  
        new (new 悖论(),SlotMode.Always),
        new (new 核爆(),SlotMode.Always),
      //  new (new 玄冰(),SlotMode.Always),
        new (new 火4(),SlotMode.Always),
        new (new 异言(),SlotMode.Always),
        new (new 火3(),SlotMode.Always),
        new (new 火2(),SlotMode.Always),
        new (new 火1(),SlotMode.Always),
        new (new 冰4(),SlotMode.Always),
        new (new 冰3(),SlotMode.Always),
        new (new 冰2(),SlotMode.Always),
        new (new 冰1(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };
    public Rotation Build(string settingFolder)
    {
        PvPBLMSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.BlackMage,
            AcrType = AcrType.PVP,
            MinLevel = 0,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
        //rot.AddTriggerAction(new LintoPvPBLMQt());
        rot.SetRotationEventHandler(new PvPBLMRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public static JobViewWindow? JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return PvPBLMEntry.JobViewWindow ?? throw new InvalidOperationException("JobViewWindow is not initialized");
    }
    private PvPBLMSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(PvPBLMSettings.Instance.JobViewSave, PvPBLMSettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPBLMOverlay.);
        //贤者ACR入口.职业视图窗口.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.职业视图窗口.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("职业配置", PvPBLMOverlay.DrawGeneral);
        JobViewWindow.AddTab("监控", PVPHelper.监控);
        JobViewWindow.AddTab("共通配置", PVPHelper.配置);
        //JobViewWindow.AddTab("解锁", PvPBLMOverlay.DrawDev);
        JobViewWindow.AddQt("悖论", true);
        JobViewWindow.AddQt("悖论", true);
        JobViewWindow.AddQt("耀星", true);
        JobViewWindow.AddQt("元素天赋", true);
        JobViewWindow.AddQt("异言", true);
        JobViewWindow.AddQt("昏沉", true);
        JobViewWindow.AddQt("磁暴", true);
        JobViewWindow.AddQt("火魔法", true);
        JobViewWindow.AddQt("冰魔法", true);
        JobViewWindow.AddQt("职能技能", false);
        JobViewWindow.AddQt("喝热水", false);
        JobViewWindow.AddQt("自动净化", false);
        JobViewWindow.AddQt("冲刺", true);
        JobViewWindow.AddHotkey("疾跑", new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("龟壳", new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("热水", new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false));
        JobViewWindow.AddHotkey("以太步", new HotkeyData.以太步());
        JobViewWindow.DrawQtWindow();
    }
    private IOpener? GetOpener(uint level)
    {
        return null;
    }
}