using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 全金属爆发 : MCHSlotResolverBase
{
    protected override string QtKey => "全金属爆发";
    protected override uint SkillId => 41469u;
    protected override int SkillRange => 25;
    protected override int? MaxGcdCooldownMs => 200;

    protected override int CheckSpecific()
    {
        if (PvPMCHSettings.Instance.金属爆发仅野火)
        {
            if (PVPTargetHelper.TargetSelector.Get野火目标() == null)
            {
                return -9;
            }
        }

        return 0;
    }
}
