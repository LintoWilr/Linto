using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Linto.Sage.GCD;

public class SGEGCDBaseGCD : ISlotResolver
{
    public Spell GetSpell()
    {
        if (Qt.GetQt("AOE"))
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
            //return SpellsDefine.Holy.GetSpell();
            if (aoeCount >=2)
                return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell();
        }
        if (Core.Me.IsMoving()&&SGESettings.Instance.useDyskrasia&&Core.Resolve<MemApiSpell>().GetGCDDuration()==0)
        {
            return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell();
        }
        return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dosis.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (Qt.GetQt("小停一下"))
        {
            return -1;
        }
        if (GetSpell() ==
            Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dyskrasia.GetSpell().Id).GetSpell()) return 0;
        if (Core.Resolve<MemApiMove>().IsMoving()&&!Core.Me.HasMyAuraWithTimeleft(AurasDefine.Swiftcast))
        {
            if (SGESettings.Instance.useDyskrasia)
            {
                return 0;
            }
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