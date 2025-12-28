using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.Ability;

public class 分析 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public int Check()
    {
        if (!PvPMCHOverlay.MCHQt.GetQt("分析"))
        {
            return -9;
        }
        if (!PVPHelper.CanActive())
        {
            return -1;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -5;
        }
        if (PVPHelper.通用技能释放Check(29414, 25) == null)
        {
            return -6;
        }
        if (SpellHelper.GetSpell(29414u).Charges < 1)
        {
            return -1;
        }
        if (PvPMCHSettings.Instance.分析可用)
        {
            if (!29405u.GetSpell().IsReadyWithCanCast())
            {
                return -8;
            }
        }
        if (Core.Me.HasAura(3158))
        {
            return -99;
        }
        if (Core.Resolve<MemApiSpellCastSuccess>().LastSpell == 29414U)
        {
            return -99;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(PVPHelper.等服务器Spell(29414U, Core.Me));
    }
}
