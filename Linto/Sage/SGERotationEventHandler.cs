using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

namespace Linto.Sage;

public class SGERotationEventHandler : IRotationEventHandler
{
    public void OnTerritoryChanged()
    {

    }
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {

    }
    public void OnResetBattle()
    {
        SGEBattleData.Instance.Reset();
        Qt.Reset();
    }
    public void OnEnterRotation()
    {
    }
    public void OnExitRotation()
    {
    }
    public async Task OnPreCombat()
    {
        await Task.CompletedTask;
    }

    public async Task OnNoTarget()
    {
        /*if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        {
            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Peek().CastTime.TotalSeconds > 0)
                if (Core.Me.IsMoving())
                    return;
            if (AI.Instance.BattleData.NextSlot != null)
                AI.Instance.BattleData.NextSlot.Add(AI.Instance.BattleData.HighPrioritySlots_GCD.Peek());
            else
                AI.Instance.BattleData.NextSlot = new Slot().Add(AI.Instance.BattleData.HighPrioritySlots_GCD.Peek());
        }

        if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek().Charges >= 1)
            {
                if (AI.Instance.BattleData.NextSlot != null)
                    AI.Instance.BattleData.NextSlot.Add(AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek());
                else
                    AI.Instance.BattleData.NextSlot =
                        new Slot().Add(AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek());
            }
        /*if (SGESettings.Instance.AutoHeal)
        {
            if (!PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607)).IsNull())
            {
                var slot = new Slot();
                if (!Core.Get<IMemApiSage>().Eukrasia())
                {
                    slot.Add(SpellsDefine.Eukrasia.GetSpell());
                }
                slot.Add(new Spell(SpellsDefine.EukrasianDiagnosis, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasMyAura(2607))));
                await slot.Run(false);
            }
        }#1#
        return;*/
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        // if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
        //     if (spell == AI.Instance.BattleData.HighPrioritySlots_OffGCD.Peek())
        //         AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
        // if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
        //     if (spell == AI.Instance.BattleData.HighPrioritySlots_GCD.Peek())
        //         AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
        // switch (spell.Id)
        // {
        // }
        // switch (spell.Id)
        // {
        //     case SpellsDefine.Eukrasia:
        //         AI.Instance.BattleData.LimitAbility = true;
        //         break;
        //     default:
        //         AI.Instance.BattleData.LimitAbility = false;
        //         break;
        // }
    }

    public void OnBattleUpdate(int currTime)
    {
    }
}