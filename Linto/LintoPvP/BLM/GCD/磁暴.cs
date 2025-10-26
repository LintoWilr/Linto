using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.GCD;

public class 磁暴 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("磁暴"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (GCDHelper.GetGCDCooldown() > 600)
        {
            return -3;
        }
        if (!29657u.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        TargetHelper.GetNearbyEnemyCount(Core.Me, 50, 50);
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 1, PvPBLMSettings.Instance.磁暴敌人距离);
        if (aoeCount >= PvPBLMSettings.Instance.磁暴敌人数量)
        {
            return 1;
        }
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(29657, SpellTargetType.Self));
    }
}
