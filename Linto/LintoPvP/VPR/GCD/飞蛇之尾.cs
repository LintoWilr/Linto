using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.GCD;

public class 飞蛇之尾 : ISlotResolver
{
    public SlotMode SlotMode { get; }
    public uint 技能飞蛇之尾 = 39168u;
    private int 技能距离 => 18 + SettingMgr.GetSetting<GeneralSettings>().AttackRange;
    public int Check()
    {
        if (!PvPVPROverlay.VPRQt.GetQt("飞蛇之尾"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (GCDHelper.GetGCDCooldown() > 600)
        {
            return -9;
        }
        if (PVPHelper.通用距离检查(技能距离))
        {
            return -5;
        }
        if (!技能飞蛇之尾.GetSpell().IsReadyWithCanCast())
        {
            return -10;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 技能飞蛇之尾, 20);
    }
}
