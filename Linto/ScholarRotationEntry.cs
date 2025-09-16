using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.TriggerModel;
using CombatRoutine.View;
using CombatRoutine.View.JobView;
using Common.Define;
using Common.Language;
using Linto.Scholar;
using Linto.Scholar.Scholar.Ability;
using Linto.Scholar.Scholar.GCD;

namespace Linto;

public class SchRotationEntry : IRotationEntry
{
    public static JobViewWindow JobViewWindow;
    private ScholarOverlay _lazyOverlay =  new ScholarOverlay();
    public List<ISlotResolver> SlotResolvers = new ()
    {
    };

    public string OverlayTitle { get; } = "尼姆海军";
    
    public void DrawOverlay()
    {
    }
    public string AuthorName { get; } = "Linto";
    public Jobs TargetJob { get; } = Jobs.Scholar;
    public AcrType AcrType { get; } = AcrType.HighEnd;
    
    public Rotation Build(string settingFolder)
    {
        SCHSettings.Build(settingFolder);
        return new Rotation(this, () => SlotResolvers)
            .SetRotationEventHandler(new SCHRotationEventHandler())
            .AddSlotSequences();
    }
    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
            jobViewWindow = new JobViewWindow(SCHSettings.Instance.JobViewSave, SCHSettings.Instance.Save, OverlayTitle);
            JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
            jobViewWindow.AddTab("设置", _lazyOverlay.DrawGeneral);
            jobViewWindow.AddQt("斩铁剑", true,"对能斩掉的敌人斩铁");
            jobViewWindow.AddQt("地天", true,"人群中开地天");
            jobViewWindow.AddQt("连击", true);
            jobViewWindow.AddQt("斩浪", true,"波切");
            jobViewWindow.AddQt("明镜", true);
            jobViewWindow.AddQt("AOE", true);
            jobViewWindow.AddQt("雪月花", true);
            jobViewWindow.AddQt("刀背击打", true);
            jobViewWindow.AddQt("喝热水", true);
            jobViewWindow.AddQt("自动净化", true);
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
