using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Linto.Sage;

public class OpenerSGE80tea : IOpener
{
    public int StartCheck()
    {
        if (PartyHelper.Party.Count <= 4 && !Core.Me.GetCurrTarget().IsDummy())
            return -100;
        return 0;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0
    };

    public Action CompeltedAction { get; set; }

    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Eukrasia, SpellTargetType.Self));
        slot.Add(new Spell(SpellsDefine.EukrasianDosis, SpellTargetType.Target));
    }

    public uint Level { get; } = 80;

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (Qt.GetQt("爆发药")) 
            countDownHandler.AddPotionAction(3000);
        countDownHandler.AddAction(20000, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Physis.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(15000, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Eukrasia.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(10500, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.EukrasianPrognosis.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(SGESettings.Instance.time, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Dosis.GetSpell().Id), SpellTargetType.Target);
    }
}