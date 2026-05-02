using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.VPR;

public class PvPVPRRotationEventHandler : IRotationEventHandler
{
    public void OnTerritoryChanged()
    {

    }
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
        if (spell.Id == 39172u)
        {
            PvPVPRBattleData.Instance.祖灵之牙已完成 = true;
        }
        else if (spell.Id == 39173u)
        {
            PvPVPRBattleData.Instance.祖灵之牙已完成 = false;
        }
    }
    public void OnResetBattle() => PvPVPRBattleData.Reset();
    public static void OnUIUpdate()
    {
        if (PvPSettings.Instance.监控) PVPHelper.监控窗口();
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
        PVPTargetHelper.自动选中();
        if (PvPSettings.Instance.无目标坐骑)
        {
            MountHandler.无目标坐骑();
        }
        await Task.CompletedTask;
    }

    public void AfterSpell(Slot slot, Spell spell) => _ = spell.Id;
    public void OnBattleUpdate(int currTime)
    {
        PVPHelper.战斗状态();
        PVPTargetHelper.自动选中();
        if (!Core.Me.HasAura(4094u))
        {
            PvPVPRBattleData.Instance.祖灵之牙已完成 = false;
        }
    }
    public void OnEnterRotation()
    {
        PVPHelper.进入ACR();
        Share.Pull = true;
    }
    public void OnExitRotation() => Share.Pull = false;
}
