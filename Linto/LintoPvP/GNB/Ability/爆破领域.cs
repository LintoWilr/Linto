using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.PVPApi;

public class 爆破领域 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;

    // 29128 爆破领域
    public int Check()
    {
        // PVP激活检查
        if (!PVPHelper.CanActive()) return -1;
        
        // 技能可用性检查
        if (!PvPGNBOverlay.GNBQt.GetQt("爆破领域")) return -9;
        if (!29128u.GetSpell().IsReadyWithCanCast()) return -1;

        // 距离检查
        if (PVPHelper.通用距离检查(5)) return -5;
        
        // 技能释放条件检查
        if (PVPHelper.通用技能释放Check(29128u, 5) == null) return -5;
        
        IBattleChara target = PVPTargetHelper.目标模式(5, 29128u);
        if (target == null) return -999;
        
        // 血量阈值检查
        float hpThreshold = PvPGNBSettings.Instance.爆破血量 / 100f;
        if (target.CurrentHpPercent() > hpThreshold) return -6;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, 29128u, 5);
    }
}