using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.GCD;

public class 祖灵之牙连击 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    private const uint 技能连击1祖灵之牙一式 = 39169u;
    private const uint 技能连击2祖灵之牙二式 = 39170u;
    private const uint 技能连击3祖灵之牙三式 = 39171u;
    private const uint 技能连击4祖灵之牙四式 = 39172u;
    private const uint 齿牙变化起点 = 39157u;
    private const uint 开大Buff = 4094u;
    private const uint LB技能 = 39190u;
    private const int 连击距离 = 5;
    private static int 技能距离 => 3 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
    private static bool 祖灵之牙已完成()
    {
        return PvPVPRBattleData.Instance.祖灵之牙已完成 || 技能连击4祖灵之牙四式.RecentlyUsed(5000);
    }
    private static uint 祖灵之牙当前技能()
    {
        var changedSkill = Core.Resolve<MemApiSpell>().CheckActionChange(齿牙变化起点);
        var isOuroStep = changedSkill is 技能连击1祖灵之牙一式 or 技能连击2祖灵之牙二式 or 技能连击3祖灵之牙三式 or 技能连击4祖灵之牙四式;
        var 祖灵牙已完成 = 祖灵之牙已完成();

        uint result = 0u;
        if (isOuroStep)
        {
            result = changedSkill;
        }
        else if (changedSkill != 39173u && !祖灵牙已完成)
        {
            result = 技能连击1祖灵之牙一式;
        }

        return result;
    }
    // public uint 技能满月 = 29527u;
    // public uint 技能樱花 = 29528u;
    // public uint 技能雪释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u);
    // public uint 技能月释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29524u);
    // public uint 技能花释放 = Core.Resolve<MemApiSpell>().CheckActionChange(29525u);
    // public bool 单体连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29523u;
    // public bool Aoe连 = Core.Resolve<MemApiSpell>().CheckActionChange(29523u) == 29526u;
    // public bool 雪连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29523u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29526u);
    // public bool 月连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29524u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29527u);
    // bool 花连击中 =(Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29525u||Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 29528u);
    // public uint 技能早天 = 29532u;
    public int Check()
    {
        if (!PvPVPROverlay.VPRQt.GetQt("连击"))
        {
            var currentTarget = Core.Me.GetCurrTarget();
            if (currentTarget != null)
            {
                TargetHelper.GetEnemyCountInsideRect(Core.Me, currentTarget, 5, 5);
            }
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (!Core.Me.HasLocalPlayerAura(开大Buff))//开大
        {
            return -3;
        }
        if (GCDHelper.GetGCDCooldown() > 1000)
        {
            return -4;
        }
        if (PVPHelper.通用距离检查(技能距离))
        {
            return -5;
        }
        if (LB技能.RecentlyUsed(800))
        {
            return -44;
        }
        if (祖灵之牙已完成())
        {
            return -5;
        }
        var changedSkill = 祖灵之牙当前技能();
        if (changedSkill == 0u)
        {
            return -5;
        }
        if (changedSkill is not (技能连击1祖灵之牙一式 or 技能连击2祖灵之牙二式 or 技能连击3祖灵之牙三式 or 技能连击4祖灵之牙四式))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(changedSkill, 连击距离) == null)
        {
            return -6;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 祖灵之牙当前技能(), 连击距离);
    }

}
