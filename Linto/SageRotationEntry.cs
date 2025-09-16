using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Helper;
using Linto.Sage;
using Linto.Sage.Ability;
using Linto.Sage.GCD;
using Linto.Sage.Triggers;

namespace Linto;

public class SageRotationEntry : IRotationEntry
    {
    public void Dispose()
    {
    }
    public string OverlayTitle { get; } = "贤者";
    public string AuthorName { get; set; } = "Linto";
    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new AutoEsunatea(),SlotMode.Always),
       // new SGEGCDInsGCD(),
       new( new SGEGCDRemake(),SlotMode.Always),
       new( new AutoEsuna(),SlotMode.Gcd),
       new(new Esuna(),SlotMode.Gcd),
       new( new SGEAbility_AutoHeal(),SlotMode.Always),
       new( new SGEAbility_AutoHealSingle(),SlotMode.Always),
       new(new SGEGCDPhlegma(),SlotMode.Gcd),
       new( new SGEGCDDot(),SlotMode.Gcd),
       new( new SGEGCDToxikon(),SlotMode.Gcd),
       new(new SGEGCDBaseGCD(),SlotMode.Gcd),
       new( new SGEAbility_LucidDreaming(),SlotMode.OffGcd),
       new( new SGEAbility_心神风息(),SlotMode.OffGcd),
       new( new SGEAbility_Kardia2(),SlotMode.OffGcd),
       new( new SGEAbility_Kardia(),SlotMode.OffGcd),
    };
    public Rotation Build(string settingFolder)
    {
        SGESettings.Build(settingFolder);
        BuildQT();
        var meow = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Sage,
            AcrType = AcrType.HighEnd,
            MinLevel = 70,
            MaxLevel = 100,
            Description = "2.0移植 高难贤者 绝亚适配 自用目的 缝了点残光的",
        };
        //meow.AddSlotSequences(new );
        meow.AddTriggerAction(new SGE_HotKey(), new SGE_Qt());
        meow.AddTriggerCondition(new 贤者时间轴蓝豆状态(), new 贤者时间轴红豆状态());
        meow.SetRotationEventHandler(new SGERotationEventHandler());
        meow.AddOpener(GetOpener);
        return meow;
    }
    private IOpener opener90 = new OpenerSGE90();
    private IOpener opener80 = new OpenerSGE80();
    private IOpener opener80tea = new OpenerSGE80tea();
    private IOpener opener90pne = new OpenerSGE90Pneuma();
    private IOpener opener70 = new OpenerSGE70();
    private IOpener? GetOpener(uint level)
    {
        if (level < 90)
        {
            if (SGESettings.Instance.opener == 2)
            {
                return opener80tea;
            }
            if (SGESettings.Instance.opener == 3)
            {
                return opener70;
            }
            return opener80;
        }
        else
        {
            if (SGESettings.Instance.opener == 0)
            {
                return opener90;
            }
            if(SGESettings.Instance.opener == 1)
            {
                return opener90pne;
            }
        }
        return null;
    }
    public static JobViewWindow JobViewWindow { get; private set; }
    public IRotationUI GetRotationUI()
    {
        return SageRotationEntry.JobViewWindow;
    }
    private SageUISetting settingUI = new();
    public void OnDrawSetting()
    {
       settingUI.Draw();
    }
    public void BuildQT()
    {
        JobViewWindow = new JobViewWindow(SGESettings.Instance.JobViewSave, SGESettings.Instance.Save, OverlayTitle);
        //JobViewWindow.AddTab("通用", PvPPCTOverlay.);
        //贤者ACR入口.JobViewWindow.AddTab("日志", _lazyOverlay.更新日志);
        //贤者ACR入口.JobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        JobViewWindow.AddTab("通用", SageOverlay.DrawGeneral);
        //JobViewWindow.AddTab("解锁", PvPPCTOverlay.DrawDev);
        JobViewWindow.AddQt("爆发药", true);
        JobViewWindow.AddQt("AOE", true);
        JobViewWindow.AddQt("DOT", true);
        JobViewWindow.AddQt("发炎", true,"移动时和CD满优先打发炎");
        JobViewWindow.AddQt("强制发炎", false,"强制使用发炎");
        JobViewWindow.AddQt("红豆", true,"移动时优先打红豆");
        JobViewWindow.AddQt("强制红豆", false,"强制使用红豆");
        JobViewWindow.AddQt("心神风息", true,"");
        JobViewWindow.AddQt("自动心关", true,"默认给当前一仇目标");
        JobViewWindow.AddQt("小停一下", false,"停止所有攻击性技能的使用");
        JobViewWindow.AddQt("拉人", true,"有即刻的情况下拉人");
        JobViewWindow.AddHotkey("单盾最低血量T", new SageTargetHelper.单盾());
        JobViewWindow.AddHotkey("单盾最低血量队友", new SageTargetHelper.单盾最低血量());
        JobViewWindow.AddHotkey("群奶", (IHotkeyResolver)new HotKeyResolver_NormalSpell(24286u, (SpellTargetType)1, false));
        JobViewWindow.AddHotkey("群盾",new SageTargetHelper.群盾());
        JobViewWindow.AddHotkey("群盾接消化", (IHotkeyResolver)(object)new SageTargetHelper.群盾消化());
        JobViewWindow.AddHotkey("整体论",new HotKeyResolver_NormalSpell(24310U,SpellTargetType.Self,false));
        JobViewWindow.AddHotkey("胖海马",new HotKeyResolver_NormalSpell(24311U,SpellTargetType.Self,false));
        JobViewWindow.AddHotkey("贤炮",new HotKeyResolver_NormalSpell(24318U,SpellTargetType.Target,false));
		JobViewWindow.AddHotkey("LB", (IHotkeyResolver)new HotKeyResolver_NormalSpell(24859u, (SpellTargetType)1, false));
		JobViewWindow.AddHotkey("防击退", (IHotkeyResolver)new HotKeyResolver_NormalSpell(7559u, (SpellTargetType)1, false));
        //JobViewWindow.AddHotkey("LB",new PVPHelper.画家LB());
    }
}
        //if (ImGui.CollapsingHeader("插入技能状态"))
        //{
        //    if (ImGui.Button("清除队列"))
        //    {
        //        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
        //        AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
        //    }

        //    ImGui.PCTeLine();
        //    if (ImGui.Button("清除一个"))
        //    {
        //        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
        //        AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
        //    }

        //    ImGui.Text("-------能力技-------");
        //    if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
        //        foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
        //            ImGui.Text(spell.Name);
        //    ImGui.Text("-------GCD-------");
        //    if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        //        foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_GCD)
        //            ImGui.Text(spell.Name);
        //}