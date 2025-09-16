using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace Linto.Sage.GCD;

public class SGEGCDToxikon : ISlotResolver
{
    public Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Toxikon.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (Qt.GetQt("小停一下"))
        {
            return -1;
        }
        if (!Qt.GetQt("红豆")) return -2;
        if (Core.Resolve<JobApi_Sage>().Addersting <= 0) return -1;
        if (Qt.GetQt("强制红豆"))
        {
            return 1;
        }
        if (Core.Resolve<MemApiSpell>().GetGCDDuration()!=0)
        {
            return -3;
        }
        if (!Core.Me.IsMoving())
        {
            return -1;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}