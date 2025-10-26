using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM.Ability;

public class 热震荡 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    List<int> list = new List<int> { 3216 };
    public int Check()
    {
        if (!PvPBLMOverlay.BLMQt.GetQt("热震荡"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (SpellHelper.GetSpell(29661u).Charges < 1)
        {
            return -3;
        }
        if (TargetHelper.GetNearbyEnemyCount(Core.Me, 30, 30) < 1)
        {
            return -2;
        }
        // if(PvPBLMOverlay.BLMQt.GetQt("冰霜女巫"))
        // {
        // 	if(PVPTargetHelper.Check目标免控(PVPTargetHelper.目标模式(30)))
        // 	{
        // 		return -5;
        // 	}
        // }
        if (Core.Resolve<MemApiBuff>().BuffStackCount(PVPTargetHelper.目标模式(30, 29661u), 3216u) == 3
           || Core.Resolve<MemApiBuff>().BuffStackCount(PVPTargetHelper.目标模式(30, 29661u), 3217u) == 3)
        {
            return 0;
        }
        return -3;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(29661u, Core.Me));
    }
}
