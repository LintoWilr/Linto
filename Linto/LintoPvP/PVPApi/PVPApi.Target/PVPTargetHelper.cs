using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.PVPApi;

public class PVPTargetHelper
{
	public static void 自动选中()
	{
		if(!Core.Me.IsPvP()) return;
		if (!(PVPHelper.通用码权限||PVPHelper.高级码)) return;
		if (PvPSettings.Instance.自动选中)
		{
			IBattleChara?target = TargetSelector.Get最近目标();
			if (TargetSelector.Get最近目标()!=Core.Me.GetCurrTarget()&&target!=Core.Me&&target!= null)
			{
				Svc.Targets.Target = target;
			}
		}
	}
	
	public static IBattleChara 目标模式(int 距离,uint 技能id)
	{
		if (PvPSettings.Instance.最合适目标&&PvPSettings.Instance.技能自动选中)
		{
			return TargetSelector.Get最合适目标(距离+ PvPSettings.Instance.长臂猿,技能id);
		}
		else if (PvPSettings.Instance.技能自动选中)
		{
			return TargetSelector.Get最近目标();
		}
		else return Core.Me.GetCurrTarget();
	}
	public static class TargetSelector
	{

		/// <summary>
		/// 获取最近的血量最低目标
		/// </summary>
		/// <param name="技能距离">技能作用距离</param>
		/// <returns>血量最低的目标</returns>
		public static IBattleChara? Get最合适目标(int 技能距离 ,uint 技能id)
		{
			if (!Core.Me.IsPvP())
			{
				return null;
			}

			// ====================== 野火目标优先选择 ======================
			if (Core.Me.CurrentJob() == Jobs.Machinist)
			{
				int 野火最大距离 = Math.Min(PvPSettings.Instance.自动选中自定义范围, 50);
				IBattleChara? 野火目标 = null;
				float 最近距离 = float.MaxValue;
				lock (TargetMgr.Instance.Enemys)
				{
					foreach (var member in TargetMgr.Instance.EnemysIn25.Values)
					{
						if (member == null || !member.IsTargetable || !member.IsEnemy()) continue;
						if (PvPSettings.Instance.不选冰 && member.CurrentJob() == Jobs.Any) continue;
						try
						{
							// 免疫状态检查（不死救赎、保护、神圣领域等）
							if (member.HasAura(3039u) || member.HasAura(2413u) || member.HasAura(1301u) ||
							    member.HasAura(1302u))
								continue;

							// 防御状态检查（龟壳、地天、视线阻挡）
							if(技能id!=29405u&&member.HasAura(3054u) )
								continue;
							if (member.HasAura(1240u) || PVPHelper.视线阻挡(member)) continue;

							// 核心条件：必须由玩家自身施加的野火(1323)
							if (!member.HasLocalPlayerAura(1323u)) continue;

							// 距离检查
							float distance = member.Distance(Core.Me);
							if (distance <= 野火最大距离 && distance < 最近距离)
							{
								野火目标 = member;
								最近距离 = distance;
							}
						}
						catch (Exception ex)
						{
							LogHelper.Error($"野火目标选择报错:{member?.Name ?? "未知目标"}");
						}
					}
				}

				// 优先返回野火目标
				if (野火目标 != null)
				{
					return 野火目标;
				}
			}
			// ====================== 结束野火选择 ======================

			// ====================== 原始最低血量选择逻辑 ======================
			IBattleChara? 最合适的目标 = null;
			float 当前最低血量 = float.MaxValue;
			foreach (var member in TargetMgr.Instance.Units.Values)
			{
				if (member == null || !member.IsTargetable || !member.IsEnemy() || PVPHelper.视线阻挡(member)) continue;
				if (PvPSettings.Instance.不选冰 && member.CurrentJob() == Jobs.Any) continue;
				if (技能id == 29128u&&member.CurrentHpPercent() >= PvPGNBSettings.Instance.爆破血量/100f) continue;
				try
				{
					// 免疫状态检查（与野火相同）
					if (member.HasAura(3039u) || member.HasAura(2413u) || member.HasAura(1301u) ||
					    member.HasAura(1302u))
						continue;

					// 特殊处理：无烈火环时跳过龟壳
					if (!Core.Me.HasAura(4315u)) // 烈火环检查
					{
						if (member.HasAura(3054u)) // 龟壳
							continue;
					}

					// 跳过地天状态
					if (member.HasAura(1240u)) continue;

					// 距离检查
					float distance = member.DistanceToPlayer();
					if (distance > 技能距离) continue;

					// 选择最低血量目标
					if (member.CurrentHp < 当前最低血量)
					{
						最合适的目标 = member;
						当前最低血量 = member.CurrentHp;
					}
				}
				catch (Exception ex)
				{
					LogHelper.Error($"血量目标报错:{member?.Name ?? "未知目标"}");
				}
			}
			// ====================== 结束原始逻辑 ======================

			return 最合适的目标;
		}
		/// <summary>
		/// 获取距离最近的目标
		/// </summary>
		/// <returns>距离最近的目标</returns>
		public static IBattleChara? Get最近目标()
		{
			if (!Core.Me.IsPvP())
			{
				return null;
			}
    
			int 最大距离 = PvPSettings.Instance.自动选中自定义范围;
			IBattleChara? 最近的目标 = null;
			float 最近距离 = Math.Min(最大距离, 50f);
			
			foreach (var member in TargetMgr.Instance.Units.Values)
			{
				if (member == null || !member.IsTargetable || !member.IsEnemy()||PVPHelper.视线阻挡(member))
					continue;
				try
				{
					//不死救赎3039 神圣领域1302 被保护2413 龟壳3054 地天1240
					if(member.HasAura(3039u) ||  member.HasAura(2413u)||member.HasAura(1301u)||member.HasAura(1302u))
						continue;
					if (PvPSettings.Instance.不选冰&&member.CurrentJob()==Jobs.Any)
						continue;
					if (!Core.Me.HasAura(4315u)) //烈火环
					{
						if (member.HasAura(3054u)) 
							continue;
					}
					if(member.HasAura(1240u))
						continue;
					float distance = member.Distance(Core.Me);
					if (distance <= 最大距离 && distance < 最近距离)
					{
						最近的目标 = member;
						最近距离 = distance;
					}
				}
				catch (Exception ex)
				{
					LogHelper.Error($"报错对象:{member.Name}");
				}
			}
			return 最近的目标;
		}
		/// <summary>
		/// 获取距离最远的目标
		/// </summary>
		/// <returns>距离最远的目标</returns>
		public static IBattleChara? Get最远目标()
		{
			if (!Core.Me.IsPvP())
			{
				return null;
			}

			int 最大距离 = PvPSettings.Instance.自动选中自定义范围;
			IBattleChara? 最远的目标 = null;
			float 最远距离 = 0f;

			lock (TargetMgr.Instance.Enemys)
			{
				foreach (var member in TargetMgr.Instance.Units.Values)
				{
					if (member == null || !member.IsTargetable || !member.IsEnemy()||PVPHelper.视线阻挡(member))
						continue;
					if (PvPSettings.Instance.不选冰&&member.CurrentJob()==Jobs.Any)
						continue;
					try
					{
						//不死救赎3039 神圣领域1302 被保护2413 龟壳3054 地天1240
						if(member.HasAura(3039u) ||  member.HasAura(2413u)||member.HasAura(1301u)||member.HasAura(1302u))
							continue;
						if (!Core.Me.HasAura(4315u)) //烈火环
						{
							if (member.HasAura(3054u)) 
								continue;
						}
						if(member.HasAura(1240u))
							continue;
						float distance = member.Distance(Core.Me);
						if (distance <= 最大距离 && distance > 最远距离)
						{
							最远的目标 = member;
							最远距离 = distance;
						}
					}
					catch (Exception ex)
					{
						LogHelper.Error($"报错对象:{member.Name}");
					}
				}
			}
			return 最远的目标;
		}
		public static IBattleChara? Get野火目标()
		{
			if (!Core.Me.IsPvP())
			{
				return null;
			}
			if (!(Core.Me.CurrentJob() == Jobs.Machinist))
			{
				return null;
			}
			int 最大距离 = PvPSettings.Instance.自动选中自定义范围;
			IBattleChara? 最近的目标 = null;
			float 最近距离 = Math.Min(最大距离, 50f);
    
			lock (TargetMgr.Instance.Enemys)
			{
				foreach (var member in TargetMgr.Instance.EnemysIn25.Values)
				{
					if (member == null || !member.IsTargetable || !member.IsEnemy())
						continue;
					if (PvPSettings.Instance.不选冰&&member.CurrentJob()==Jobs.Any)
						continue;
					try
					{
						if(member.HasAura(3039u) ||  member.HasAura(2413u)||member.HasAura(1301u)||member.HasAura(1302u))
							continue;
						if (member.HasAura(3054u) ||  member.HasAura(1240u)||PVPHelper.视线阻挡(member))
							continue;
						if (!member.HasLocalPlayerAura(1323u))
							continue;
						float distance = member.Distance(Core.Me);
						if (distance <= 最大距离 && distance < 最近距离)
						{
							最近的目标 = member;
							最近距离 = distance;
						}
					}
					catch (Exception ex)
					{
						LogHelper.Error($"报错对象:{member.Name}");
					}
				}
			}
			return 最近的目标;
		}
		/// <summary>
		/// 获取多斩目标。
		/// </summary>
		/// <param name="多斩count">所需的有效敌人数量</param>
		/// <returns>符合条件的目标敌人，如果没有则返回 null</returns>
		public static IBattleChara Get多斩Target(int 多斩count)
		{
			// 检查玩家是否处于 PvP 模式
			if (!Core.Me.IsPvP())
			{
				return null;
			}
			// 检查玩家的 Limit Break 值是否足够
			if (Core.Me.LimitBreakCurrentValue() < 4000)
			{
				return null;
			}
			// 获取半径25内的所有敌人
			var enemiesIn25 = TargetMgr.Instance.EnemysIn25.Values;
			// 创建一个列表存储符合初始条件的敌人
			List<IBattleChara> validEnemies = new List<IBattleChara>();
			// 预过滤符合条件的敌人，减少后续循环中的判断次数
			foreach (var enemy in enemiesIn25)
			{
				// 计算敌人的总生命值百分比，包括护盾
				float totalHpPercentage = enemy.CurrentHpPercent() + (enemy.ShieldPercentage / 100f);
				// 检查敌人是否具备特定的BUFF，且生命值符合要求
				if (enemy.HasLocalPlayerAura(3202u) &&   // 检查是否有本地玩家的光环3202
				    !enemy.HasAura(2413u) &&             // 检查是否没有光环2413
				    !enemy.HasAura(1301u) &&             // 检查是否没有光环1301
				    totalHpPercentage <= 1.0f)            // 检查总生命值是否不超过100%
				{
					validEnemies.Add(enemy);
				}
			}
			// 如果没有任何符合初始条件的敌人，直接返回 null
			if (validEnemies.Count == 0)
			{
				return null;
			}
			// 遍历所有符合初始条件的敌人，寻找符合多斩条件的目标
			foreach (var target in validEnemies)
			{
				int nearbyValidCount = 0;
				// 遍历所有符合初始条件的敌人，统计与目标距离在5以内的数量
				foreach (var nearbyEnemy in validEnemies)
				{
					// 跳过自身
					if (target.DataId == nearbyEnemy.DataId)
					{
						continue;
					}
					// 计算目标与附近敌人的距离
					float distance = target.Distance(nearbyEnemy);
					// 检查距离是否在5以内
					if (distance <= 5f)
					{
						nearbyValidCount++;
						// 一旦满足多斩数量要求，立即返回目标（如果目标可攻击）
						if (nearbyValidCount >= 多斩count)
						{
							return target.IsTargetable ? target : null;
						}
					}
				}
			}
			// 如果没有找到符合条件的目标，返回 null
			return null;
		}
		/// <summary>
		/// 获取适合进行“斩铁”技能的目标。
		/// </summary>
		/// <returns>符合条件的目标敌人，如果没有则返回 null。</returns>
		public static IBattleChara Get斩铁目标()
		{
			// 检查玩家是否处于 PvP 模式
			if (!Core.Me.IsPvP())
			{
				return null;
			}
			// 检查玩家的 Limit Break 值是否足够
			if (Core.Me.LimitBreakCurrentValue() < 4000)
			{
				return null;
			}
			// 获取半径25内的所有敌人
			var enemiesIn25 = TargetMgr.Instance.EnemysIn25.Values;
			// 遍历所有敌人，寻找符合“斩铁”目标条件的敌人
			foreach (var enemy in enemiesIn25)
			{
				if (Is斩铁目标Eligible(enemy))
				{
					// 一旦找到第一个符合条件的目标，立即返回
					return enemy;
				}
			}
			// 如果没有找到符合条件的目标，返回 null
			return null;
		}
		/// <summary>
		/// 判断敌人是否符合“斩铁”目标的条件。
		/// </summary>
		/// <param name="enemy">要检查的敌人对象。</param>
		/// <returns>如果符合条件则返回 true，否则返回 false。</returns>
		private static bool Is斩铁目标Eligible(IBattleChara enemy)
		{
			// 计算敌人的总生命值百分比，包括护盾
			float totalHpPercentage = enemy.CurrentHpPercent() + (enemy.ShieldPercentage / 100f);
			// 检查敌人是否具备特定的条件
			bool isTargetable = enemy.IsTargetable;
			bool hasSufficientHp = totalHpPercentage <= 1.0f;
			bool hasRequiredAura = enemy.HasLocalPlayerAura(3202u);
			bool lacksForbiddenAuras = !enemy.HasAura(3039u) && !enemy.HasAura(2413u) && !enemy.HasAura(1301u);

			// 返回所有条件的逻辑与结果
			return isTargetable && hasSufficientHp && hasRequiredAura && lacksForbiddenAuras;
		}
	}

	private static long 上一次AOE目标搜索时间点;
	public static bool Check目标罩子(IBattleChara? target)
	{
		return PVPHelper.HasBuff(target, 3054u);
	}
	public static bool Check目标无敌(IBattleChara? target)
	{
		if (!PVPHelper.HasBuff(target, 3039u))
		{
			return PVPHelper.HasBuff(target, 1302u);
		}
		return true;
	}
	public static bool Check目标地天(IBattleChara? target)
	{
		return PVPHelper.HasBuff(target, 1240u);
	}
	public static bool Check目标不可攻击(IBattleChara? target)
	{
		if (!Check目标无敌(target) && !Check目标罩子(target))
		{
			return Check目标地天(target);
		}
		return true;
	}
	public static bool Check目标可施法(IBattleChara? target)
	{
		if (target.IsTargetable)
		{
			//不太好用
			return target.InLineOfSight();
		}
		return false;
	}

	public static List<uint> 免控BUFFList => new List<uint>
	{
		3054u,//防御
		3248u,//活性
		1320u,//明镜
	};
	public static bool Check目标免控(IBattleChara? target)
	{
		return target.HasAnyAura(免控BUFFList);
	}
	private static bool FilterCharacter(IBattleChara? unit, Filter filter)
	{
		if (filter == Filter.None)
		{
			return true;
		}
		if (Check目标可施法(unit))
		{
			switch (filter)
			{
				case Filter.可施法:
					return true;
				case Filter.可攻击:
					if (!Check目标不可攻击(unit))
					{
						return true;
					}
					break;
			}
			if (filter == Filter.无无敌 && !Check目标无敌(unit))
			{
				return true;
			}
			if (filter == Filter.可控制 && !Check目标免控(unit))
			{
				return true;
			}
		}
		return false;
	}

	public static Dictionary<uint, IBattleChara> Get全部单位(Group type, float range = 50f, Filter filter = Filter.None)
	{
		Dictionary<uint, IBattleChara> 全部单位 = TargetMgr.Instance.Units;
		Dictionary<uint, IBattleChara> dict = new Dictionary<uint, IBattleChara>();

		// 遍历所有单位
		foreach (IBattleChara unit in 全部单位.Values)
		{
			// 过滤不符合条件的单位
			if (!FilterCharacter(unit, filter) || unit.IsDead || unit.DistanceToPlayer() > range)
			{
				continue;
			}

			// 根据单位类型进行筛选
			switch (type)
			{
				case Group.全部:
					AddUnitToDict(dict, unit);
					break;

				case Group.敌人:
					if (unit.ValidAttackUnit())
					{
						AddUnitToDict(dict, unit);
					}

					break;

				case Group.队友:
					if (!unit.ValidAttackUnit())
					{
						AddUnitToDict(dict, unit);
					}

					break;
			}
		}

		return dict;
	}

// 辅助方法：将单位添加到字典中
	private static void AddUnitToDict(Dictionary<uint, IBattleChara> dict, IBattleChara unit)
	{
		uint key = GetGameObjectIdAsUint(unit.GameObjectId); // 将 ulong 转换为 uint
		if (!dict.ContainsKey(key))
		{
			dict[key] = unit;
		}
	}

// 转换 GameObjectId（ulong）为 uint
	private static uint GetGameObjectIdAsUint(ulong gameObjectId)
	{
		if (gameObjectId > uint.MaxValue)
		{
			// 如果超出 uint 范围，可以根据需求做处理
			throw new OverflowException("GameObjectId 超出了 uint 的范围！");
		}

		return (uint)gameObjectId;
	}



	public static List<IBattleChara>Get看着目标的人(Group type, IBattleChara target, float range = 50f)
	{
		List<IBattleChara> re = new List<IBattleChara>();
    
		// 添加 null 检查
		if (target == null)
		{
			// 如果 target 为 null，返回空列表或者可以选择抛出异常
			return re;
		}
    
		Dictionary<uint, IBattleChara> 全部单位 = Get全部单位(type, range);
		foreach (IBattleChara 单位 in 全部单位.Values)
		{
			if (单位.CurrentJob() == Jobs.Any) break;
			var currTarget = 单位.GetCurrTarget();
			if (currTarget != null && currTarget.GameObjectId == target.GameObjectId)
			{
				float distance = 单位.DistanceToPlayer();
				if (distance <= range)
				{
					re.Add(单位);
				}
			}
		}
		return re;
	}

}
