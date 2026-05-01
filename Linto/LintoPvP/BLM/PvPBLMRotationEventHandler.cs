using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using Linto.LintoPvP.DRG;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.BLM;

public class PvPBLMRotationEventHandler : IRotationEventHandler
{
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {

    }
    public void OnTerritoryChanged()
    {

    }
    public void OnResetBattle() => PvPDRGBattleData.Reset();

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
        _ = new Slot();
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
    }
    public void OnEnterRotation()
    {
        PVPHelper.进入ACR();
        Share.Pull = true;
    }
    public void OnExitRotation() => Share.Pull = false;
}
