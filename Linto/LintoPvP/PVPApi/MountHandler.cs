using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Conditions;
using ECommons.DalamudServices;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using Linto;
using Linto.LintoPvP.PVPApi;


public static class MountHandler
{
    
    private static DateTime _lastMountAttempt = DateTime.MinValue;
    private const uint MountActionId = 4; // 游戏内“骑乘”动作 ID（通用）

    // 限制区域（可移到设置中）
    private static readonly HashSet<uint> RestrictedTerritoryIds = new()
    {
        250, 1138, 1139, 1116, 1117, 1032, 1033, 1034, 1058, 1059, 1060
    };

    public static void 无目标坐骑()
    {
        if (!Core.Me.IsPvP()) return;
        if (!PvPSettings.Instance.无目标坐骑) return;
        if (!(PVPHelper.通用码权限 || PVPHelper.高级码)) return;

        // 基础条件
        if (!CanMount()) return;

        // 附近敌人 >1
        if (TargetHelper.GetNearbyEnemyCount(Core.Me,
                PvPSettings.Instance.无目标坐骑范围,
                PvPSettings.Instance.无目标坐骑范围) > 1)
            return;

        // 防频繁触发（游戏内置 CD 约 1.5s + 自定义）
        if (IsOnCooldown()) return;

        // 执行骑乘（优先用技能 ID，避免文本依赖）
        ExecuteMount();
        _lastMountAttempt = DateTime.Now;
    }

    private static bool CanMount()
    {
        return
            // 配置开启
            PvPSettings.Instance.无目标坐骑 &&
            // 未在骑乘
            !Svc.Condition[ConditionFlag.Mounted] &&
            // 无目标 或 目标极远（>80y，基本无威胁）
            (Core.Me.GetCurrTarget() == null || Core.Me.GetCurrTarget().DistanceToPlayer() > 80) &&
            // 未在施法
            !Core.Me.IsCasting &&
            // 不在限制区域
            !IsInRestrictedTerritory();
    }

    private static bool IsInRestrictedTerritory()
    {
        var territoryId = Core.Resolve<MemApiMap>().GetCurrTerrId();
        return RestrictedTerritoryIds.Contains(territoryId);
    }

    private static bool IsOnCooldown()
    {
        // 游戏内置骑乘 CD 约 1.5s + 自定义 CD
        var elapsed = (DateTime.Now - _lastMountAttempt).TotalSeconds;
        return elapsed < Math.Max(1.8, PvPSettings.Instance.坐骑cd);
    }

    private static void ExecuteMount()
    {
        ExecuteMountByAction();
    }

    private unsafe static void ExecuteMountByAction()
    {
        var actionManager = FFXIVClientStructs.FFXIV.Client.Game.ActionManager.Instance();
        var mountId = PvPSettings.Instance.坐骑名; 
        if (mountId != 0)
            actionManager->UseAction(FFXIVClientStructs.FFXIV.Client.Game.ActionType.Mount, mountId);
    }
}