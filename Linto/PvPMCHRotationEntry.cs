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

namespace Linto;


public class PvPMCHRotationEntry : IRotationEntry
{
    public void Dispose()
    {
    }
    public IRotationUI GetRotationUI()
    {
        return PvPMCHRotationEntry.JobViewWindow;
    }
    private PvPMCHSettingUI settingUI = new();
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    public static JobViewWindow JobViewWindow;
    private PvPMCHOverlay _lazyOverlay =  new PvPMCHOverlay();
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

    public string OverlayTitle { get; } = "机工";
    
    public void DrawOverlay()
    {
    }
    public string AuthorName { get; set; } = "Linto PvP";
    public Rotation Build(string settingFolder)
    {
        PvPMCHSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        BuildQt();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Machinist,
            AcrType = AcrType.PVP,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "机工",
        };
        //rot.AddSlotSequences(new TriggerAction_QT());
    //    rot.AddTriggerAction(new LintoPvPMCHQt());
        rot.SetRotationEventHandler(new PvPMCHRotationEventHandler());
        rot.AddOpener(GetOpener);
        return rot;
    }
    public void BuildQt()
    {
         JobViewWindow = new JobViewWindow(PvPMCHSettings.Instance.JobViewSave, PvPMCHSettings.Instance.Save, OverlayTitle);
         //   jobViewWindow.AddTab("看你的", _lazyOverlay.Draw目标监控窗口);
         JobViewWindow.AddTab("职业配置", _lazyOverlay.DrawGeneral);
         JobViewWindow.AddTab("监控",PVPHelper.监控);
         JobViewWindow.AddTab("共通配置", PVPHelper.配置);
         JobViewWindow.AddQt("蓄力冲击", false);
         JobViewWindow.AddQt("钻头", true,"");
         JobViewWindow.AddQt("空气锚", true,"");
         JobViewWindow.AddQt("毒菌冲击", true,"");
         JobViewWindow.AddQt("回转飞锯", true,"");
         JobViewWindow.AddQt("霰弹枪", false,"");
         JobViewWindow.AddQt("野火", true,"");
         JobViewWindow.AddQt("分析", true,"");
         JobViewWindow.AddQt("职能技能", true,"");
         JobViewWindow.AddQt("浮空炮", true,"");
         JobViewWindow.AddQt("全金属爆发", true,"");
         JobViewWindow.AddQt("喝热水", true);
         JobViewWindow.AddQt("自动净化", true);
         JobViewWindow.AddQt("冲刺", true);
         JobViewWindow.AddHotkey("疾跑",new HotKeyResolver_NormalSpell(29057U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("龟壳",new HotKeyResolver_NormalSpell(29054U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("热水",new HotKeyResolver_NormalSpell(29711U,SpellTargetType.Self,false));
         JobViewWindow.AddHotkey("智能目标:LB最低范围内最低血量,请确保没有高低差视野阻挡\n未启用智能目标:选中目标",new HotkeyData.机工LB());
         JobViewWindow.AddHotkey("对当前目标释放霰弹枪",new HotkeyData.霰弹枪());
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