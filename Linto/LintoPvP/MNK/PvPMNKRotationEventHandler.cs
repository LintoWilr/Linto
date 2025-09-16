using AEAssist.MemoryApi;
using CombatRoutine;
using Common;
using Common.Define;
using ECommons.DalamudServices;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.MNK;

public class PvPMNKRotationEventHandler : IRotationEventHandler
{
	public void OnResetBattle()
	{
		PvPMNKBattleData.Instance.Reset();
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
		PvPSettings.Instance.YourCID = Svc.ClientState.LocalContentId;
		PvPSettings.Instance.Save(); 
		if(!PVPHelper.通用权限())
		{
			Core.Get<IMemApiChatMessage>().Toast("未获得权限，无法使用！\n\n请输入密码！");
			Core.Get<IMemApiSendMessage>().SendMessage("/e <se.6><se.6><se.6><se.6><se.6><se.6>");
			Core.Get<IMemApiSendMessage>().SendMessage("/e <se.11><se.11><se.11><se.11><se.11>");
			Core.Get<IMemApiSendMessage>().SendMessage("/e 请输入密码!");
		}
	}
	public void OnExitRotation()
	{
		Share.Pull = false;
	}
}
