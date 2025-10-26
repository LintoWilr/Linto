
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.RDM.Ability;

public class 决断 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public int Check()
    {
        if (!PvPRDMOverlay.RDMQt.GetQt("决断")) return -1;
        if (!PVPHelper.CanActive()) return -2;
        if (PvPRDMSettings.Instance.鼓励决断 && !Core.Me.HasLocalPlayerAura(2282U)) return -3;
        if (GCDHelper.GetGCDCooldown() > 0) return -5;
        if (!41492u.GetSpell().IsReadyWithCanCast()) return -6;
        if (PVPHelper.通用距离检查(25)) return -5;
        if (PVPHelper.通用技能释放Check(41492U, 25) == null) return -6;
        return 0;
    }
    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 41492U, 25);
    }
}
