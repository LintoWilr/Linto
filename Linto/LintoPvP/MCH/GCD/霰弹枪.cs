using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Linto.LintoPvP.MCH;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH.GCD;

public class 霰弹枪 : MCHSlotResolverBase
{
    protected override string QtKey => "霰弹枪";
    protected override uint SkillId => 29404u;
    protected override int SkillRange => 12;
    protected override bool BlockInMarksmanPreAnim => false;
    protected override int? MaxGcdCooldownMs => 200;
}
