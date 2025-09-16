

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;

namespace Linto.Sage.Ability;

public class SGEAbility_AutoHeal : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!SGESettings.Instance.AutoHeal)
        {
            return -100;
        }
        if (Getspell()==SpellsDefine.Kardia.GetSpell())
        {
            return -5;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Getspell());
    }

    Spell Getspell()
    {
        var count = PartyHelper.CastableParty.Count(agent => agent.CurrentHpPercent() < 0.8f);
        if (SpellsDefine.Ixochole.GetSpell().IsReadyWithCanCast()&& Core.Resolve<JobApi_Sage>().Addersgall>0)
        {
            if (count > 3)
            {
                return SpellsDefine.Ixochole.GetSpell();
            }
        }
        if (SpellsDefine.Panhaima.GetSpell().IsReadyWithCanCast())
        {
            if (count > 2)
            {
                return SpellsDefine.Panhaima.GetSpell();
            }
        }
        if (SpellsDefine.PhysisIi.GetSpell().IsReadyWithCanCast())
        {
            if (count > 2)
            {
                return SpellsDefine.PhysisIi.GetSpell();
            }
        }
        if (SpellsDefine.Kerachole.GetSpell().IsReadyWithCanCast()&& Core.Resolve<JobApi_Sage>().Addersgall>0)
        {
            if (count > 1)
            {
                return SpellsDefine.Kerachole.GetSpell();
            }
        }

        if (!Core.Me.GetCurrTarget().IsTargetable&&Core.Me.GetCurrTarget().IsCasting)
        {
            
        }
        
        return SpellsDefine.Kardia.GetSpell();
        
    }
}