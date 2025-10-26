
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.Ability;

public class 莫古力炮 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public uint 莫古力炮变化()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能莫古力激流);
    }
    public int Check()
    {
        if (!PvPPCTOverlay.PCTQt.GetQt("莫古力炮"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(莫古力炮变化(), 25) == null)
        {
            return -6;
        }
        if (Core.Me.HasLocalPlayerAura(PCTSkillID.buff莫古力标识) || Core.Me.HasLocalPlayerAura(PCTSkillID.buff马蒂恩标识))
        {
            return 1;
        }
        return -2;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 莫古力炮变化(), 25);
    }
}
