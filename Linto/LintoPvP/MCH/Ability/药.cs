using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH
{
    public class 药 : MCHSlotResolverBase
    {
        protected override string QtKey => "喝热水";
        protected override uint SkillId => 29711u;
        protected override bool UseSharedTargeting => false;
        protected override bool BlockInMarksmanPreAnim => false;

        protected override int CheckSpecific()
        {
            if (Core.Me.CurrentMp < 2500)
            {
                return -2;
            }
            if (Core.Me.CurrentHpPercent() <= PvPMCHSettings.Instance.药血量 / 100f)
            {
                return 0;
            }
            return -1;
        }
    }
}
