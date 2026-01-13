using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;
using Linto.Wilr.DarkKnight;

namespace Linto.Wilr;

public static class Data
{
	public static class Buff
	{
		public const uint 盾姿 = 743u;

		public const uint 嗜血 = 742u;

		public const uint 血乱 = 1972u;

		public const uint 腐蚀大地 = 749u;
	}

	public const uint 噬魂斩 = 3632u;

	public const uint 吸收斩 = 3623u;

	public const uint 重斩 = 3617u;

	public const uint 血溅 = 7392u;

	public const uint 伤残 = 3624u;

	public const uint 刚魂 = 16468u;

	public const uint 释放 = 3621u;

	public const uint 寂灭 = 7391u;

	public const uint 暗黑波动 = 16466u;

	public const uint 暗影波动 = 16469u;

	public const uint 海胆 = 3641u;

	public const uint 嗜血 = 3625u;

	public const uint 血乱 = 7390u;

	public const uint 弗雷 = 16472u;

	public const uint 跳斩 = 3640u;

	public const uint 暗黑锋 = 16467u;

	public const uint 暗影锋 = 16470u;

	public const uint 精雕怒斩 = 3643u;

	public const uint 腐蚀大地 = 3639u;

	public const uint 腐秽黑暗 = 25755u;

	public const uint 暗影使者 = 25757u;

	public const uint 疾跑 = 3u;

	public const uint 挑衅 = 7533u;

	public const uint 退避 = 7537u;

	public const uint 盾姿 = 3629u;

	public const uint 黑盾 = 7393u;

	public const uint 铁壁 = 7531u;

	public const uint 雪仇 = 7535u;

	public const uint 暗黑布道 = 16471u;

	public const uint 弃明投暗 = 3634u;

	public const uint 献奉 = 25754u;

	public const uint 暗影墙 = 3636u;

	public const uint 活死人 = 3638u;

	public static double 爆发120 => SpellHelper.GetSpell(16472u).Cooldown.TotalMilliseconds;

	public static double 爆发60
	{
		get
		{
			if (!(爆发120 > 60000.0))
			{
				return 爆发120;
			}
			return 爆发120 - 60000.0;
		}
	}

	public static GeneralSettings Setting => SettingMgr.GetSetting<GeneralSettings>();

	public static int 攻击距离
	{
		get
		{
			if (!DkSetting.Far4)
			{
				return Setting.AttackRange;
			}
			return Setting.AttackRange + 1;
		}
	}

	public static PvPDRKSettings DkSetting => PvPDRKSettings.Instance;

	public static int OGCDLock => PvPDRKSettings.Instance.OGCDLock;

	public static int Get暗血()
	{
		return Core.Get<IMemApiDarkKnight>().Blood();
	}

	public static int Get暗黑剩余时间()
	{
		return Core.Get<IMemApiDarkKnight>().DarksideTimeRemaining();
	}

	public static int Get弗雷剩余时间()
	{
		return Core.Get<IMemApiDarkKnight>().ShadowTimeRemaining();
	}

	public static bool 是否延后对齐120()
	{
		if (SpellHelper.GetSpell(16472u).Cooldown.TotalMilliseconds < 20000.0)
		{
			return SpellHelper.GetSpell(16472u).Cooldown.TotalMilliseconds > 1.0;
		}
		return false;
	}

	public static uint GetHP()
	{
		CharacterAgent me = Core.Me;
		return me.CurrentHealth;
	}

	public static float GetHPPercent()
	{
	
		CharacterAgent me = Core.Me;
		return me.CurrentHealthPercent;
	}

	public static ulong GetHPMax()
	{
	
		CharacterAgent me = Core.Me;
		return me.MaxHealth;
	}

	public static uint GetMP()
	{
	
		CharacterAgent me = Core.Me;
		return me.CurrentMana;
	}

	public static CharacterAgent GetTarget()
	{
	
		return UnitHelper.GetCurrTarget(Core.Me);
	}

	public static float Get距离(CharacterAgent target)
	{
		CharacterAgent me = Core.Me;
		return me.DistanceMelee(target);
	}

	public static float Get到目标距离()
	{
	
		return Get距离(GetTarget());
	}

	public static int GetGCD剩余时间()
	{
		return AI.Instance.GetGCDCooldown();
	}

	public static bool HasBuff(uint buff)
	{
		
		CharacterAgent me = Core.Me;
		return me.HasMyAura(buff);
	}

	public static int Buff时间(uint buff)
	{
		CharacterAgent me = Core.Me;
		return (int)me.GetBuffTimespanLeft(buff).TotalMilliseconds;
	}
}
