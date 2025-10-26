using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.Ability;

public class 蛇续剑 : ISlotResolver
{
    public uint 蛇续剑id()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(39183u);
    }

    public SlotMode SlotMode { get; } = SlotMode.Always;
    private int 技能距离 => 2 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
    public int Check()
    {
        if (!PvPVPROverlay.VPRQt.GetQt("蛇续剑"))
        {
            return -9;
        }
        if (蛇续剑id() == 39183)
        {
            return -2;
        }
        if (!PVPHelper.CanActive())
        {
            return -3;
        }

        if (蛇续剑id() != 39177u && PVPHelper.通用距离检查(技能距离))
        {
            return -5;
        }
        if (蛇续剑id() == 39177u && PVPHelper.通用距离检查(20))
        {
            return -5;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 蛇续剑id(), 技能距离);
    }
}
