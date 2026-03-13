

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons.DalamudServices;
using Linto.LintoPvP.RDM;
using Linto.LintoPvP.SAM;
using System.Numerics;

namespace Linto.LintoPvP.PVPApi.PVPApi.Target;

public class HotkeyData
{
	// 小工具：统一绘制热键图标，避免每个热键都手写一遍样板代码。
	private static void DrawActionIcon(Vector2 size, uint actionId, bool useAdjustedIcon = false)
	{
		Vector2 iconSize = size * 0.8f;
		ImGui.SetCursorPos(size * 0.1f);
		IDalamudTextureWrap? textureWrap;
		if (!Core.Resolve<MemApiIcon>().GetActionTexture(actionId, out textureWrap, useAdjustedIcon))
			return;
		if (textureWrap != null) ImGui.Image(textureWrap.Handle, iconSize);
	}

	// 小工具：确保 NextSlot 可用，后面的业务逻辑就不用每次重复判空了。
	private static Slot EnsureNextSlot()
	{
		if (AI.Instance.BattleData.NextSlot == null)
			AI.Instance.BattleData.NextSlot = new Slot();
		return AI.Instance.BattleData.NextSlot;
	}

	// 小工具：按自身状态切换不同技能（图标/释放都能复用这个判断）。
	private static uint 按状态选择技能(uint auraId, uint activeSkillId, uint normalSkillId)
	{
		return Core.Me.HasLocalPlayerAura(auraId) ? activeSkillId : normalSkillId;
	}

	private static void 绘制状态切换图标(Vector2 size, uint auraId, uint activeSkillId, uint normalSkillId)
	{
		DrawActionIcon(size, 按状态选择技能(auraId, activeSkillId, normalSkillId));
	}

	private static void 绘制状态切换外显(Vector2 size, bool isActive, uint auraId, uint activeSkillId, uint normalSkillId)
	{
		SpellHelper.DrawSpellInfo(new Spell(按状态选择技能(auraId, activeSkillId, normalSkillId), Core.Me), size, isActive);
	}

	private static void 释放状态切换自施法(uint auraId, uint activeSkillId, uint normalSkillId)
	{
		EnsureNextSlot();
		// 这里不做额外判定，严格保持原来“根据状态二选一直接放技能”的行为。
		AI.Instance.BattleData.NextSlot.Add(new Spell(按状态选择技能(auraId, activeSkillId, normalSkillId), Core.Me));
	}

	public class 蛇LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 39190u);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(39190u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			EnsureNextSlot();

			// 这里先做一层“快速拦截”，条件不满足就直接走，不往下绕圈子。
			var currentTarget = Core.Me.GetCurrTarget();
			var canUseLb = !Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3000;
			if (!canUseLb || currentTarget == null || currentTarget.DistanceToPlayer() >= 25)
			{
				return;
			}

			var bestTarget = PVPTargetHelper.TargetSelector.Get最合适目标(20, 39190u);
			if (bestTarget == Core.Me)
			{
				return;
			}

			if (!PvPSettings.Instance.技能自动选中)
			{
				AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, currentTarget));
				return;
			}

			if (PvPSettings.Instance.最合适目标)
			{
				if (bestTarget != null)
				{
					AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, bestTarget));
				}
				return;
			}

			var nearestTarget = PVPTargetHelper.TargetSelector.Get最近目标();
			if (nearestTarget != null)
			{
				AI.Instance.BattleData.NextSlot.Add(new Spell(39190u, nearestTarget));
			}
		}
	}

	public class 机工LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29415u, true);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29415u, Core.Me.GetCurrTarget() ?? Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			EnsureNextSlot();
			if (Core.Me.LimitBreakCurrentValue() < 3000)
			{
				return;
			}

			if (PvPMCHSettings.Instance.智能魔弹)
			{
				// 智能模式下只算一次最优目标，避免同一帧里反复取值前后不一致。
				var bestTarget = PVPTargetHelper.TargetSelector.Get最合适目标(50, 29415u);
				if (bestTarget is { IsTargetable: true })
			{
					AI.Instance.BattleData.NextSlot.Add(new Spell(29415u, bestTarget));
					Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {bestTarget.Name} 释放 魔弹射手,距离你{bestTarget.DistanceToPlayer()}米!", 1, 3000);
				}
				return;
			}

			var currentTarget = Core.Me.GetCurrTarget();
			if (currentTarget == null)
			{
				return;
			}

			AI.Instance.BattleData.NextSlot.Add(new Spell(29415u, currentTarget));
			Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {currentTarget.Name} 释放 魔弹射手,距离你{currentTarget.DistanceToPlayer()}米!", 1, 3000);
		}
	}
    public class 龙神LB : IHotkeyResolver
    {
        private const uint LBID = 29673; // 龙神LB技能ID
        private const float 最大释放距离 = 30f;      // LB最大释放距离
		public void Draw(Vector2 size) => DrawActionIcon(size, LBID, true);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(LBID, Core.Me.GetCurrTarget() ?? Core.Me), size, isActive);
        public int Check() => 0;

        public void Run()
        {
            Slot slot = new Slot();
            Vector3 gamePos = Core.Me.GetCurrTarget()?.Position ?? Core.Me.Position;
            var factPos = GetMaxSetPosition(Core.Me.Position, gamePos, 最大释放距离);
            LogHelper.Print("即将释放释放LB技能Id：" + LBID);

            ///法系特殊处理
            //默认为当前选中目标位置，无目标则放在自己脚下
            //确保为可释放位置[指定目标朝向最大距离位置]
            //法系LB是圆形,不用一定放在目标jio下,满足[玩家和地点距离]<=最大释放距离+LB伤害半径即可

            slot.Add(new Spell(LBID, factPos));

            AI.Instance.BattleData.NextSlot = slot;
        }

    }
    public class 凤凰LB : IHotkeyResolver
    {
        private const uint LBID = 29678; // 龙神LB技能ID
        private const float 最大释放距离 = 30f;      // LB最大释放距离
		public void Draw(Vector2 size) => DrawActionIcon(size, LBID, true);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(LBID, Core.Me.GetCurrTarget() ?? Core.Me), size, isActive);
        public int Check() => 0;

        public void Run()
        {
            Slot slot = new Slot();
            Vector3 gamePos = Core.Me.GetCurrTarget()?.Position ?? Core.Me.Position;
            var factPos = GetMaxSetPosition(Core.Me.Position, gamePos, 最大释放距离);
            LogHelper.Print("即将释放释放LB技能Id：" + LBID);

            ///法系特殊处理
            //默认为当前选中目标位置，无目标则放在自己脚下
            //确保为可释放位置[指定目标朝向最大距离位置]
            //法系LB是圆形,不用一定放在目标jio下,满足[玩家和地点距离]<=最大释放距离+LB伤害半径即可

            slot.Add(new Spell(LBID, factPos));

            AI.Instance.BattleData.NextSlot = slot;
        }

    }
    /// <summary>
    /// 计算最大有效释放位置（移入类内，修正逻辑）
    /// 逻辑：仅计算XZ平面距离，Y轴保留目标的高度
    /// </summary>
    /// <param name="origin">玩家位置</param>
    /// <param name="target">目标位置</param>
    /// <param name="maxDistance">最大释放距离</param>
    /// <returns>有效释放位置</returns>
    private static Vector3 GetMaxSetPosition(Vector3 origin, Vector3 target, float maxDistance)
    {
        // 提取XZ平面坐标（忽略Y轴高度）
        var originXZ = new Vector2(origin.X, origin.Z);
        var targetXZ = new Vector2(target.X, target.Z);

        // 计算平方距离（避免开根号，提升性能）
        float distanceSquared = Vector2.DistanceSquared(originXZ, targetXZ);

        // 如果超出最大距离，计算朝向目标的最大距离位置
        if (distanceSquared > maxDistance * maxDistance)
        {
            // 归一化方向向量（避免零向量）
            Vector2 direction = targetXZ - originXZ;
            if (direction.LengthSquared() < float.Epsilon)
                direction = Vector2.Zero; // 兜底：无方向时设为零向量
            else
                direction = Vector2.Normalize(direction);

            // 计算XZ平面的最大有效位置
            targetXZ = originXZ + (direction * maxDistance);

            // 保留目标的Y轴高度，返回新位置
            return new Vector3(targetXZ.X, target.Y, targetXZ.Y);
        }

        // 未超出距离，直接返回目标位置
        return target;
    }

	public class 霰弹枪 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29404u);
		public void DrawExternal(Vector2 size, bool isActive) => SpellHelper.DrawSpellInfo(new Spell(29404u, Core.Me), size, isActive);
		public int Check()
		{
			var currentTarget = Core.Me.GetCurrTarget();
			if (currentTarget == null)
			{
				return -1;
			}
			if (currentTarget.DistanceToPlayer() > 12)
			{
				return -3;
			}
			if(!29404u.GetSpell().IsReadyWithCanCast())
			{
				return -2;
			}
			return 0;
		} 
		public void Run()
		{
			EnsureNextSlot();
			var target = Core.Me.GetCurrTarget();
			if (target != null)
				AI.Instance.BattleData.NextSlot.Add(new Spell(29404u, target));
		}
	}
	public class 画家LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 39215u);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(39215u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			EnsureNextSlot();
			if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() >= 3500)
			{
				AI.Instance.BattleData.NextSlot.Add(new Spell(39215u, Core.Me));
			}
		}

		private static Vector3 Get画家LBPosition(Vector3 origin, Vector3 target, float maxDistance)
		{
			// 提取 XZ 平面坐标（忽略 Y 轴高度）
			var originXZ = new Vector2(origin.X, origin.Z);
			var targetXZ = new Vector2(target.X, target.Z);

			// 计算平方距离（避免开根号，提升性能）
			float distanceSquared = Vector2.DistanceSquared(originXZ, targetXZ);

			// 如果超出最大距离，计算朝向目标的最大距离位置
			if (distanceSquared > maxDistance * maxDistance)
			{
				// 归一化方向向量（避免零向量）
				Vector2 direction = targetXZ - originXZ;
				if (direction.LengthSquared() < float.Epsilon)
					direction = Vector2.Zero; // 兜底：无方向时设为零向量
				else
					direction = Vector2.Normalize(direction);

				// 计算 XZ 平面的最大有效位置
				targetXZ = originXZ + (direction * maxDistance);

				// 保留目标的 Y 轴高度，返回新位置
				return new Vector3(targetXZ.X, target.Y, targetXZ.Y);
			}

			// 未超出距离，直接返回目标位置
			return target;
		}
	}
	public class 绝枪LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29130u);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29130u, Core.Me), size, isActive);

		public int Check()
		{
			if (Core.Me.LimitBreakCurrentValue() < 2000)
				return -1;
			return 0;
		}

		public void Run()
		{
			EnsureNextSlot();
			if (!Core.Me.HasLocalPlayerAura(4094u) & Core.Me.InCombat())
			{
				AI.Instance.BattleData.NextSlot.Add(new Spell(29130u, Core.Me));
			}
		}
	}
	public class 武士LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29537u);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29537u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			EnsureNextSlot();
			if (!PVPHelper.CanActive() && Core.Me.InCombat() && Core.Me.LimitBreakCurrentValue() > 3500)
			{
				if (PvPSAMSettings.Instance.多斩模式)
				{
					// 多斩模式下只取一次目标，保证同一轮判断/播报/施法一致。
					var target = PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数);
					if (PvPSAMSettings.Instance.斩铁调试)
					{
						LogHelper.Print($"尝试斩铁目标：{target}");
					}
					AI.Instance.BattleData.NextSlot.Add(new Spell(29537u, target));
					Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {target.Name} 释放 斩铁剑", 1, 1500);
				}
				else
				{
					var target = PVPTargetHelper.TargetSelector.Get斩铁目标();
					if (PvPSAMSettings.Instance.斩铁调试)
					{
						LogHelper.Print($"尝试斩铁目标：{target}");
					}
					AI.Instance.BattleData.NextSlot.Add(new Spell(29537u, target));
					Core.Resolve<MemApiChatMessage>().Toast2($"正在尝试对 {target.Name} 释放 斩铁剑", 1, 1500);
				}

			}
		}
	}
	
	public class 诗人LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29401u);

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29401u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			EnsureNextSlot();
			if (Core.Me.InCombat()&&Core.Me.LimitBreakCurrentValue()>=4000)
				AI.Instance.BattleData.NextSlot.Add(new Spell(29401, Core.Me));
		}
	}
	public class 抗死 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => 绘制状态切换图标(size, 3245u, 29697u, 29698u);

		public void DrawExternal(Vector2 size, bool isActive) => 绘制状态切换外显(size, isActive, 3245u, 29697u, 29698u);
		
		public int Check()
		{
			if (!29697u.IsUnlockWithCDCheck()&&!29698u.IsUnlockWithCDCheck())
				return -1;
			return 1;
		} 

		public void Run()
		{
			释放状态切换自施法(3245u, 29697u, 29698u);
		}
	}
	public class 黑白魔元切换 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => 绘制状态切换图标(size, 3245u, 29702u, 29703u);

		public void DrawExternal(Vector2 size, bool isActive) => 绘制状态切换外显(size, isActive, 3245u, 29702u, 29703u);
		
		public int Check()
		{
			return 0;
		} 

		public void Run()
		{
			释放状态切换自施法(3245u, 29702u, 29703u);
		}
	}
	public class 赤魔LB : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 41498u);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(41498u, Core.Me.GetCurrTarget() ?? Core.Me), size, isActive);

		public int Check()
		{
			if (Core.Me.InCombat() & Core.Me.LimitBreakCurrentValue() < 3000)
				return -1;
			return 1;
		} 

		public void Run()
		{
			EnsureNextSlot();
			if (Core.Me.InCombat()&Core.Me.LimitBreakCurrentValue()>=3000)
				if(PvPRDMSettings.Instance.南天自己)
					AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, Core.Me));
				else
				{
					var currentTarget = Core.Me.GetCurrTarget();
					if (currentTarget == null)
					{
						return;
					}
					AI.Instance.BattleData.NextSlot.Add(new Spell(41498u, currentTarget));
				}
		}
	}
	public class 后射 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29399u);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29399u, Core.Me), size, isActive);

		public int Check()
		{
			if (!29399u.GetSpell().IsReadyWithCanCast()) return -2;
			return 1;
		}
		public void Run()
		{
			EnsureNextSlot();
			var target = Core.Me.GetCurrTarget();
			if (target != null)
				AI.Instance.BattleData.NextSlot.Add(new Spell(29399, target));
		}
	}
	public class 后跳 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29494u);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29494u, Core.Me), size, isActive);

		public int Check()
		{
			if (!29494u.GetSpell().IsReadyWithCanCast()) return -2;
			return 1;
		}
		public void Run()
		{
			Core.Resolve<MemApiMove>().SetRot(PVPHelper.GetCameraRotation反向());
			Core.Resolve<MemApiSpell>().Cast(29494u, PVPHelper.向量位移反向(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
		}
	}
	public class 速涂 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 39210u);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(39210u, Core.Me), size, isActive);

		public int Check()
		{
			if (!39210u.GetSpell().IsReadyWithCanCast()) return -2;
			return 1;
		}
		public void Run()
		{
			Core.Resolve<MemApiMove>().SetRot(PVPHelper.GetCameraRotation());
			Core.Resolve<MemApiSpell>().Cast(39210u, PVPHelper.向量位移(Core.Me.Position, PVPHelper.GetCameraRotation(), 15));
		}
	}
	public class 以太步 : IHotkeyResolver
	{
		public void Draw(Vector2 size) => DrawActionIcon(size, 29660u);
		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(29660u, Core.Me), size, isActive);

		public int Check()
		{
			if (!29660u.GetSpell().IsReadyWithCanCast()) return -2;
			return 1;
		}
		public void Run()
		{
			EnsureNextSlot();
			var target = Core.Me.GetCurrTarget();
			if (target != null)
				AI.Instance.BattleData.NextSlot.Add(new Spell(29660u, target));
		}
	}
	public class 喵 : IHotkeyResolver
	{
		public void Draw(Vector2 size)
		{
		}

		public void DrawExternal(Vector2 size, bool isActive) =>
			SpellHelper.DrawSpellInfo(new Spell(39215u, Core.Me), size, isActive);

		public int Check() => 0;

		public void Run()
		{
			
		}
	}
}
