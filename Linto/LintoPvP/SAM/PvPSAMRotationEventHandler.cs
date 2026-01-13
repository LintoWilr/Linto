using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.SAM;

public class PvPSAMRotationEventHandler : IRotationEventHandler
{
	public void OnTerritoryChanged()
	{

	}
	public void OnSpellCastSuccess(Slot slot, Spell spell)
	{

	}
	public void OnResetBattle()
	{
		PvPSAMBattleData.Instance.Reset();
	}
	public async Task OnPreCombat()
	{
		PVPTargetHelper.自动选中();
		if (PvPSettings.Instance.无目标坐骑)
		{
			MountHandler.无目标坐骑();
		}
		await Task.CompletedTask;
	}

	
	public async Task OnNoTarget()
	{
		var slot = new Slot();
		PVPTargetHelper.自动选中();
		if (PvPSettings.Instance.无目标坐骑)
		{
			MountHandler.无目标坐骑();
		}
		await Task.CompletedTask;
	}

	public void AfterSpell(Slot slot, Spell spell)
	{
		uint id = spell.Id;
		if (id == 29537u&&PvPSettings.Instance.播报)
		{
			Core.Resolve<MemApiChatMessage>().Toast2($"{Core.Me.Name}对 {PVPTargetHelper.TargetSelector.Get斩铁目标().Name} 使用了 斩铁剑！", 1, 1500);
		}
	}
	public void OnBattleUpdate(int currTime)
	{
		PVPHelper.战斗状态();
		PVPTargetHelper.自动选中();
	}
	public void OnEnterRotation()
	{
		PVPHelper.进入ACR();
		Share.Pull = true;
	}
	public void OnExitRotation()
	{
		Share.Pull = false;
	}
}
