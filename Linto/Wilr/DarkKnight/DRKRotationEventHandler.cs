using CombatRoutine;
using Common.Define;

namespace Linto.Wilr.DarkKnight;

public class DRKRotationEventHandler : IRotationEventHandler
{
	public void OnResetBattle()
	{
	}

	public async Task OnPreCombat()
	{
		await Task.CompletedTask;
	}

	public Task OnNoTarget()
	{
		await Task.CompletedTask;
	}

	public void AfterSpell(Slot slot, Spell spell)
	{
		uint id = spell.Id;
		if (id == 16138)
		{
			AI.Instance.BattleData.LimitAbility = true;
		}
		else
		{
			AI.Instance.BattleData.LimitAbility = false;
		}
	}

	public void OnBattleUpdate(int currTime)
	{
		JobWindow.Instance.RunHotkey();
	}
}
