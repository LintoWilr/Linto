using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;


namespace Linto.Sage.Ability;

public class SGEAbility_AutoHealSingle : ISlotResolver
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
        slot.Add(new Spell(Getspell().Id, PartyHelper.CastableTanks.FirstOrDefault()));
    }

    Spell Getspell()
    {
        var MT = PartyHelper.CastableTanks.FirstOrDefault().CurrentHpPercent();
        if (SpellsDefine.Haima.GetSpell().IsReadyWithCanCast())
        {
            if (MT < 0.8f)
            {
                return SpellsDefine.Haima.GetSpell();
            }
        }
        if (SpellsDefine.Taurochole.GetSpell().IsReadyWithCanCast()&& Core.Resolve<JobApi_Sage>().Addersgall>0)
        {
            if (MT < 0.7f)
            {
                return SpellsDefine.Taurochole.GetSpell();
            }
        }
        if (SpellsDefine.Soteria.GetSpell().IsReadyWithCanCast())
        {
            if (MT < 0.9f)
            {
                return SpellsDefine.Soteria.GetSpell();
            }
        }
        if (SpellsDefine.Druochole.GetSpell().IsReadyWithCanCast()&& Core.Resolve<JobApi_Sage>().Addersgall>0)
        {
            if (MT < 0.3f)
            {
                return SpellsDefine.Druochole.GetSpell();
            }
        }
        return SpellsDefine.Kardia.GetSpell();
    }
}