using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.GCD;

public class 命运之环 : ISlotResolver
{
    public SlotMode SlotMode { get; }

    public int Check()//41511 命运之环
    {
        if (!PvPGNBOverlay.GNBQt.GetQt("命运之环"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (PVPHelper.固定距离检查(6))
        {
            return -3;
        }
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(41511).GetSpell().IsReadyWithCanCast())
        {
            return -4;
        }
        if (PVPHelper.通用技能释放Check(41511u, 6) == null)
        {
            return -5;
        }

        var ChitenEnemies = PVPTargetHelper.获取自身周围6码内地天状态敌对玩家();
        if (ChitenEnemies.Count > 0)
        {
            return -6;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(41511u, SpellTargetType.Self));
    }
}
