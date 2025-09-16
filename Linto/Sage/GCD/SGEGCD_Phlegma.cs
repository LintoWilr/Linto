using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;


namespace Linto.Sage.GCD;

public class SGEGCDPhlegma : ISlotResolver
{
    public Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Phlegma.GetSpell().Id).GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Always;

    public int Check()
    {
        if (Qt.GetQt("小停一下"))
            return -1;
        if (!Qt.GetQt("发炎")) 
            return -2;
        if (Core.Me.GetCurrTarget().DistanceToPlayer() > 4+SettingMgr.GetSetting<GeneralSettings>().AttackRange) 
            return -3;
        if (GetSpell().Charges < 1) 
            return -4;
        if (Qt.GetQt("强制发炎"))
            return 4;
        if (GetSpell().Charges > 1.9)
            return 2;
        if (Core.Resolve<MemApiSpell>().GetGCDDuration()!=0)
            return -6;
        if (Core.Me.IsMoving())
            if (GetSpell().Charges > 1)
                return 1;
        if (GetSpell().Charges < 1.5) return -1;

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