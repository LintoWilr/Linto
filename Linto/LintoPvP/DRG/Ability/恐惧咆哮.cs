using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.DRG.Ability;

public class 恐惧咆哮 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public static bool 龙血()
    {
        return PvPDRGSettings.Instance.恐惧咆哮x龙血;
    }
    public int Check()//恐惧咆哮 29496
    {
        if (!DRGQt.GetQt("恐惧咆哮"))
        {
            return -1;
        }
        if (!PVPHelper.CanActive())
        {
            return -2;
        }
        if (龙血())
        {
            if (!Core.Me.HasAura(3177))
            {
                return -8;
            }
        }
        var 敌人数量 = TargetHelper.GetNearbyEnemyCount(Core.Me, 10, 10);
        if (敌人数量 < PvPDRGSettings.Instance.恐惧咆哮人数)
        {
            return -3;
        }
        if (!29496u.GetSpell().IsReadyWithCanCast())
        {
            return -5;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        {
            slot.Add(PVPHelper.等服务器Spell(DRGSkillID.恐惧咆哮, Core.Me));
        }
    }
}
