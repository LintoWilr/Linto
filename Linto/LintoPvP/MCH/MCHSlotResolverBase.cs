using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MCH;

public abstract class MCHSlotResolverBase : ISlotResolver
{
    public virtual SlotMode SlotMode { get; } = SlotMode.Always;

    protected abstract string QtKey { get; }

    protected abstract uint SkillId { get; }

    protected virtual uint CheckSkillId => SkillId;

    protected virtual uint BuildSkillId => SkillId;

    protected virtual int SkillRange => 25;

    protected virtual bool BlockInMarksmanPreAnim => true;

    protected virtual bool UseSharedTargeting => true;

    protected virtual bool RequireReady => true;

    protected virtual uint? RequiredAura => null;

    protected virtual int RequiredAuraFailCode => -9;

    protected virtual int? MaxGcdCooldownMs => null;

    protected virtual int CheckSpecific() => 0;

    protected virtual void BuildSpecific(Slot slot)
    {
        slot.Add(new Spell(BuildSkillId, Core.Me));
    }

    public int Check()
    {
        if (!PVPHelper.MCH.IsQtEnabled(QtKey))
        {
            return -9;
        }

        if (!PVPHelper.CanActive())
        {
            return -1;
        }

        if (BlockInMarksmanPreAnim && PVPHelper.MCH.IsMarksmanPreAnim())
        {
            return -2;
        }

        if (RequiredAura.HasValue && !Core.Me.HasAura(RequiredAura.Value))
        {
            return RequiredAuraFailCode;
        }

        if (MaxGcdCooldownMs.HasValue && !PVPHelper.MCH.IsGcdReady(MaxGcdCooldownMs.Value))
        {
            return -3;
        }

        if (UseSharedTargeting && !PVPHelper.MCH.CanUseTargetedSkill(CheckSkillId, SkillRange))
        {
            return -6;
        }

        if (RequireReady && !PVPHelper.MCH.IsSkillReady(CheckSkillId))
        {
            return -2;
        }

        return CheckSpecific();
    }

    public virtual void Build(Slot slot)
    {
        if (UseSharedTargeting)
        {
            PVPHelper.MCH.BuildSharedSpell(BuildSkillId, SkillRange, slot);
            return;
        }

        BuildSpecific(slot);
    }
}
