using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace Linto.Sage;

public class OpenerSGE90Pneuma : IOpener
{
    public int StartCheck()
    {
        /*if (!SpellsDefine.Pneuma.GetSpell().IsReadyWithCanCast()&&SpellsDefine.Pneuma.GetSpell().Cooldown.TotalSeconds>10)
        {
            return -1;
        }*/
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
        Step0,
        Step0,
        Step0_1,
        Step1,
    };

    public Action CompeltedAction { get; set; }
    private static void Step0(Slot slot)
    {
        slot.Add(SpellsDefine.Kardia.GetSpell());
        slot.Add(SpellsDefine.ToxikonIi.GetSpell());
    }
    private static void Step0_1(Slot slot)
    {
        slot.Add(SpellsDefine.ToxikonIi.GetSpell());
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());
    }

    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.EukrasianDosisIii.GetSpell().Id), SpellTargetType.Target));
    }

    public uint Level { get; } = 90;

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(13500, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Eukrasia.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(12500, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.EukrasianDiagnosis.GetSpell().Id), SpellTargetType.Pm7);
        countDownHandler.AddAction(10000, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Eukrasia.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(9000, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.EukrasianDiagnosis.GetSpell().Id), SpellTargetType.Pm8);
        countDownHandler.AddAction(6500, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Eukrasia.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(5500, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.EukrasianDiagnosis.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(3000, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Eukrasia.GetSpell().Id), SpellTargetType.Self);
        countDownHandler.AddAction(SGESettings.Instance.time, Core.Resolve<MemApiSpell>().CheckActionChange(SpellsDefine.Pneuma.GetSpell().Id), SpellTargetType.Target);
    }
}