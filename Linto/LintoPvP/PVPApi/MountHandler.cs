using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using Linto;
using Linto.LintoPvP.PVPApi;

/// <summary>
/// 处理无目标骑乘逻辑
/// </summary>
public static class MountHandler
{
    // 记录上次骑乘命令发送的时间。
    private static DateTime _lastMountTime;

    /// <summary>
    /// 检查角色是否处于骑乘状态。
    /// </summary>
    private static bool check坐骑() => Svc.Condition[ConditionFlag.Mounted];

    /// <summary>
    /// 在无目标情况下执行骑乘命令。
    /// 如果条件满足，将发送 "/ac 随机坐骑" 指令以使用随机坐骑。
    /// </summary>
    public static void 无目标坐骑()
    {
        if (!Core.Me.IsPvP()) return;
        if (!(PVPHelper.通用码权限 || PVPHelper.高级码)) return;
        // 检查是否可以进行骑乘，如果不满足条件则返回。
        if (!CanUseMount()) return;
        if (Core.Me.GetCurrTarget() != null) return;
        // 检查附近敌人数量，如果超过设定范围内的敌人数量，则返回。
        if (TargetHelper.GetNearbyEnemyCount(Core.Me,
                PvPSettings.Instance.无目标坐骑范围,
                PvPSettings.Instance.无目标坐骑范围) > 1)
        {
            return;
        }

        // 检查技能（ID: 29055）是否在冷却中，若在冷却则返回。
        if (29055u.RecentlyUsed(2000)) return;

        // 检查是否在骑乘冷却时间内，若是则返回。
        if (IsMountCooldownInEffect()) return;

        // 发送骑乘命令并更新最后骑乘时间。
        if (PvPSettings.Instance.指定坐骑)
        {
            Core.Resolve<MemApiSendMessage>().SendMessage("/mcancel");
            Core.Resolve<MemApiSendMessage>().SendMessage($"/mount {PvPSettings.Instance.坐骑名}");
        }
        else
        {
            Core.Resolve<MemApiSendMessage>().SendMessage("/mcancel");
            Core.Resolve<MemApiSendMessage>().SendMessage("/mount 专属陆行鸟");
        }
        _lastMountTime = DateTime.Now;
    }
    // 定义限制区域 ID 常量
    private const uint 狼狱停船厂 = 250;
    private const uint 赤土红沙 = 1138;
    private const uint 赤土红沙自定义 = 1139;
    private const uint 机关大殿 = 1116;
    private const uint 机关大殿自定义 = 1117;
    private const uint 角力学校 = 1032;
    private const uint 火山之心 = 1033;
    private const uint 九霄云上 = 1034;
    private const uint 角力学校自定义 = 1058;
    private const uint 火山之心自定义 = 1059;
    private const uint 九霄云上自定义 = 1060;

    public static readonly HashSet<uint> RestrictedTerritoryIds = new HashSet<uint>
    {
        狼狱停船厂,
        赤土红沙,
        赤土红沙自定义,
        机关大殿,
        机关大殿自定义,
        角力学校,
        火山之心,
        九霄云上,
        角力学校自定义,
        火山之心自定义,
        九霄云上自定义,
    };

    /// <summary>
    /// 检查角色是否处于骑乘状态。
    /// </summary>
    private static bool IsMounted() => Svc.Condition[ConditionFlag.Mounted];

    /// <summary>
    /// 在无目标情况下执行骑乘命令。
    /// 如果条件满足，将发送 "/ac 随机坐骑" 指令以使用随机坐骑。
    /// </summary>
    public static void UseMountWithoutTarget()
    {
        // 检查是否可以进行骑乘，如果不满足条件则返回
        if (!CanUseMount()) return;

        // 检查附近敌人数量，如果超过设定范围内的敌人数量则返回
        if (TargetHelper.GetNearbyEnemyCount(Core.Me,
                PvPSettings.Instance.无目标坐骑范围,
                PvPSettings.Instance.无目标坐骑范围) > 1)
        {
            return;
        }

        // 检查技能（ID: 29055）是否在冷却中，若在冷却则返回
        if (29055u.RecentlyUsed(2000)) return;

        // 检查是否在骑乘冷却时间内，若是则返回
        if (IsMountCooldownInEffect()) return;

        // 发送骑乘命令并更新最后骑乘时间
        Core.Resolve<MemApiSendMessage>().SendMessage("/mcancel");
        Core.Resolve<MemApiSendMessage>().SendMessage("/ac 随机坐骑");
        _lastMountTime = DateTime.Now;
    }

    /// <summary>
    /// 检查是否可以进行骑乘。
    /// 骑乘必须满足以下条件：
    /// 1. 配置中允许无目标骑乘。
    /// 2. 当前角色未在移动状态。
    /// 3. 当前全球冷却时间 (GCD) 为 0。
    /// 4. 当前未骑乘状态。
    /// 5. 当前未施放任何技能。
    /// 6. 当前角色在 PvP 状态。
    /// 7. 当前不在限制骑乘的领地内。
    /// </summary>
    /// <returns>
    /// 返回一个布尔值，指示是否满足骑乘条件。
    /// </returns>
    private static bool CanUseMount()
    {
        return PvPSettings.Instance.无目标坐骑
               && GCDHelper.GetGCDCooldown() == 0
               && !IsMounted()
               && Core.Me.GetCurrTarget() == null || Core.Me.GetCurrTarget().DistanceToPlayer() > 80
               && !Core.Me.IsCasting
               && Core.Me.IsPvP()
               && !IsInRestrictedTerritory();
    }

    /// <summary>
    /// 检查当前角色是否在限制骑乘的领地内。
    /// </summary>
    /// <returns>
    /// 返回一个布尔值，指示当前领地 ID 是否在限制骑乘的列表中。
    /// </returns>
    private static bool IsInRestrictedTerritory()
    {
        uint currentTerritoryId = Core.Resolve<MemApiMap>().GetCurrTerrId();
        return RestrictedTerritoryIds.Contains(currentTerritoryId);
    }

    /// <summary>
    /// 检查骑乘命令的冷却时间是否仍在限制中。
    /// </summary>
    /// <returns>
    /// 返回一个布尔值，指示是否处于骑乘冷却中。
    /// </returns>
    private static bool IsMountCooldownInEffect()
    {
        return (DateTime.Now - _lastMountTime).TotalSeconds < PvPSettings.Instance.坐骑cd;
    }
}
