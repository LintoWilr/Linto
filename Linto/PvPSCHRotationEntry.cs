using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common.Define;
using Common.Language;
using ImGuiNET;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.SCH;
using Linto.LintoPvP.SCH.Ability;
using Linto.LintoPvP.SCH.GCD;

namespace Linto;


public class PvPSCHRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    private PvPSCHOverlay _lazyOverlay =  new PvPSCHOverlay();
    public List<ISlotResolver> SlotResolvers = new ()
    {
        new 药(),
        new 净化(),
        new 枯骨法(),
        new 蛊毒法(),
        new 鼓舞(),
        new 慰藉(),
        new 炽天召唤(),
        new 跑快快(),
        new 毒扩散(),
        new 鼓励扩散(),
        new 慰藉(),
        new 极炎法(),
    };

    public string OverlayTitle { get; } = "苗疆毒妇";
    
    public void DrawOverlay()
    {
    }
    public string AuthorName { get; } = "Linto PvP";
    public Jobs TargetJob { get; } = Jobs.Scholar;
    public AcrType AcrType { get; } = AcrType.PVP;
    public int MinLevel { get; } = 1;
    public int MaxLevel { get; } = 90;
    public string Description { get; } = "需要获取权限才能使用\n适用场景:PvP";

    public Rotation Build(string settingFolder)
    {
        PvPSCHSettings.Build(settingFolder);
        PvPSettings.Build(settingFolder);
        return new Rotation(this, () => SlotResolvers)
            .SetRotationEventHandler(new PvPSCHRotationEventHandler())
            .AddSlotSequences();
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
            jobViewWindow = new JobViewWindow(PvPSCHSettings.Instance.JobViewSave, PvPSCHSettings.Instance.Save, OverlayTitle);
            JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
            jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
       //     jobViewWindow.AddTab("更新日志", _lazyOverlay.更新日志);
            jobViewWindow.AddTab("解锁", _lazyOverlay.DrawDev);
            jobViewWindow.AddQt("极炎法", true);
            jobViewWindow.AddQt("蛊毒法", true);
            jobViewWindow.AddQt("枯骨法", true);
            jobViewWindow.AddQt("鼓舞", true);
            jobViewWindow.AddQt("跑快快", true);
            jobViewWindow.AddQt("扩散", true);
            jobViewWindow.AddQt("炽天召唤", false);
            jobViewWindow.AddQt("喝热水", true);
            jobViewWindow.AddQt("自动净化", true);
            jobViewWindow.AddHotkey("疾跑",new HotKeyResolver_NormalSpell(29057U,SpellTargetType.Self,false));
            jobViewWindow.AddHotkey("龟壳",new PVPHelper.龟壳());
            return true;
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

    public void OnLanguageChanged(LanguageType languageType)
    {
    }
}
