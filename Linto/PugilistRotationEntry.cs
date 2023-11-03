using Linto.Monk;
using Linto.Monk.Ability;
using Linto.Monk.GCD;
using Linto.Monk.Triggers;
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common.Define;
using Common.Language;

namespace Linto;

public class PugilistRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    private MNKOverlay _lazyOverlay = new MNKOverlay();
  //  private IOpener opener90 = new OpenerWAR90();

    public List<ISlotResolver> SlotResolvers = new()
    {
        new MNKGCDMasterfulBlitz(),
        new MNKGCD_PerfectBalance(),
        new MNKGCD_BaseGCD(),
        new MNKGCDMeditation(),
        new MNKAbility_RiddleofFire(),
        new MNKAbility_Brotherhood(),
        new MNKAbility_PerfectBalance(),
        new MNKAbility_RiddleofWind(),
        new MNKAbility_TheforbiddenChakra(),
        new MNKAbility_HowlingFist(),
        new MNKAbility_BloodBath(),
        new MNKAbility_SecondWind()
    };

    public string OverlayTitle { get; } = "Linto 日随专用格斗家";

    public void DrawOverlay()
    {
    }

    public string AuthorName { get; } = "Linto";
    public Jobs TargetJob { get; } = Jobs.Pugilist;

    public Rotation Build(string settingFolder)
    {
        MNKSettings.Build(settingFolder);
        return new Rotation(this, () => SlotResolvers)
            .AddOpener(GetOpener)
            .SetRotationEventHandler(new MNKRotationEventHandler())
            .AddSettingUIs(new DPSMNKSettingView())
            .AddSlotSequences()
            .AddTriggerAction(new LintoMNKQt());
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(MNKSettings.Instance.JobViewSave, MNKSettings.Instance.Save, OverlayTitle);
        JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
      //  jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("额外", _lazyOverlay.DrawDev);
      //  jobViewWindow.AddQt("吃爆发药", false);
        jobViewWindow.AddQt("爆发", true);
        jobViewWindow.AddQt("疾风极意", true);
        jobViewWindow.AddQt("AOE", true);
        jobViewWindow.AddQt("自动浴血", true);
        jobViewWindow.AddQt("自动内丹", true);
        jobViewWindow.AddQt("自动真言", true);

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