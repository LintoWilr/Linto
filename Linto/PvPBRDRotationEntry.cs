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

namespace Linto;

public class PvPBRDRotationEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public IRotationUI GetRotationUI()
    {
        return PvPBRDRotationEntry.JobViewWindow;
    }
    private PvPBRDSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public static JobViewWindow JobViewWindow;
    private PvPBRDOverlay _lazyOverlay =  new PvPBRDOverlay();
    public List<SlotResolverData> SlotResolvers = new()
    {
        new (new 净化(),SlotMode.Always),
        new (new 药(),SlotMode.Always),
        new (new 光阴神(),SlotMode.Always),
        new (new 速度之星(),SlotMode.Always),
        new (new 勇气(),SlotMode.Always),
        new (new 九天连箭(),SlotMode.Always),
        new (new 英雄的返场余音(),SlotMode.Always),
        new (new 默者的夜曲(),SlotMode.Always),
        new (new 爆破箭(),SlotMode.Always),
        new (new 绝峰箭(),SlotMode.Always),
        new (new 完美音调(),SlotMode.Always),
        new (new 强劲射击(),SlotMode.Always),
        new (new 冲刺(),SlotMode.Always),
    };

    public string OverlayTitle { get; } = "巴德";
    
    public void DrawOverlay()
    {
    }
    public string AuthorName { get; set; } = "Linto PvP";
    public Rotation Build(string settingFolder)
    {
        PvPBRDSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Bard,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[1级码及以上使用]不定时更新,有问题DC频道反馈\n[7.1适配]",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
       // rot.AddTriggerAction(new LintoPvPBRDQt());
        rot.SetRotationEventHandler(new PvPBRDRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public void BuildQt()
    {
         JobViewWindow = new JobViewWindow(PvPBRDSettings.Instance.JobViewSave, PvPBRDSettings.Instance.Save, OverlayTitle);
         //   jobViewWindow.AddTab("看你的", _lazyOverlay.Draw目标监控窗口);
         JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
         JobViewWindow.AddTab("监控",PVPHelper.监控);
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
         JobViewWindow.AddHotkey("疾跑",new HotKeyResolver_NormalSpell(29057U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("龟壳",new HotKeyResolver_NormalSpell(29054U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("热水",new HotKeyResolver_NormalSpell(29711U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("LB",new HotkeyData.诗人LB());
         JobViewWindow.AddHotkey("后跳",new HotkeyData.后射());
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