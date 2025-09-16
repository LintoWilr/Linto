using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;


namespace Linto.Sage.GCD;

public class SGEGCDDot : ISlotResolver
{
    public Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dosis.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (!Qt.GetQt("DOT")) return -2;
        if (Qt.GetQt("小停一下"))
        {
            return -1;
        }
        if (DotBlacklistHelper.IsBlackList(Core.Me.GetCurrTarget()))
            return -10;
        if (Core.Me.HasAura(AurasDefine.Medicated))
        {
            if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 6000))
            {
                return -1;
            }
            return 2;
        }
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosis, 3000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIi, 3000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(AurasDefine.EukrasianDosisIii, 3000))
            return -1;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        if (!Core.Resolve<JobApi_Sage>().Eukrasia) 
            slot.Add(SpellsDefine.Eukrasia.GetSpell());
        slot.Add(spell);
        //slot.AppendSequence();
    }
}