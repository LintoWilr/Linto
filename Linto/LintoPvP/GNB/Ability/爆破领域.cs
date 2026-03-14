using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.PVPApi;

public class 爆破领域 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    private const int SkillRange = 5;
    private const uint SkillId = 29128u;

    // 29128 爆破领域
    public int Check()
    {
        // PVP激活检查
        if (!PVPHelper.CanActive()) return -1;
        
        // 技能可用性检查
        if (!PvPGNBOverlay.GNBQt.GetQt("爆破领域")) return -9;
        if (!SkillId.GetSpell().IsReadyWithCanCast()) return -1;

        // 距离检查
        if (PVPHelper.通用距离检查(SkillRange)) return -5;
        
        // 技能释放条件检查
        if (PVPHelper.通用技能释放Check(SkillId, SkillRange) == null) return -5;
        
        IBattleChara target = PVPTargetHelper.目标模式(SkillRange, SkillId);
        if (target == null) return -999;
        
        // 血量阈值检查
        float hpThreshold = PvPGNBSettings.Instance.爆破血量 / 100f;
        if (target.CurrentHpPercent() > hpThreshold) return -6;
        
        return 0;
    }

    public void Build(Slot slot)
    {
        PVPHelper.通用技能释放(slot, SkillId, SkillRange);
    }
}
