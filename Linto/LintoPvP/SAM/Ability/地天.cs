
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM.Ability;

public class 地天 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public uint 技能地天 = 29533u;
    public int Check()//29533 必杀剑·地天
    {
        if (!PvPSAMOverlay.SAMQt.GetQt("地天"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        var 敌人数量 = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 10);
        if (敌人数量 < PvPSAMSettings.Instance.周围敌人数量)
        {
            return -3;
        }
        if (Core.Me.CurrentHpPercent() >= PvPSAMSettings.Instance.地天自身血量)
        {
            return -4;
        }
        if (!技能地天.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(PVPHelper.等服务器Spell(29533u, Core.Me));
    }
}
public class 残心 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check() //41577
    {
        if (!PvPSAMOverlay.SAMQt.GetQt("残心"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!Core.Me.InCombat())
        {
            return -8;
        }
        if (GCDHelper.GetGCDCooldown() < 300)
        {
            return -9;
        }
        if (PVPHelper.通用距离检查(8))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(41577, 8) == null)
        {
            return -6;
        }
        if (!Core.Me.HasAura(1318u))
        {
            return -2;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 41577, 8);
    }
}