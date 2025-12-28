using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.Extension;
using AEAssist.MemoryApi;
using Linto.LintoPvP.PVPApi;

namespace Linto.LintoPvP.PCT;

public class PvPPCTRotationEventHandler : IRotationEventHandler
{
    public void OnTerritoryChanged()
    {

    }
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {

    }
    public void OnResetBattle()
    {
        PvPPCTBattleData.Instance.Reset();
    }


    public async Task OnPreCombat()
    {

        var slot = new Slot();
        PVPTargetHelper.自动选中();
        if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物彩绘) != PCTSkillID.技能动物彩绘 && !Core.Me.IsMoving())
        {
            slot.Add(PVPHelper.等服务器Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物彩绘), Core.Me));
            await slot.Run(AI.Instance.BattleData, false);
        }
        else if (PvPSettings.Instance.无目标坐骑)
        {
            MountHandler.无目标坐骑();
        }
        await Task.CompletedTask;
    }


    public async Task OnNoTarget()
    {
        var slot = new Slot();
        PVPTargetHelper.自动选中();
        if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物彩绘) != PCTSkillID.技能动物彩绘 && !Core.Me.IsMoving())
        {
            slot.Add(PVPHelper.等服务器Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能动物彩绘), Core.Me));
            await slot.Run(AI.Instance.BattleData, false);
        }
        else if (PvPSettings.Instance.无目标坐骑)
        {
            MountHandler.无目标坐骑();
        }
        await Task.CompletedTask;
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        uint id = spell.Id;
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
