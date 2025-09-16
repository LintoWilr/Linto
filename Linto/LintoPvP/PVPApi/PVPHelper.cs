using System.Numerics;
using System.Windows.Forms;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons.DalamudServices;
using ImGuiNET;
using AEAssist.Verify;
using Dalamud.Game.ClientState.Conditions;
using System.Runtime.CompilerServices;
using Dalamud.Game.Text.SeStringHandling;
using ECommons.GameFunctions;
using FFXIVClientStructs.FFXIV.Client.UI;
using Linto.LintoPvP.PVPApi.PVPApi.Target;
using Linto.LintoPvP.RDM;
using CSFramework = FFXIVClientStructs.FFXIV.Client.System.Framework.Framework;    
namespace Linto.LintoPvP.PVPApi;

public class PVPHelper
{ 
	//抄来的
	public static Vector3 向量位移(Vector3 position, float facingRadians, float distance) 
	{
		// 计算x-z平面上移动的距离分量  
		float dx = (float)(Math.Sin(facingRadians) * distance);
		float dz = (float)(Math.Cos(facingRadians) * distance);

		return new Vector3(position.X + dx, position.Y + 5, position.Z + dz); 
	}
	public static Vector3 向量位移反向(Vector3 position, float facingRadians, float distance) 
	{
		// 计算x-z平面上移动的距离分量（反向）
		float dx = (float)(Math.Sin(facingRadians) * (-distance));
		float dz = (float)(Math.Cos(facingRadians) * (-distance));

		return new Vector3(position.X + dx, position.Y + 5, position.Z + dz); 
	}

	private static unsafe RaptureAtkModule* RaptureAtkModule => CSFramework.Instance()->GetUIModule()->GetRaptureAtkModule();

	internal static unsafe float GetCameraRotation()
	{
		// Gives the camera rotation in deg between -180 and 180
		var cameraRotation = RaptureAtkModule->AtkModule.AtkArrayDataHolder.NumberArrays[24]->IntArray[3];

		// Transform the [-180,180] rotation to rad with same 0 as a GameObject rotation
		// There might be an easier way to do that, but geometry and I aren't friends
		var sign = Math.Sign(cameraRotation) == -1 ? -1 : 1;
		var rotation = (float)((Math.Abs(cameraRotation * (Math.PI / 180)) - Math.PI) * sign);

		return rotation;
	}
	internal static unsafe float GetCameraRotation反向()
	{
		// 获取当前相机旋转（以弧度表示）
		float rotation = GetCameraRotation();

		// 添加 π（180度）以获得反向旋转
		rotation += (float)Math.PI;

		// 归一化旋转值，使其保持在 [-π, π] 范围内
		if (rotation > Math.PI)
		{
			rotation -= 2 * (float)Math.PI;
		}
		else if (rotation < -Math.PI)
		{
			rotation += 2 * (float)Math.PI;
		}

		return rotation;
	}

	public static unsafe bool 视线阻挡(IBattleChara 目标角色)
	{
		return MemApiSpell.LineOfSightChecker.IsBlocked(Core.Me.GameObject(),(目标角色.GameObject()));
	}

	
	/// <summary>
	/// 判断是否需要用净化 有BUFF为True 无BUFF为False
	/// </summary>
	public static bool 净化判断()
	{
		if (Core.Me.HasCanDispel())
		{
			return true;
		}
		return false;
	}

	public static Spell 不等服务器Spell(uint id, IBattleChara? target)
	{
		var spell = (new Spell(id, target));
		spell.WaitServerAcq = false;
		return spell;
	}

	public static Spell 等服务器Spell(uint id, IBattleChara? target)
	{
		var spell = (new Spell(id, target));
		spell.WaitServerAcq = true;
		return spell;
	}

	/// <summary>
	/// 你的CID
	/// </summary>
	public static List<ulong> 通用码权限列表 = new List<ulong>
	{
		18014469511346939,
		18014469510423537, //兔团
		18014469510702542, //ayase
		18014469510525313, //林玖
		18014479510723122, //dc canumm
		18014469509612031, //844545513
		18014398555212021, //dc digua
		19014409512829860, //dc aria
		19014409517683706, //dc yokoyamamio
		18014479511064381, //dc 雷雷
		18014469509698926, //dc 雷雷2
		19014409515975551, //dc mga
		18014398549659316, //dc tempest
		19014409511786239, //dc arkstardust
		19014409515975799, //dc sibianjun
		18014479510257104, //dc qiaokelikeke
		19014409516898973, //dc 佐迪亚克
	};
	public static bool 通用码权限 => 通用码权限列表.Contains(Svc.ClientState.LocalContentId)||Core.Resolve<MemApiMap>().GetCurrTerrId()==250;
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
	public static bool 是否55() => RestrictedTerritoryIds.Contains(Core.Resolve<MemApiMap>().GetCurrTerrId());
	public static bool 高级码 => Share.VIP.Level != VIPLevel.Normal;
	public static bool check坐骑() => Svc.Condition[ConditionFlag.Mounted];
	/*public static Dictionary<uint, IBattleChara> GetList_all(float range = 50f)
	{
		if (!Core.Me.IsPvP())
		{
			return null;
		}
		Dictionary<uint, IBattleChara> List_all = TargetMgr.Instance.Units;
		Dictionary<uint, IBattleChara> dict = new Dictionary<uint, IBattleChara>();
		foreach (IBattleChara value in List_all.Values)
		{
			IBattleChara unit = value;
			if (!unit.IsDead && !(unit.DistanceToPlayer() > range))
			{
				dict.Add(unit.DataId, unit);
				break;
			}
		}
		return dict;
	}
	public static List<IBattleChara> GetList_LookatMe(IBattleChara target, float range = 50f)
	{
		if (!Core.Me.IsPvP())
		{
			return null;
		}
		List<IBattleChara> re = new List<IBattleChara>();
		Dictionary<uint, IBattleChara> all = GetList_all();
		foreach (IBattleChara value in all.Values)
		{
			IBattleChara one = value;
			float distance = one.Distance(Core.Me);
			if (!(distance > range))
			{
				IBattleChara currTarget = one;
				re.Add(one);
			}
		}
		return re;
	}*/
	/// <summary>
	/// 行动状态检测
	/// </summary>
	/// <param name="检查在不在屁威屁">1</param>
	/// <param name="检查有没有权限">2</param>
	/// <param name="检查是不是在吃药">3</param>
	/// <param name="检查是不是在用坐骑">4</param>
	/// <param name="检查是不是在用龟壳">5</param>
	/// <param name="检查是不是已经上了坐骑">6</param>
	/// <param name="检查是不是3秒内用过龟壳">7</param>
	public static bool CanActive()
	{
		uint 技能龟壳 = 29711u;
		//	if (!Enum.IsDefined(typeof(ZoneId), GetZoneId()))
		if (!Core.Me.IsPvP())
		{
			return false;
		}
		if (!通用码权限 && !高级码)
		{
			return false;
		}
		if (Core.Me.CastActionId == 29055)
		{
			return false;
		}
		if (Core.Me.CastActionId == 4)
		{
			return false;
		}
		if (Core.Me.HasAura(3054u))
		{
			return false;
		}
		if (check坐骑())
		{
			return false;
		}
		if (技能龟壳.RecentlyUsed(3000))
		{
			return false;
		}
		return true;
	}
	private static IBattleChara? Target;
	public static bool HasBuff(IBattleChara? BattleChara, uint buffId)
	{
		return BattleChara.HasAura(buffId, 0);
	}
	public static void 技能图标(uint id)
	{
		uint skillid = id;
		Vector2 size1 = new Vector2(60, 60);
		IDalamudTextureWrap? textureWrap;
		if (!Core.Resolve<MemApiIcon>().GetActionTexture(skillid, out textureWrap))
			return;
		ImGui.Image(textureWrap.ImGuiHandle, size1);
	}
	private static void s1()
	{
		ImGui.PushItemWidth(100);
		ImGui.InputInt($"米内有敌人时不用##{228}", ref PvPSettings.Instance.无目标坐骑范围);
		ImGui.PopItemWidth();
	}

	private static void s2()
	{
		ImGui.PushItemWidth(100);
		ImGui.InputInt($"米内最近目标##{229}", ref PvPSettings.Instance.自动选中自定义范围);
		ImGui.PopItemWidth();
	}
	private static void s3()
	{
		ImGui.PushItemWidth(100);
		ImGui.InputInt($"秒##{230}", ref PvPSettings.Instance.坐骑cd);
		ImGui.PopItemWidth();
	}
	public static void 通用设置配置()
	{
		ImGui.Text("共通配置");
		ImGui.PushItemWidth(100);
		// ImGui.Checkbox($"播报(玩具功能)##{888}", ref PvPSettings.Instance.播报);
		//
		// ImGui.Checkbox($"电棍鬼叫##{883}", ref PvPSettings.Instance.鬼叫);
		// PvPSettings.Instance.Volume = Math.Clamp(PvPSettings.Instance.Volume,1, 100);
		// ImGui.InputInt($"鬼叫音量##{884}", ref PvPSettings.Instance.Volume);
		// PvPSettings.Instance.语音cd = Math.Clamp(PvPSettings.Instance.语音cd,1, 100);
		// ImGui.InputInt($"冲刺鬼叫cd##{885}", ref PvPSettings.Instance.语音cd);
		//
		//ImGui.Checkbox($"无目标也喝热水##{233}", ref PvPSettings.Instance.脱战嗑药);
		if (ImGui.InputInt($"攻击判定增加(仅影响ACR判断,配合长臂猿使用)##{66}", ref PvPSettings.Instance.长臂猿, 1, 1))
		{
			PvPSettings.Instance.长臂猿 = Math.Clamp(PvPSettings.Instance.长臂猿, 0, 5);
		}
		// if (ImGui.InputInt($"自动冲刺间隔)##{1211}", ref PvPSettings.Instance.冲刺, 1, 1))
		// {
		// 	PvPSettings.Instance.冲刺 = Math.Clamp(PvPSettings.Instance.冲刺, 0, 99);
		// }
		ImGui.Text("自动选中默认排除:" +
		           "\n不死救赎3039 神圣领域1302 被保护2413 龟壳3054 地天1240");
		ImGui.Checkbox($"自动选中(只在PvP生效)##{666}", ref PvPSettings.Instance.自动选中);
		ImGui.SameLine();
		s2();
		ImGui.Checkbox($"只选中玩家(测试)##{1412}", ref PvPSettings.Instance.不选冰);
		//ImGui.Checkbox($"无目标挂机时冲刺(测试)##{232}", ref PvPSettings.Instance.无目标冲刺);
		//ImGui.SameLine();
		ImGui.Checkbox($"无目标时自动坐骑(默认陆行鸟)测试##{222}", ref PvPSettings.Instance.无目标坐骑);
		ImGui.Checkbox($"自动坐骑指定相应坐骑##{678}", ref PvPSettings.Instance.指定坐骑);
		if (PvPSettings.Instance.指定坐骑) 
		{
			ImGui.SameLine();
			if (ImGui.InputText("坐骑名字", ref PvPSettings.Instance.坐骑名, 16))
			{
				ImGui.Text($"设置的坐骑名字为: {PvPSettings.Instance.坐骑名}");
			}
			if (ImGui.Button("呼叫坐骑!"))
			{
				Core.Resolve<MemApiSendMessage>().SendMessage($"/mount {PvPSettings.Instance.坐骑名}");
			}
		}
		ImGui.Text($"自动坐骑在范围");
		ImGui.SameLine();
		s1();
		ImGui.Text($"自动坐骑尝试间隔");
		ImGui.SameLine();
		s3();
		ImGui.Checkbox($"!!技能自动对最近敌人释放!!##{212}", ref PvPSettings.Instance.技能自动选中);
		if (PvPSettings.Instance.技能自动选中)
		{
			ImGui.SameLine();
			ImGui.Checkbox($"!!选择技能范围内血量最低目标!!##{216}", ref PvPSettings.Instance.最合适目标);
		}
		ImGui.PopItemWidth();
		// if (ImGui.Button("播报todo"))
		// {
		// 	Core.Resolve<MemApiChatMessage>()
		// 		.Toast2(
		// 			$"当前区域: {Core.Resolve<MemApiMap>().ZoneName(Core.Resolve<MemApiMap>().GetCurrTerrId())} ", 1,
		// 			1500);
		// }
		// if (ImGui.Button("鬼叫todo"))
		// {
		// 	Voice.PlayVoice("崩破");
		// }
		PvPSettings.Instance.Save();
		//监控窗口();
		if (ImGui.CollapsingHeader("7.2更新进度"))
		{
			ImGui.Text("只更新了绝枪及其职能技能 其他职业待更新");
		}
		if (ImGui.CollapsingHeader("更新日志"))
		{
			ImGui.Text("7/1 自动选中只在pvp生效 修复机工野火目标");
			ImGui.Text("3/8 诗人光阴神提供自定义选择");
			ImGui.Text("3/2 更新了7.1 机工");
			ImGui.Text("2/26 更新了7.1 画蛇");
			ImGui.Text("2/25 提供了只选中玩家和坐骑间隔");
			ImGui.Text("2/20 添加了黑魔自定义配置");
			ImGui.Text("2/19 更新了7.1 黑诗侍赤龙");
			ImGui.Text("12/15 修复战场武士Lb狂按的问题");
			ImGui.Text("12/4 添加了指定坐骑");
			ImGui.Text("11/7 修复黑魔热震荡");
			ImGui.Text("11/2 添加赤魔");
			ImGui.Text("11/1 机工现在会强制锁野火目标了");
			ImGui.Text("11/1 修复冲刺会打断龟壳的问题");
		}
	}
	
	/*public static IBattleChara Get绝枪绿吸取目标()
	{
		IBattleChara[] 玩家25米内敌人 = TargetMgr.Instance.EnemysIn25.Values.ToArray();
		if (PartyHelper.CastableHealers.Count != 0)
		{
			return PartyHelper.CastableHealers[0];
		}
		foreach (var member in 玩家25米内敌人)
		{
			if (member.IsHealer()&&member.IsTargetable)//&&member.InLos
				return member;
		}
		return Core.Me;
	}
	public static IBattleChara Get绝枪红吸取目标()
	{
		IBattleChara[] 玩家25米内敌人 = TargetMgr.Instance.EnemysIn25.Values.ToArray();
		if (PartyHelper.CastableDps.Count != 0)
		{
			return PartyHelper.CastableDps[0];
		}
		foreach (var member in 玩家25米内敌人)
		{
			if (member.IsDps()&&member.IsTargetable)
				return member;
		}
		return Core.Me;
	}*/
	

	public static void 权限获取()
	{
		ulong cid = Svc.ClientState.LocalContentId;
		string CID = cid.ToString();
		ImGui.Text($"当前的码等级：[{Share.VIP.Level}]");
		if (Share.VIP.Level == VIPLevel.Normal && 高级码)
		{
			ImGui.Text($"仅狼狱可用 战场无权限");
		}
		if (!通用码权限 && !高级码)
		{
			ImGui.TextColored(new Vector4(255f / 255f, 0f / 255f, 0f / 255f, 0.8f), "无权限");
			ImGui.SameLine();
			if (ImGui.Button("复制CID到剪贴板"))
			{
				Clipboard.SetText(CID);
				LogHelper.Print("已复制CID到剪贴板");
			}
		}
		if (通用码权限 || 高级码)
		{
			ImGui.TextColored(new Vector4(42f / 255f, 215f / 255f, 57f / 255f, 0.8f), "已解锁");
			
		}
	}

	public static void 技能配置(uint 技能图标id, string 技能名字, string 描述文字, ref bool 切换配置, int id)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{描述文字}: {切换配置}");
		if (ImGui.Button($"切换##{id}"))
		{
			切换配置 = !切换配置;
		}

		ImGui.Columns(1);
	}

	public static void 技能配置2(uint 技能图标id, string 技能名字, string 描述文字, ref bool 切换配置, int id)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{描述文字}:");
		ImGui.Checkbox($"##{id}", ref 切换配置);
		ImGui.Columns(1);
	}

	public static void 技能配置3(uint 技能图标id, string 技能名字, string 描述文字, ref int 数值, int 幅度, int 快速幅度, int id)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{描述文字}:");
		ImGui.InputInt($"##{id}", ref 数值, 幅度, 快速幅度);
		ImGui.Columns(1);
	}

	public static void 技能配置4(uint 技能图标id, string 技能名字, string 数值描述, string 描述文字, ref bool 切换配置, ref int 数值, int 幅度,
		int 快速幅度, int id)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{描述文字}:");
		ImGui.Checkbox($"##{id}", ref 切换配置);
		ImGui.Text($"{数值描述}:");
		ImGui.InputInt($"##{id}+1", ref 数值, 幅度, 快速幅度);
		ImGui.Columns(1);
	}

	public static void 技能配置5(uint 技能图标id, string 技能名字, string IntDescription, ref float value, float min, float max,
		int id)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{IntDescription}:");
		ImGui.SliderFloat($"##{id}", ref value, min, max);
		ImGui.Columns(1); 
	}

	public static void 技能解释(uint 技能图标id, string 技能名字, string 描述文字)
	{
		ImGui.Separator();
		ImGui.Columns(2, null, false);
		ImGui.SetColumnWidth(0, 70);
		技能图标(技能图标id);
		ImGui.NextColumn();
		ImGui.SetColumnWidth(1, 150);
		ImGui.Text(技能名字);
		ImGui.Text($"{描述文字}:");
		ImGui.Columns(1);
	}
	
	public static Spell? 通用技能释放Check(uint skillid, int 距离)
	{
		if (skillid==29402u||skillid==29403u||skillid==29408u||skillid==29406u||skillid==29407u||skillid==29404u)
		{
			if (PVPTargetHelper.TargetSelector.Get野火目标() != null)
			{
				return 等服务器Spell(skillid,
					PVPTargetHelper.TargetSelector.Get野火目标());
			}
		}

		if (skillid == 29405u)
		{
			if (PvPSettings.Instance.技能自动选中)
			{
				if (PvPSettings.Instance.最合适目标 &&
				    (PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,29405u) != null &&
				     PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,29405u) != Core.Me))
				{
					return 等服务器Spell(skillid,
						PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,29405u));
				}

				if ((PVPTargetHelper.TargetSelector.Get最近目标() != null &&
				     PVPTargetHelper.TargetSelector.Get最近目标() != Core.Me))
				{
					return 等服务器Spell(skillid, PVPTargetHelper.TargetSelector.Get最近目标());
				}
			}
		}

		if (PvPSettings.Instance.技能自动选中)
		{
			if (PvPSettings.Instance.最合适目标 &&
			    (PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,skillid) != null &&
			     PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,skillid) != Core.Me))
			{
				return 等服务器Spell(skillid,
					PVPTargetHelper.TargetSelector.Get最合适目标(距离 + PvPSettings.Instance.长臂猿,skillid));
			}

			if ((PVPTargetHelper.TargetSelector.Get最近目标() != null &&
			     PVPTargetHelper.TargetSelector.Get最近目标() != Core.Me))
			{
				return 等服务器Spell(skillid, PVPTargetHelper.TargetSelector.Get最近目标());
			}
		}
		if (Core.Me.GetCurrTarget() != null && Core.Me.GetCurrTarget() != Core.Me)
		{
			return 等服务器Spell(skillid, Core.Me.GetCurrTarget());
		}
		return null;
	}

	public static void 通用技能释放(Slot slot, uint skillid, int 距离)
	{
		slot.Add(通用技能释放Check(skillid, 距离));
	}

	public static bool 通用距离检查(int 距离)
	{
		if (PvPSettings.Instance.技能自动选中)
		{
			if (PVPTargetHelper.TargetSelector.Get最近目标().DistanceToPlayer() > 距离 + PvPSettings.Instance.长臂猿 ||
			    PVPTargetHelper.TargetSelector.Get最近目标() == null ||
			    PVPTargetHelper.TargetSelector.Get最近目标() == Core.Me)
			{
				return true;
			}
		}
		else if (!PvPSettings.Instance.技能自动选中 &&
		         Core.Me.GetCurrTarget().DistanceToPlayer() > 距离 + PvPSettings.Instance.长臂猿 ||
		         Core.Me.GetCurrTarget() == Core.Me || Core.Me.GetCurrTarget() == null)
		{
			return true;
		}

		return false;
	}
	public static bool 固定距离检查(int 距离)
	{
		if (PvPSettings.Instance.技能自动选中)
		{
			if (PVPTargetHelper.TargetSelector.Get最近目标().DistanceToPlayer() > 距离 ||
			    PVPTargetHelper.TargetSelector.Get最近目标() == null ||
			    PVPTargetHelper.TargetSelector.Get最近目标() == Core.Me)
			{
				return true;
			}
		}
		else if (!PvPSettings.Instance.技能自动选中 &&
		         Core.Me.GetCurrTarget().DistanceToPlayer() > 距离||
		         Core.Me.GetCurrTarget() == Core.Me || Core.Me.GetCurrTarget() == null)
		{
			return true;
		}

		return false;
	}
	public static void 配置(JobViewWindow jobViewWindow)
	{
		通用设置配置();
	}

	public static void 更新日志(JobViewWindow jobViewWindow)
	{

	}

	// private static DateTime _lastAuraCheck = DateTime.MinValue;
	// private static readonly TimeSpan AuraCheckInterval = TimeSpan.FromSeconds(5);

	/*public static void 无目标冲刺()
	{
		if (Core.Me.IsMoving())
		{
			var slot = new Slot();
			if (!Core.Me.HasAura(1342u) &&
			    GCDHelper.GetGCDCooldown() == 0 && !check坐骑() && !Core.Me.IsCasting)
			{
				slot.Add(new Spell(29057u, Core.Me));
			}
		}
	}*/
	public static void PvP调试窗口()
	{
		if(Svc.ClientState.LocalContentId==18014469511346939)
		{
			ImGui.Begin("调试窗口");
			ImGui.Text($"gcd:{GCDHelper.GetGCDCooldown()}");
			ImGui.Text($"CastActionId:{Core.Me.CastActionId}");
			ImGui.Text($"是否55:{PVPHelper.是否55()}");
			IBattleChara?最近目标 = PVPTargetHelper.TargetSelector.Get最近目标();
			SeString 最近目标名称 = 最近目标?.Name ?? "无";
			ImGui.Text($"视线阻挡: {视线阻挡(Core.Me.GetCurrTarget())}");
			ImGui.Text($"最近目标: {最近目标名称}");
			IBattleChara?最合适目标25米 = PVPTargetHelper.TargetSelector.Get最合适目标(25,1);
			SeString 最合适目标25米名称 = 最合适目标25米?.Name ?? "无";
			ImGui.Text($"最合适25米目标: {最合适目标25米名称}");
		//	ImGui.Text($"技能状态变化:{Core.Resolve<MemApiSpell>().CheckActionChange(PCTSkillID.技能1冰结之蓝青)}");
			ImGui.Text($"自己：{Core.Me.Name},{Core.Me.DataId},{Core.Me.Position}");
			ImGui.Text($"坐骑状态：{Svc.Condition[(ConditionFlag)4]}");
			ImGui.Text($"血量百分比：{Core.Me.CurrentHpPercent()}");
			ImGui.Text($"盾值百分比：{Core.Me.ShieldPercentage / 100f}");
			ImGui.Text($"血量百分比：{Core.Me.CurrentHpPercent() + Core.Me.ShieldPercentage / 100f <= 1.0f}");
			IBattleChara? 目标 = Core.Me.GetCurrTarget();
			SeString 目标1 = Core.Me.GetCurrTarget().Name ?? "无";
			ImGui.Text(
				$"目标：{目标1},{Core.Me.GetCurrTarget().DataId},{Core.Me.GetCurrTarget().Position}");
			ImGui.Text($"是否移动：{MoveHelper.IsMoving()}");
			ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
			ImGui.Text($"25米内敌方人数：{TargetHelper.GetNearbyEnemyCount(Core.Me, 1, PvPRDMSettings.Instance.护盾距离)}");
			ImGui.Text($"20米内小队人数：{PartyHelper.CastableAlliesWithin20.Count}");
			ImGui.Text($"目标5米内人数：{TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 5)}");
			ImGui.Text($"LB槽当前数值：{Core.Me.LimitBreakCurrentValue()}");
			ImGui.Text($"上个技能：{Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
			ImGui.Text($"上个GCD：{Core.Resolve<MemApiSpellCastSuccess>().LastGcd}");
			ImGui.Text($"上个能力技：{Core.Resolve<MemApiSpellCastSuccess>().LastAbility}");
			ImGui.Text($"上个连击技能：{Core.Resolve<MemApiSpell>().GetLastComboSpellId()})");
			IBattleChara?Get野火目标 = PVPTargetHelper.TargetSelector.Get野火目标();
			SeString 野火目标 = Get野火目标?.Name ?? "无";
			ImGui.Text($"t野火目标：{野火目标})");
			ImGui.Text($"技能变化：{Core.Resolve<MemApiSpell>().CheckActionChange(29102)})");
			ImGui.Text($"烈牙cd：{29102u.GetSpell().IsReadyWithCanCast()})");
			ImGui.Text($"烈牙2cd：{29103u.GetSpell().IsReadyWithCanCast()})");
			ImGui.Text($"自身技能：{Core.Me.HasLocalPlayerAura(2282)})");
			ImGui.Text($"IsUnlockWithCDCheck：{29649u.IsUnlockWithCDCheck()})");
			ImGui.Text($"IsReadyWithCanCast：{29649u.GetSpell().IsReadyWithCanCast()})");
			if (ImGui.Button("null"))
			{
				Svc.Targets.Target = null;
			}
			if (ImGui.Button("最远"))
			{
				Svc.Targets.Target = PVPTargetHelper.TargetSelector.Get最远目标();
			}
			if (ImGui.Button("1"))
			{
				Core.Resolve<MemApiMove>().SetRot(GetCameraRotation反向());
			}
			if (ImGui.Button("21"))
			{
				Core.Resolve<MemApiMove>().SetRot(GetCameraRotation());
			}
			ImGui.Text($"？：{Core.Me.HasAura(3212)}");
			if (ImGui.Button("21"))
			{
				
			}

			ImGui.End();
		}
		else
		{
			ImGui.Text("你不需要用到调试");
		}
	}
	public static void 进入ACR()
	{
	//	if(!PvPSettings.Instance.鬼叫) return;
		if (!(通用码权限 || 高级码))
		{
			Core.Resolve<MemApiChatMessage>()
				.Toast2(
					$"没有权限！", 1,
					1500);
		}
		else
		{ 
		//	Voice.PlayVoiceRandom();
		}
	}
	private static DateTime 冲刺time = DateTime.MinValue;
	private static DateTime 崩破time = DateTime.MinValue;
	public static void 战斗状态()
	{
	//	if(PvPSettings.Instance.监控)PVPHelper.监控窗口();
	//	if(!PvPSettings.Instance.鬼叫) return;
		
		/*DateTime now = DateTime.Now;
		if(Core.Me.HasAura(3202u,3000))
		{
			if ((now - 崩破time).TotalSeconds < 4)
			{
				return;
			}
			Voice.PlayVoice("崩破");
			崩破time = now;
		}
		if(29057u.RecentlyUsed(1000))
		{
			if ((now - 冲刺time).TotalSeconds < PvPSettings.Instance.语音cd)
			{
				return;
			}
			Voice.PlayVoice("冲刺");
			冲刺time = now;
		}*/
		
	}
	public class SprintTracker
	{
		private DateTime? _lastSprintTime;  // 使用可空的 DateTime 类型来记录最后一次冲刺的时间
		private bool _isSprinting;          // 内部标识是否处于冲刺状态
		// 判断是否处于冲刺状态，通过 Core.Me.HasAura(1342) 来判断是否拥有冲刺光环
		public bool IsSprinting
		{
			get
			{
				// 自动同步当前冲刺状态
				return Core.Me.HasAura(1342);
			}
		}
		// 每当检测冲刺时，如果处于冲刺状态且没有记录过时间，则记录当前时间
		public void CheckAndTrackSprint()
		{
			if (IsSprinting && !_isSprinting)
			{
				// 只有从非冲刺状态变为冲刺状态时才记录时间
				_lastSprintTime = DateTime.Now;
			}
        
			// 更新冲刺状态
			_isSprinting = IsSprinting;
		}
		// 计算冲刺间隔时间
		public bool CheckSprint()
		{
			if (_isSprinting && _lastSprintTime.HasValue)
			{
				// 如果距离上次冲刺小于设定秒，返回true
				if ((DateTime.Now - _lastSprintTime.Value).TotalSeconds < PvPSettings.Instance.冲刺)
				{
					return true;
				}
			}
			//不然就返回false
			return false;
		}
	}
	private static bool 警报 = true;

	public static void 监控(JobViewWindow jobViewWindow)
	{
		if (通用码权限 || 高级码)
		{
			ImGui.Checkbox($"启用监控窗口##{28}", ref PvPSettings.Instance.监控);
			ImGui.SliderFloat("监控图片宽", ref PvPSettings.Instance.图片宽1, 0, 1000);
			ImGui.SliderFloat("监控图片高", ref PvPSettings.Instance.图片高1, 0, 1000);
			ImGui.Checkbox($"监控布局紧凑##{29}", ref PvPSettings.Instance.紧凑);
			ImGui.Checkbox($"##{35}", ref PvPSettings.Instance.名字);
			ImGui.SameLine();
			ImGui.Checkbox($"##{36}", ref PvPSettings.Instance.血量);
			ImGui.SameLine();
			ImGui.Checkbox($"##{37}", ref PvPSettings.Instance.距离);
			ImGui.SameLine();
			ImGui.Text("显示名字|血量百分比|距离");
			ImGui.Text("紧凑数量");
			ImGui.SameLine();
			ImGui.InputInt($"##{32}", ref PvPSettings.Instance.紧凑数量);
			ImGui.Text("监控最大人数");
			ImGui.SameLine();
			ImGui.InputInt($"##{33}", ref PvPSettings.Instance.监控数量);
			ImGui.Checkbox($"监控警报##{30}", ref PvPSettings.Instance.警报);
			ImGui.Text("警报数量人数");
			ImGui.SameLine();
			ImGui.InputInt($"##{34}", ref PvPSettings.Instance.警报数量);
			ImGui.Checkbox($"DevList##{31}", ref PvPSettings.Instance.窗口开关);
			if (PvPSettings.Instance.窗口开关) DrawUnitList();
			监控窗口();
			PvPSettings.Instance.Save();
			List<IBattleChara> targetMe;

			void Draw图片UnitList()
			{
				IDalamudTextureWrap textureJob;
				// 遍历字典中的所有单位
				foreach (var kvp in targetMe)
				{
					IBattleChara unit = kvp;
					uint job = (uint)unit.CurrentJob();
					// 跳过无效单位（可选）
					if (unit == null || unit.IsDead || !unit.IsPlayerCamp()) continue;
					if (Core.Resolve<MemApiIcon>().TryGetTexture($"Resources\\jobs\\{job}.png", out textureJob))
					{
						// 如果成功获取到职业图标，显示该图标
						ImGui.Image(textureJob.ImGuiHandle, new Vector2(50f, 50f));
					}

					ImGui.SameLine();
					ImGui.Text($"{unit.Name}");
					ImGui.SameLine();
					ImGui.Text($"{unit.DistanceToPlayer()}");
					// 添加分隔线（可选）
					ImGui.Separator();
				}

				// 重置列状态
				ImGui.Columns(1);
			}

			void DrawUnitList()
			{
				// 添加表头（分列显示）
				ImGui.Columns(7); // 分为7列
				ImGui.Text("ID");
				ImGui.NextColumn();
				ImGui.Text("名称");
				ImGui.NextColumn();
				ImGui.Text("职业");
				ImGui.NextColumn();
				ImGui.Text("血量百分比");
				ImGui.NextColumn();
				ImGui.Text("距离");
				ImGui.NextColumn();
				ImGui.Text("选中目标");
				ImGui.NextColumn();
				ImGui.Text("选中目标ID");
				ImGui.NextColumn();
				ImGui.Separator();

				// 遍历字典中的所有单位
				foreach (var kvp in TargetMgr.Instance.Units)
				{
					uint unitId = kvp.Key;
					IBattleChara unit = kvp.Value;

					// 跳过无效单位（可选）
					if (unit == null || unit.IsDead) continue;

					// 第一列：单位ID
					ImGui.Text($"{unitId}");
					ImGui.NextColumn();

					// 第二列：单位名称
					ImGui.Text($"{unit.Name}");
					ImGui.NextColumn();

					// 第三列：职业（假设 CurrentJob 是枚举）
					ImGui.Text($"{unit.CurrentJob()}");
					ImGui.NextColumn();

					// 第四列
					ImGui.Text($"{unit.CurrentHpPercent()}");
					ImGui.NextColumn();

					// 第五列：距离玩家距离
					ImGui.Text($"{unit.DistanceToPlayer():F1}m");
					ImGui.NextColumn();
					ImGui.Text($"{unit.GetCurrTarget()?.Name}");
					ImGui.NextColumn();
					ImGui.Text($"{unit.GetCurrTarget()?.GameObjectId}");
					ImGui.NextColumn();
					// 添加分隔线（可选）
					ImGui.Separator();
				}

				// 重置列状态
				ImGui.Columns(1);
			}
			
			// if (HasBuff(Core.Me, 895u) && !HasBuff(Core.Me, 1342u) && IsMoving())
			// {
			//     Core.Get<IMemApiSpell>().Cast(29057u, Core.Me);
			// }
		}
		else ImGui.Text("请获取权限");
	}

	public static void 监控窗口()
	{
		if (PvPSettings.Instance.监控)
		{
			ImGui.SetNextWindowSize(new Vector2(
				PvPSettings.Instance.宽1, 
				PvPSettings.Instance.高1
			), ImGuiCond.FirstUseEver); 
			
			// 开始绘制一个窗口，窗口标识为"###targetMe_Window"，使用了(ImGuiWindowFlags)43这个标志
			ImGui.Begin("###targetMe_Window", ImGuiWindowFlags.None);

			// 获取看着玩家的敌方目标
			List<IBattleChara> targetMe = PVPTargetHelper.Get看着目标的人(Group.敌人, Core.Me);

			// 创建一个默认的字符串格式化处理器
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler =
				new DefaultInterpolatedStringHandler(28, 1);

			// 生成一个图片路径，根据目标数量（最多显示4个敌人）
			defaultInterpolatedStringHandler.AppendLiteral("Resources\\Images\\Number\\");
			defaultInterpolatedStringHandler.AppendFormatted((targetMe.Count <= 4) ? ((object)targetMe.Count) : "4+");
			defaultInterpolatedStringHandler.AppendLiteral(".png");

			// 转换格式化后的路径为字符串
			string imgPath = defaultInterpolatedStringHandler.ToStringAndClear();

			// 尝试获取目标数量图标
			IDalamudTextureWrap texture;
			if (Core.Resolve<MemApiIcon>().TryGetTexture(imgPath, out texture))
			{
				// 如果成功获取到图标，显示它
				ImGui.Text("    ");
				ImGui.SameLine();
				ImGui.Image(texture.ImGuiHandle, new Vector2(PvPSettings.Instance.图片宽1, PvPSettings.Instance.图片高1));
				ImGui.Columns();
			}

			// 如果开启了警报设置并且目标数量大于0，则显示警报窗口
			if (PvPSettings.Instance.警报 && targetMe.Count >= PvPSettings.Instance.警报数量)
			{
				Core.Resolve<MemApiChatMessage>().Toast2("好像有很多人在看你耶!", 1, 3000); // 显示警报文本
			}

			// 如果有敌人目标（目标数量大于0）
			if (targetMe.Count > 0)
			{
				int i = 1;
				IDalamudTextureWrap textureJob;

				// 遍历每一个敌方目标
				foreach (IBattleChara v in targetMe)
				{
					if (i > PvPSettings.Instance.监控数量)
					{
						break;
					}

					// 获取当前敌人的职业
					uint job = (uint)v.CurrentJob();
					// 根据职业获取对应的图标
					if (Core.Resolve<MemApiIcon>().TryGetTexture($"Resources\\jobs\\{job}.png", out textureJob))
					{
						if (PvPSettings.Instance.紧凑 && i % PvPSettings.Instance.紧凑数量 == 0 && i != 1)
						{
							ImGui.NewLine();
						}

						// 如果成功获取到职业图标，显示该图标
						ImGui.Image(textureJob.ImGuiHandle, new Vector2(50f, 50f));
						if (PvPSettings.Instance.名字)
						{
							ImGui.SameLine();
							ImGui.Text($"{v.Name}");
						}

						if (PvPSettings.Instance.血量)
						{
							ImGui.SameLine();
							ImGui.Text($"HP百分比:{v.CurrentHpPercent() * 100f}");
						}

						if (PvPSettings.Instance.距离)
						{
							ImGui.SameLine();
							ImGui.Text($"距离:{v.DistanceToPlayer():F1}m");
						}
					}

					{
						if (PvPSettings.Instance.紧凑 && i % PvPSettings.Instance.紧凑数量 != 0)
						{
							ImGui.SameLine(0f, 5f); // 设置图标间隔
						}

						i++; // 增加计数器
					}
				}
			}
			ImGui.End();
		}
	}
}




