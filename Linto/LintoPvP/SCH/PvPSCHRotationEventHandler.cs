using AEAssist.MemoryApi;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using ECommons.DalamudServices;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SCH;

public class PvPSCHRotationEventHandler : IRotationEventHandler
{
	public void OnResetBattle()
	{
		PvPBLMBattleData.Instance.Reset();
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
	}
	public void OnEnterRotation()
	{
		Share.Pull = true;
		if(!PVPHelper.通用权限())
		{
			Core.Get<IMemApiChatMessage>().Toast("未获得权限，无法使用！\n");
		}
		
	}
	public void OnExitRotation()
	{
		Share.Pull = false;
	}
}
