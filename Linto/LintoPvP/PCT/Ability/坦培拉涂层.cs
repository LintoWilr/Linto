using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT.Ability;

public class 坦培拉涂层 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public int Check()
    {
        if (!PvPPCTOverlay.PCTQt.GetQt("坦培拉涂层"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (Core.Me.CurrentHpPercent() > PvPPCTSettings.Instance.盾自身血量)
        {
            return -4;
        }
        if (!PCTSkillID.技能坦培拉涂层.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(PVPHelper.等服务器Spell(PCTSkillID.技能坦培拉涂层, Core.Me));
    }
}
