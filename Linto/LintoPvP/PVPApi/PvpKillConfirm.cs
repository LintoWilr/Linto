using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace Linto.LintoPvP.PVPApi;

public readonly record struct PvpKillConfirmCandidate(
    uint 技能Id,
    SpellTargetType 目标类型,
    float 血量阈值,
    int 优先级,
    Func<bool>? 前置条件 = null,
    Func<IBattleChara, bool>? 目标条件 = null,
    float? 出手血量阈值 = null);

public static class PvpKillConfirm
{
    public static Spell? 获取当前目标收头技能(IEnumerable<PvpKillConfirmCandidate> candidates)
    {
        var target = Core.Me.GetCurrTarget();
        if (target == null)
        {
            return null;
        }

        foreach (var candidate in candidates.OrderBy(candidate => candidate.优先级))
        {
            if (可作为收头技能(candidate, target))
            {
                return PVPHelper.等服务器Spell(candidate.技能Id, 选择目标(candidate, target));
            }
        }

        return null;
    }

    public static IBattleChara? 获取收头目标(IEnumerable<PvpKillConfirmCandidate> candidates)
    {
        var orderedCandidates = candidates.OrderBy(candidate => candidate.优先级).ToArray();
        if (orderedCandidates.Length == 0 || !Core.Me.IsPvP())
        {
            return null;
        }

        IBattleChara? bestTarget = null;
        PvpKillConfirmCandidate? bestCandidate = null;

        foreach (var target in TargetMgr.Instance.EnemysIn25.Values)
        {
            if (target == null)
            {
                continue;
            }

            foreach (var candidate in orderedCandidates)
            {
                if (!可作为收头技能(candidate, target))
                {
                    continue;
                }

                if (bestTarget == null || bestCandidate == null || 收头排序更优(candidate, target, bestCandidate.Value, bestTarget))
                {
                    bestTarget = target;
                    bestCandidate = candidate;
                }

                break;
            }
        }

        return bestTarget;
    }

    public static Spell? 获取收头技能(IEnumerable<PvpKillConfirmCandidate> candidates)
    {
        var orderedCandidates = candidates.OrderBy(candidate => candidate.优先级).ToArray();
        var target = 获取收头目标(orderedCandidates);
        if (target == null)
        {
            return null;
        }

        var candidate = orderedCandidates.FirstOrDefault(candidate => 可作为收头技能(candidate, target));
        return candidate.技能Id == 0 ? null : PVPHelper.等服务器Spell(candidate.技能Id, 选择目标(candidate, target));
    }

    private static bool 收头排序更优(PvpKillConfirmCandidate candidate, IBattleChara target, PvpKillConfirmCandidate bestCandidate, IBattleChara bestTarget)
    {
        if (candidate.优先级 != bestCandidate.优先级)
        {
            return candidate.优先级 < bestCandidate.优先级;
        }

        var targetHpPercent = target.CurrentHpPercent();
        var bestTargetHpPercent = bestTarget.CurrentHpPercent();
        if (Math.Abs(targetHpPercent - bestTargetHpPercent) > 0.0001f)
        {
            return targetHpPercent < bestTargetHpPercent;
        }

        if (target.CurrentHp != bestTarget.CurrentHp)
        {
            return target.CurrentHp < bestTarget.CurrentHp;
        }

        return target.DistanceToPlayer() < bestTarget.DistanceToPlayer();
    }

    private static bool 可作为收头技能(PvpKillConfirmCandidate candidate, IBattleChara target)
    {
        if (!Core.Me.IsPvP() || !目标有效(target))
        {
            return false;
        }

        if (candidate.前置条件 != null && !candidate.前置条件())
        {
            return false;
        }

        if (candidate.目标条件 != null && !candidate.目标条件(target))
        {
            return false;
        }

        var hpThreshold = 归一化血量阈值(candidate.出手血量阈值 ?? candidate.血量阈值);
        if (target.CurrentHpPercent() > hpThreshold)
        {
            return false;
        }

        if (!candidate.技能Id.GetSpell().IsReadyWithCanCast())
        {
            return false;
        }

        var spellApi = Core.Resolve<MemApiSpell>();
        return spellApi == null || spellApi.CheckActionInRangeOrLoS(candidate.技能Id, target);
    }

    private static bool 目标有效(IBattleChara target)
    {
        return target.IsTargetable && !target.IsDead && target.IsEnemy() && !PVPHelper.视线阻挡(target);
    }

    private static float 归一化血量阈值(float threshold)
    {
        if (threshold > 1f)
        {
            threshold /= 100f;
        }

        return Math.Clamp(threshold, 0f, 1f);
    }

    private static IBattleChara 选择目标(PvpKillConfirmCandidate candidate, IBattleChara target)
    {
        return candidate.目标类型 == SpellTargetType.Self ? Core.Me : target;
    }
}
