using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 净化 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public uint 技能净化 = 29056;
    public int Check()
    {
        if (!PvPMCHOverlay.MCHQt.GetQt("自动净化"))
        {
            return -9;
        }
        if (!技能净化.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        if (PVPHelper.净化判断())
        {
            return 1;
        }
        return -3;
    }
    public void Build(Slot slot)
    {
        slot.Add(PVPHelper.等服务器Spell(29056u, Core.Me));
    }
}