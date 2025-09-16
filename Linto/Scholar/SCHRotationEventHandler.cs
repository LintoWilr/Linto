
using CombatRoutine;
using Common;
using Common.Define;

namespace Linto.Scholar;

public class SCHRotationEventHandler : IRotationEventHandler
{
	public void OnResetBattle()
	{
		SCHBattleData.Instance.Reset();
		if (SCHSettings.Instance.AutoReset)
		{
			ScholarOverlay.SCHQt.Reset();
		}
	}

	public async Task OnPreCombat()
	{
		await Task.CompletedTask;
	}

	public Task OnNoTarget()
	{
		if (SCHBattleData.Instance.SpellQueueGCD.Count > 0)
		{
			if (SCHBattleData.Instance.SpellQueueGCD.Peek().CastTime.TotalSeconds > 0.0 && Core.Get<IMemApiMove>().IsMoving())
			{
				await Task.CompletedTask;
			}
			if (AI.Instance.BattleData.NextSlot != null)
			{
				AI.Instance.BattleData.NextSlot.Add(SCHBattleData.Instance.SpellQueueGCD.Peek());
			}
			else
			{
				AI.Instance.BattleData.NextSlot = new Slot(2500).Add(SCHBattleData.Instance.SpellQueueGCD.Peek());
			}
		}
		if (SCHBattleData.Instance.SpellQueueoGCD.Count > 0 && SCHBattleData.Instance.SpellQueueoGCD.Peek().Charges >= 1f)
		{
			if (AI.Instance.BattleData.NextSlot != null)
			{
				AI.Instance.BattleData.NextSlot.Add(SCHBattleData.Instance.SpellQueueoGCD.Peek());
			}
			else
			{
				AI.Instance.BattleData.NextSlot = new Slot(2500).Add(SCHBattleData.Instance.SpellQueueoGCD.Peek());
			}
		}
		await Task.CompletedTask;
	}

	public void AfterSpell(Slot slot, Spell spell)
	{
		if (SCHBattleData.Instance.SpellQueueoGCD.Count > 0 && spell == SCHBattleData.Instance.SpellQueueoGCD.Peek())
		{
			SCHBattleData.Instance.SpellQueueoGCD.Dequeue();
			AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
		}
		if (SCHBattleData.Instance.SpellQueueGCD.Count > 0 && spell == SCHBattleData.Instance.SpellQueueGCD.Peek())
		{
			SCHBattleData.Instance.SpellQueueGCD.Dequeue();
			AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
		}
	}

	public void OnBattleUpdate(int currTime)
	{
	}
}
