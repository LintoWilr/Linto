using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

namespace Linto.Sage.GCD;

public class AutoEsuna: ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (Core.Me.IsMoving())
        {
            return -1;
        }
        if (SGESettings.Instance.康复tea)
        {
            return -100;
        }
        if (!SGESettings.Instance.AutoHeal)
        {
            return -100;
        }
        if (PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasCanDispel()))
        {
            return -2;
        }

        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Esuna,PartyHelper.CastableAlliesWithin30.FirstOrDefault(agent => agent.HasCanDispel())));
    }
    
}

public class AutoEsunatea: ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    
    public int Check()
    {
        if (!SGESettings.Instance.康复tea)
        {
            return -100;
        }
        if (PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasCanDispel()))
        {
            return 1;
        }
        return -1;
    }

    public void Build(Slot slot)
    {
        if (SGESettings.Instance.H1)
        {
            slot.Add(new Spell(SpellsDefine.Esuna,SageTargetHelper.GetH1康复目标()));
        }
        else
        {
            slot.Add(new Spell(SpellsDefine.Esuna,SageTargetHelper.GetH2康复目标()));
        }
    }
    
}