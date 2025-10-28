using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.GNB.Ability;

public class 净化 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;


    public int Check()
    {
        if (!PvPGNBOverlay.GNBQt.GetQt("自动净化"))
        {
            return -9;
        }
        if (!29056u.GetSpell().IsReadyWithCanCast() || Core.Me.CurrentMp < 2500)
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
        slot.maxDuration = 300;
        slot.Add(PVPHelper.等服务器Spell(29056u, Core.Me));
    }
}