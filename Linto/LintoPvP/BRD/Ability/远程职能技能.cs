using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BRD.Ability;

public class 速度之星 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public uint 速度之星u = 43249;
    public uint 速度之星buff = 4489u;
    public int Check()
    {
        if (!PvPBRDOverlay.BRDQt.GetQt("职能技能"))
        {
            return -9;
        }
        if (!Core.Me.HasAura(速度之星buff))
        {
            return -2;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!速度之星u.GetSpell().IsReadyWithCanCast())
        {
            return -1;
        }
        if (PVPHelper.check坐骑())
        {
            return -5;
        }
        return 0;
    }
    public void Build(Slot slot)
    {
        slot.Add(new Spell(速度之星u, Core.Me));
    }
}
public class 勇气 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public uint 勇气u = 43250;
    public uint 勇气释放buff = 4490u;
    public uint 勇气buff = 4479u;
    public uint 钻头变化()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(29408u);
    }
    public int Check()
    {
        if (!PvPBRDOverlay.BRDQt.GetQt("职能技能"))
        {
            return -9;
        }
        if (!Core.Me.HasAura(勇气释放buff))
        {
            return -2;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (!勇气u.GetSpell().IsReadyWithCanCast())
        {
            return -1;
        }
        if (PVPHelper.check坐骑())
        {
            return -5;
        }
        if (PVPHelper.check坐骑())
        {
            return -5;
        }
        return 0;
    }
    public void Build(Slot slot)
    {
        slot.Add(new Spell(勇气u, Core.Me));
    }
}