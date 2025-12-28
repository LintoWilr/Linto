using AEAssist;
using AEAssist. CombatRoutine;
using AEAssist. CombatRoutine. Module. Target;
using AEAssist. Extension;
using AEAssist. Helper;
using AEAssist. MemoryApi;
using Dalamud. Game. ClientState. Objects. Types;
using ECommons. DalamudServices;
using Linto. LintoPvP. GNB;
using Linto. LintoPvP. PVPApi. PVPApi. Target;
using System;
using System. Collections. Generic;

namespace Linto. LintoPvP. PVPApi;

public class PVPTargetHelper
	{
	/// <summary>
	/// 野火技能是否已释放的标志位
	/// </summary>
	private static bool _wildfireReleased = false;

	/// <summary>
	/// 野火技能ID常量
	/// </summary>
	private const uint WildfireSkillId = 29405;

	// 状态ID常量定义
	private const uint 免疫状态1 = 3039u; // 不死救赎
	private const uint 免疫状态2 = 2413u; // 被保护
	private const uint 免疫状态3 = 1301u; // 神圣领域
	private const uint 免疫状态4 = 1302u; // 神圣领域
	private const uint 防御状态 = 3054u;  // 龟壳
	private const uint 地天状态 = 1240u;  // 地天
	private const uint 野火光环 = 1323u;  // 野火
	private const uint 烈火环 = 4315u;   // 烈火环
	private const uint 崩破 = 3202u;  // 崩破

	/// <summary>
	/// 监控技能释放，当释放野火技能时设置标志位
	/// </summary>
	/// <param name="skillId">释放的技能ID</param>
	public static void MonitorSkill ( uint skillId )
		{
		// 只有机械师职业且释放的是野火技能时才设置标志位
		if (Core. Me. CurrentJob () == Jobs. Machinist && skillId == WildfireSkillId)
			{
			_wildfireReleased = true;
			}
		}

	/// <summary>
	/// 重置野火状态标志位
	/// </summary>
	public static void ResetWildfireStatus ()
		{
		_wildfireReleased = false;
		}

	/// <summary>
	/// 自动选择目标功能
	/// </summary>
	public static void 自动选中 ()
		{
		// 非PvP环境直接返回
		if (!Core.Me.IsPvP()) return;

		// 没有权限直接返回
		if (!( PVPHelper. 通用码权限 || PVPHelper. 高级码 )) return;

		// 未启用自动选中功能直接返回
		if (!PvPSettings. Instance. 自动选中) return;

		IBattleChara? target = null;
		
		// 如果已释放野火技能，优先选择野火目标
		if (_wildfireReleased)
			{
			target = TargetSelector. Get野火目标 ();
			// 如果野火目标不存在或距离过远，则重置状态并选择最近目标
			if (target == null || target. DistanceToPlayer () > 25)
				{
				_wildfireReleased = false;
				target = TargetSelector. Get最近目标 ();
				}
			}
		else
			{
			// 否则选择最近的目标
			target = TargetSelector. Get最近目标 ();
			}

		// 如果选中的目标与当前目标不同，且不是自己，则设置为目标
		if (target != Core. Me. GetCurrTarget () && target != Core. Me && target != null)
			{
			Core.Me.SetTarget(target);
			}
		
		}

	/// <summary>
	/// 根据设置和技能ID选择目标模式
	/// </summary>
	/// <param name="距离">技能距离</param>
	/// <param name="技能id">技能ID</param>
	/// <returns>选中的目标</returns>
	public static IBattleChara 目标模式 ( int 距离, uint 技能id )
		{
		// 快速路径：如果不需要自动选中，直接返回当前目标
		if (!PvPSettings. Instance. 技能自动选中)
			{
			return Core. Me. GetCurrTarget ();
			}

		// 野火优先级检查
		if (_wildfireReleased && 技能id == WildfireSkillId)
			{
			var wildfireTarget = TargetSelector. Get野火目标 (技能id);
			if (wildfireTarget != null && wildfireTarget. DistanceToPlayer () <= 25)
				{
				return wildfireTarget;
				}
			_wildfireReleased = false;
			}

		// 根据设置选择目标模式
		return PvPSettings. Instance. 最合适目标
			? TargetSelector. Get最合适目标 (距离 + PvPSettings. Instance. 长臂猿, 技能id)
			: TargetSelector. Get最近目标 (技能id);
		}

	/// <summary>
	/// 目标选择器类，包含各种目标选择方法
	/// </summary>
	public static class TargetSelector
		{
		/// <summary>
		/// 快速目标验证 - 按性能开销排序检查
		/// </summary>
		private static bool IsValidTarget ( IBattleChara member, int maxDistance, uint skillId, bool checkRangeAndLoS = true )
			{
			// 1. 最快速检查 (CPU开销最低)
			if (member == null || !member. IsTargetable || !member. IsEnemy ())
				return false;

			// 2. 距离检查 (相对快速)
			if (member. DistanceToPlayer () > maxDistance)
				return false;

			// 3. 视线阻挡检查
			if (PVPHelper. 视线阻挡 (member))
				return false;

			// 4. 职业过滤
			if (PvPSettings. Instance. 不选冰 && member. CurrentJob () == Jobs. Any)
				return false;

			// 5. 免疫状态检查 (HasAura调用 - 中等开销)
			if (member. HasAura (免疫状态1) || member. HasAura (免疫状态2) ||
				member. HasAura (免疫状态3) || member. HasAura (免疫状态4))
				return false;

			// 6. 防御状态检查
			if (member. HasAura (防御状态) && !Core. Me. HasAura (烈火环))
				return false;

			// 7. 地天状态检查
			if (member. HasAura (地天状态))
				return false;

			// 8. 最昂贵的检查放在最后 (Spell API)
			if (checkRangeAndLoS)
				{
				var spellApi = Core. Resolve<MemApiSpell> ();
				if (spellApi != null && !spellApi. CheckActionInRangeOrLoS (skillId, member))
					return false;
				}

			return true;
			}

		/// <summary>
		/// 从Units中获取指定范围内的敌人
		/// </summary>
		private static IEnumerable<IBattleChara> GetEnemiesInRange ( int maxDistance )
			{
			foreach (var member in TargetMgr. Instance. Units. Values)
				{
				if (member == null || !member. IsTargetable || !member. IsEnemy ())
					continue;

				if (member. DistanceToPlayer () <= maxDistance)
					yield return member;
				}
			}

		/// <summary>
		/// 获取最适合的目标（血量最低）
		/// </summary>
		/// <param name="技能距离">技能作用距离</param>
		/// <param name="技能id">技能ID用于范围和视线检查</param>
		/// <returns>血量最低的目标</returns>
		public static IBattleChara? Get最合适目标 ( int 技能距离, uint 技能id )
			{
			if (!Core. Me. IsPvP ())
				{
				return null;
				}

			var spellApi = Core. Resolve<MemApiSpell> ();
			int 最大距离 = Math. Min (PvPSettings. Instance. 自动选中自定义范围, 50);

			// 优先检查野火目标
			if (Core. Me. CurrentJob () == Jobs. Machinist)
				{
				var 野火目标 = Get野火目标 (技能id);
				if (野火目标 != null) return 野火目标;
				}

			// 最低血量目标选择
			IBattleChara? 最合适的目标 = null;
			float 当前最低血量 = float. MaxValue;

			// 使用Units集合，内部进行距离过滤
			foreach (var member in GetEnemiesInRange (最大距离))
				{
				if (!IsValidTarget (member, 技能距离, 技能id))
					continue;

				// 特殊技能血量检查
				if (技能id == 29128u && member. CurrentHpPercent () >= PvPGNBSettings. Instance. 爆破血量 / 100f)
					continue;

				// 选择最低血量目标
				if (member. CurrentHp < 当前最低血量)
					{
					最合适的目标 = member;
					当前最低血量 = member. CurrentHp;
					}
				}

			return 最合适的目标;
			}

		/// <summary>
		/// 获取距离最近的目标
		/// </summary>
		/// <param name="actionId">技能ID用于范围和视线检查</param>
		/// <returns>距离最近的目标</returns>
		public static IBattleChara? Get最近目标 ( uint? actionId = null )
			{
			if (!Core. Me. IsPvP ()) return null;

			int 最大距离 = Math. Min (PvPSettings. Instance. 自动选中自定义范围, 50);
			IBattleChara? 最近的目标 = null;
			float 最近距离 = 最大距离;

			foreach (var member in GetEnemiesInRange (最大距离))
				{
				// 使用简化验证 (不检查Spell API，除非指定actionId)
				if (!IsValidTarget (member, 最大距离, actionId ?? 0, actionId. HasValue))
					continue;

				float distance = member. DistanceToPlayer ();
				if (distance < 最近距离)
					{
					最近的目标 = member;
					最近距离 = distance;
					}
				}

			return 最近的目标;
			}

		/// <summary>
		/// 获取距离最远的目标
		/// </summary>
		/// <param name="actionId">技能ID用于范围和视线检查</param>
		/// <returns>距离最远的目标</returns>
		public static IBattleChara? Get最远目标 ( uint? actionId = null )
			{
			if (!Core. Me. IsPvP ())
				{
				return null;
				}

			var spellApi = Core. Resolve<MemApiSpell> ();
			int 最大距离 = PvPSettings. Instance. 自动选中自定义范围;
			IBattleChara? 最远的目标 = null;
			float 最远距离 = 0f;

			foreach (var member in GetEnemiesInRange (最大距离))
				{
				if (!IsValidTarget (member, 最大距离, actionId ?? 0, actionId. HasValue))
					continue;

				float distance = member. DistanceToPlayer ();
				if (distance <= 最大距离 && distance > 最远距离)
					{
					最远的目标 = member;
					最远距离 = distance;
					}
				}
			return 最远的目标;
			}

		/// <summary>
		/// 获取野火目标
		/// </summary>
		/// <param name="actionId">技能ID用于范围和视线检查</param>
		/// <returns>野火目标</returns>
		public static IBattleChara? Get野火目标 ( uint? actionId = null )
			{
			if (!Core. Me. IsPvP () || Core. Me. CurrentJob () != Jobs. Machinist)
				return null;

			int 最大距离 = Math. Min (PvPSettings. Instance. 自动选中自定义范围, 25); // 野火距离限制更严格
			IBattleChara? 最近的目标 = null;
			float 最近距离 = 最大距离;

			foreach (var member in GetEnemiesInRange (最大距离))
				{
				// 基础验证已在GetEnemiesInRange中完成

				// 核心条件：野火光环
				if (!member. HasLocalPlayerAura (野火光环))
					continue;

				// 免疫状态快速检查
				if (member. HasAura (免疫状态1) || member. HasAura (免疫状态2) ||
					member. HasAura (免疫状态3) || member. HasAura (免疫状态4) ||
					member. HasAura (防御状态) || member. HasAura (地天状态) ||
					PVPHelper. 视线阻挡 (member))
					continue;

				// 可选的Spell API检查
				if (actionId. HasValue)
					{
					var spellApi = Core. Resolve<MemApiSpell> ();
					if (spellApi != null && !spellApi. CheckActionInRangeOrLoS (actionId. Value, member))
						continue;
					}

				float distance = member. DistanceToPlayer ();
				if (distance < 最近距离)
					{
					最近的目标 = member;
					最近距离 = distance;
					}
				}

			return 最近的目标;
			}

		/// <summary>
		/// 获取多斩目标。
		/// </summary>
		/// <param name="多斩count">所需的有效敌人数量</param>
		/// <param name="actionId">技能ID用于范围和视线检查</param>
		/// <returns>符合条件的目标敌人，如果没有则返回 null</returns>
		public static IBattleChara Get多斩Target ( int 多斩count, uint? actionId = null )
			{
			if (!Core. Me. IsPvP () || Core. Me. LimitBreakCurrentValue () < 4000)
				return null;

			var spellApi = Core. Resolve<MemApiSpell> ();
			var validEnemies = new List<IBattleChara> ();

			// 预过滤：只收集符合条件的敌人 (25米范围内)
			foreach (var enemy in GetEnemiesInRange (25))
				{
				if (enemy == null || !enemy. IsTargetable) continue;

				float totalHpPercentage = enemy. CurrentHpPercent () + ( enemy. ShieldPercentage / 100f );

				if (enemy. HasLocalPlayerAura (崩破) &&
					!enemy. HasAura (免疫状态2) &&
					!enemy. HasAura (免疫状态3) &&
					totalHpPercentage <= 1.0f)
					{
					if (actionId. HasValue)
						{
						if (spellApi != null && spellApi. CheckActionInRangeOrLoS (actionId. Value, enemy))
							validEnemies. Add (enemy);
						}
					else
						{
						validEnemies. Add (enemy);
						}
					}
				}

			if (validEnemies. Count == 0) return null;

			// 寻找满足多斩条件的目标
			foreach (var target in validEnemies)
				{
				int nearbyValidCount = 0;
				foreach (var nearbyEnemy in validEnemies)
					{
					if (target. DataId == nearbyEnemy. DataId) continue;

					if (target. Distance (nearbyEnemy) <= 5f)
						{
						nearbyValidCount++;
						if (nearbyValidCount >= 多斩count)
							return target. IsTargetable ? target : null;
						}
					}
				}

			return null;
			}

		/// <summary>
		/// 获取适合进行"斩铁剑"的目标。
		/// </summary>
		/// <param name="actionId">技能ID用于范围和视线检查</param>
		/// <returns>符合条件的目标敌人，如果没有则返回 null。</returns>
		public static IBattleChara Get斩铁目标 ( uint? actionId = null )
			{
			if (!Core. Me. IsPvP () || Core. Me. LimitBreakCurrentValue () < 4000)
				return null;

			var spellApi = Core. Resolve<MemApiSpell> ();

			// 快速找到第一个符合条件的就返回 (25米范围内)
			foreach (var enemy in GetEnemiesInRange (25))
				{
				if (enemy == null || !enemy. IsTargetable) continue;

				float totalHpPercentage = enemy. CurrentHpPercent () + ( enemy. ShieldPercentage / 100f );

				if (totalHpPercentage <= 1.0f &&
					enemy. HasLocalPlayerAura (崩破) &&
					!enemy. HasAura (免疫状态1) &&
					!enemy. HasAura (免疫状态2) &&
					!enemy. HasAura (免疫状态3))
					{
					if (actionId. HasValue)
						{
						if (spellApi != null && spellApi. CheckActionInRangeOrLoS (actionId. Value, enemy))
							return enemy;
						}
					else
						{
						return enemy;
						}
					}
				}

			return null;
			}
		}

	/// <summary>
	/// 上一次AOE目标搜索时间点
	/// </summary>
	private static long 上一次AOE目标搜索时间点;

	/// <summary>
	/// 检查目标是否有罩子（龟壳）
	/// </summary>
	/// <param name="target">目标</param>
	/// <returns>有罩子返回true，否则返回false</returns>
	public static bool Check目标罩子 ( IBattleChara? target )
		{
		return target != null && target. HasAura (防御状态);
		}

	/// <summary>
	/// 检查目标是否无敌
	/// </summary>
	/// <param name="target">目标</param>
	/// <returns>无敌返回true，否则返回false</returns>
	public static bool Check目标无敌 ( IBattleChara? target )
		{
		return target != null && ( target. HasAura (免疫状态1) || target. HasAura (免疫状态4) );
		}

	/// <summary>
	/// 检查目标是否处于地天状态
	/// </summary>
	/// <param name="target">目标</param>
	/// <returns>地天状态返回true，否则返回false</returns>
	public static bool Check目标地天 ( IBattleChara? target )
		{
		return target != null && target. HasAura (地天状态);
		}

	/// <summary>
	/// 检查目标是否不可攻击 - 内联优化版本
	/// </summary>
	/// <param name="target">目标</param>
	/// <returns>不可攻击返回true，否则返回false</returns>
	public static bool Check目标不可攻击 ( IBattleChara? target )
		{
		if (target == null) return true;

		// 内联检查，避免多次函数调用
		return target. HasAura (免疫状态1) || target. HasAura (免疫状态4) ||  // 不死救赎、神圣领域
			   target. HasAura (免疫状态2) ||                                // 被保护
			   ( target. HasAura (防御状态) && !Core. Me. HasAura (烈火环) ) ||   // 龟壳（无烈火环时）
			   target. HasAura (地天状态);                                   // 地天
		}

	/// <summary>
	/// 检查目标是否可施法
	/// </summary>
	/// <param name="target">目标</param>
	/// <param name="actionId">技能ID</param>
	/// <returns>可施法返回true，否则返回false</returns>
	public static bool Check目标可施法 ( IBattleChara? target, uint actionId )
		{
		// 检查目标是否为空或不可选中
		if (target == null || !target. IsTargetable)
			{
			return false;
			}

		// 获取 Spell API 实例
		var spellApi = Core. Resolve<MemApiSpell> ();
		if (spellApi == null)
			{
			return false;
			}

		// 使用提供的actionId检查范围和视线
		return spellApi. CheckActionInRangeOrLoS (actionId, target);
		}

	/// <summary>
	/// 免控BUFF列表
	/// </summary>
	public static List<uint> 免控BUFFList => new List<uint>
	{
		3054u,//防御
        3248u,//活性
        1320u,//明镜
    };

	/// <summary>
	/// 检查目标是否免控
	/// </summary>
	/// <param name="target">目标</param>
	/// <returns>免控返回true，否则返回false</returns>
	public static bool Check目标免控 ( IBattleChara? target )
		{
		return target != null && target. HasAnyAura (免控BUFFList);
		}

	/// <summary>
	/// 过滤角色 - 优化版本
	/// </summary>
	/// <param name="unit">单位</param>
	/// <param name="filter">过滤器类型</param>
	/// <param name="actionId">技能ID</param>
	/// <returns>符合条件返回true，否则返回false</returns>
	private static bool FilterCharacter ( IBattleChara? unit, Filter filter, uint? actionId = null )
		{
		if (filter == Filter. None) return true;
		if (unit == null || !unit. IsTargetable) return false;

		// 快速路径：先检查可施法
		if (actionId. HasValue)
			{
			var spellApi = Core. Resolve<MemApiSpell> ();
			if (spellApi == null || !spellApi. CheckActionInRangeOrLoS (actionId. Value, unit))
				return false;
			}

		switch (filter)
			{
			case Filter. 可施法:
				return true;
			case Filter. 可攻击:
				return !Check目标不可攻击 (unit);
			case Filter. 无无敌:
				return !( unit. HasAura (免疫状态1) || unit. HasAura (免疫状态4) );
			case Filter. 可控制:
				return !unit. HasAnyAura (免控BUFFList);
			default:
				return false;
			}
		}

	/// <summary>
	/// 获取全部单位
	/// </summary>
	/// <param name="type">单位类型</param>
	/// <param name="range">范围</param>
	/// <param name="filter">过滤器</param>
	/// <param name="actionId">技能ID</param>
	/// <returns>符合条件的单位字典</returns>
	public static Dictionary<uint, IBattleChara> Get全部单位 ( Group type, float range = 50f, Filter filter = Filter. None, uint? actionId = null )
		{
		Dictionary<uint, IBattleChara> 全部单位 = TargetMgr. Instance. Units;
		Dictionary<uint, IBattleChara> dict = new Dictionary<uint, IBattleChara> ();

		// 遍历所有单位
		foreach (IBattleChara unit in 全部单位. Values)
			{
			// 过滤不符合条件的单位
			if (!FilterCharacter (unit, filter, actionId) || unit. IsDead || unit. DistanceToPlayer () > range)
				{
				continue;
				}

			// 根据单位类型进行筛选
			switch (type)
				{
				case Group. 全部:
					AddUnitToDict (dict, unit);
					break;

				case Group. 敌人:
					if (unit. ValidAttackUnit ())
						{
						AddUnitToDict (dict, unit);
						}
					break;

				case Group. 队友:
					if (!unit. ValidAttackUnit ())
						{
						AddUnitToDict (dict, unit);
						}
					break;
				}
			}

		return dict;
		}

	// 辅助方法：将单位添加到字典中
	private static void AddUnitToDict ( Dictionary<uint, IBattleChara> dict, IBattleChara unit )
		{
		uint key = GetGameObjectIdAsUint (unit. GameObjectId);
		if (!dict. ContainsKey (key))
			{
			dict [key] = unit;
			}
		}

	// 转换 GameObjectId（ulong）为 uint
	private static uint GetGameObjectIdAsUint ( ulong gameObjectId )
		{
		if (gameObjectId > uint. MaxValue)
			{
			LogHelper. Error ("GameObjectId 超出了 uint 的范围！");
			return 0;
			}

		return (uint) gameObjectId;
		}

	/// <summary>
	/// 获取看着目标的人
	/// </summary>
	/// <param name="type">单位类型</param>
	/// <param name="target">目标</param>
	/// <param name="range">范围</param>
	/// <param name="actionId">技能ID</param>
	/// <returns>看着目标的人列表</returns>
	public static List<IBattleChara> Get看着目标的人 ( Group type, IBattleChara target, float range = 50f, uint? actionId = null )
		{
		List<IBattleChara> re = new List<IBattleChara> ();

		// 添加 null 检查
		if (target == null)
			{
			return re;
			}

		Dictionary<uint, IBattleChara> 全部单位 = Get全部单位 (type, range, Filter. None, 29415u);
		foreach (IBattleChara 单位 in 全部单位. Values)
			{
			if (单位. CurrentJob () == Jobs. Any) continue;
			var currTarget = 单位. GetCurrTarget ();
			if (currTarget != null && currTarget. GameObjectId == target. GameObjectId)
				{
				float distance = 单位. DistanceToPlayer ();
				if (distance <= range)
					{
					re. Add (单位);
					}
				}
			}
		return re;
		}

	/// <summary>
	/// 检查自身周围6码内是否存在地天状态的敌对玩家
	/// </summary>
	/// <returns>存在返回true，否则返回false</returns>
	public static bool 自身周围6码内存在地天状态敌对玩家 ()
		{
		// 非PvP环境直接返回false
		if (!Core. Me. IsPvP ()) return false;

		// 检查自身周围6码内的所有敌对玩家
		foreach (var unit in TargetMgr. Instance. Units. Values)
			{
			// 基础过滤条件
			if (unit == null ||
				!unit. IsTargetable ||
				!unit. IsEnemy () ||
				unit. IsDead)
				continue;

			// 距离检查 - 6码范围内
			if (unit. DistanceToPlayer () > 6f)
				continue;

			// 视线阻挡检查
			if (PVPHelper. 视线阻挡 (unit))
				continue;

			// 核心条件：检查地天状态
			if (unit. HasAura (地天状态))
				{
				return true;
				}
			}

		return false;
		}

	/// <summary>
	/// 获取自身周围6码内所有地天状态的敌对玩家列表
	/// </summary>
	/// <returns>地天状态敌对玩家列表</returns>
	public static List<IBattleChara> 获取自身周围6码内地天状态敌对玩家 ()
		{
		var result = new List<IBattleChara> ();

		// 非PvP环境直接返回空列表
		if (!Core. Me. IsPvP ()) return result;

		// 检查自身周围6码内的所有敌对玩家
		foreach (var unit in TargetMgr. Instance. Units. Values)
			{
			// 基础过滤条件
			if (unit == null ||
				!unit. IsTargetable ||
				!unit. IsEnemy () ||
				unit. IsDead)
				continue;

			// 距离检查 - 6码范围内
			if (unit. DistanceToPlayer () > 6f)
				continue;

			// 视线阻挡检查
			if (PVPHelper. 视线阻挡 (unit))
				continue;

			// 核心条件：检查地天状态
			if (unit. HasAura (地天状态))
				{
				result. Add (unit);
				}
			}

		return result;
		}

	/// <summary>
	/// 检查指定目标周围6码内是否存在地天状态的敌对玩家
	/// </summary>
	/// <param name="target">指定的目标</param>
	/// <returns>存在返回true，否则返回false</returns>
	public static bool 目标周围6码内存在地天状态敌对玩家 ( IBattleChara target )
		{
		if (target == null || !Core. Me. IsPvP ()) return false;

		foreach (var unit in TargetMgr. Instance. Units. Values)
			{
			// 基础过滤条件
			if (unit == null ||
				!unit. IsTargetable ||
				!unit. IsEnemy () ||
				unit. IsDead ||
				unit. GameObjectId == target. GameObjectId) // 排除目标自身
				continue;

			// 距离检查 - 与目标的距离在6码内
			if (target. Distance (unit) > 6f)
				continue;

			// 视线阻挡检查（相对于目标）
			//if (PVPHelper. 视线阻挡 (unit, target))
			//	continue;

			// 核心条件：检查地天状态
			if (unit. HasAura (地天状态))
				{
				return true;
				}
			}

		return false;
		}
	}