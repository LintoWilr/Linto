
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR.Ability;

public class 飞蛇之魂 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    private const uint 技能飞蛇之魂 = 39189u;
    private const uint 技能飞蛇之尾 = 39168u;
    private const uint 开大Buff = 4094u;
    public int Check()
    {
        if (!PvPVPROverlay.VPRQt.GetQt("飞蛇之魂"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (Core.Me.HasLocalPlayerAura(开大Buff))
        {
            return -5;
        }
        if (Core.Resolve<MemApiSpell>().GetCharges(技能飞蛇之尾) > 1f)
        {
            return -6;
        }
        if (!技能飞蛇之魂.GetSpell().IsReadyWithCanCast())
        {
            return -3;
        }
        if (技能飞蛇之尾.GetSpell().IsReadyWithCanCast())
        {
            return -4;
        }
        return 1;
    }

    public void Build(Slot slot) => slot.Add(PVPHelper.等服务器Spell(技能飞蛇之魂, Core.Me));
}
