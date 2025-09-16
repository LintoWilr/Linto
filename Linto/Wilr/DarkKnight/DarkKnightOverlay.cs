using System.Numerics;
using CombatRoutine;
using Common;
using Common.Define;
using Common.Define.HotKey;
using Common.Helper;
using ImGuiNET;
using Linto.JobView;
using Linto.LintoPvP.PVPApi;

namespace Linto.Wilr.DarkKnight;

public class DarkKnightOverlay
{
	private static Keys key = (Keys)107;

	public void Draw()
	{
		JobWindow.Instance.SetMainStyle();
		JobWindow.Instance.MainControlView(ref Core.CombatRun, ref PlayerOptions.Instance.Stop);
		ImGui.Dummy(new Vector2(0f, 10f));
		if (ImGui.BeginTabBar("###tab"))
		{
			if (ImGui.BeginTabItem("通常"))
			{
				ImGui.BeginChild("###tab1", new Vector2(0f, 0f));
				if (ImGui.CollapsingHeader("技能设置"))
				{
					/*ImGui.InputInt("能力技插入", ref SettingMgr.GetSetting<GeneralSettings>().MaxAbilityTimesInGcd);
					ImGui.DragInt("能力技僵直", ref Data.DkSetting.OGCDLock, 10f, 200, 1000);
					ImGuiHelper.SetHoverTooltip("这个不是hack功能哦！只是确保大部分能力技不会导致卡GCD。\n通常什么都不开设置600，三插设置400左右。");
					ImGui.InputInt("攻击距离", ref SettingMgr.GetSetting<GeneralSettings>().AttackRange);
					ImGui.Checkbox("启用TTK", ref Data.DkSetting.useTTK);
					ImGuiHelper.SetHoverTooltip("智能检测boss击杀速度，\n当距离boss死亡还有15秒时清空所有资源");
					ImGui.Checkbox("爆发药时间内倾泻所有资源", ref Data.DkSetting.PushInPotion);
					ImGuiHelper.SetHoverTooltip("打空蓝量和跳斩");
					ImGui.Checkbox("攻击距离1米容错", ref Data.DkSetting.Far4);
					ImGuiHelper.SetHoverTooltip("比如你在拉怪时距离会忽近忽远，在4米内时即使微卡gcd也正常打不使用伤残");
					ImGui.Checkbox("超过攻击范围时不要使用能力技", ref Data.DkSetting.NoOgcdFar);
					ImGuiHelper.SetHoverTooltip("影响跳斩、血乱、嗜血，弗雷不受此影响");*/
				}
				ImGui.EndChild();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Qt"))
			{
				ImGui.BeginChild("###tab2", new Vector2(0f, 0f));
				ImGui.Checkbox("战斗结束qt自动重置回战斗前状态", ref Data.DkSetting.AutoReset);
				JobWindow.Instance.QtSettingView();
				ImGui.EndChild();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("风格"))
			{
				ImGui.BeginChild("###tab4", new Vector2(0f, 0f));
				JobWindow.Instance.ChangeStyleView();
				ImGui.EndChild();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Dev"))
			{
				ImGui.BeginChild("###tab5", new Vector2(0f, 0f));
				CharacterAgent? anotherTank = PartyHelper.GetAnotherTank(Core.Me);
				object obj;
				CharacterAgent val;
				if (!anotherTank.HasValue)
				{
					obj = null;
				}
				else
				{
					val = anotherTank.GetValueOrDefault();
					obj = val.Name;
				}

				ImGui.Text($"GCD剩余时间：{Data.GetGCD剩余时间()} ms");
				if (ImGui.TreeNode("DEV"))
				{
					CharacterAgent Target = UnitHelper.GetCurrTarget(Core.Me);
					val = Core.Me;
					Vector3 me = (val).Pos;
					Vector3 target = (Target).Pos;
					if (ImGui.TreeNode("pos"))
					{
						ImGui.Text($"me：({Math.Round(me.X, 1)},{Math.Round(me.Y, 1)},{Math.Round(me.Z, 1)})");
						ImGui.Text(
							$"target：({Math.Round(target.X, 1)},{Math.Round(target.Y, 1)},{Math.Round(target.Z, 1)})");
						ImGui.TreePop();
					}

					ImGui.Text("target：" + (Target).Name);
					ImGui.Text($"设定距离：{Data.攻击距离}");
					ImGui.Text($"到目标范围圈距离：{Data.Get到目标距离()}");
					ImGui.Text($"上个技能：{Core.Get<IMemApiSpellCastSucces>().LastSpell}");
					ImGui.Text($"上个GCD：{Core.Get<IMemApiSpellCastSucces>().LastGcd}");
					ImGui.Text($"上个能力技：{Core.Get<IMemApiSpellCastSucces>().LastAbility}");
					ImGui.Text($"CurrSlot {AI.Instance.BattleData.CurrSlot}");
					ImGui.Text($"CurrSlotStack.Count {AI.Instance.BattleData.CurrSlotStack.Count}");
					ImGui.TreePop();
					ImGui.Text($"小队人数：{PartyHelper.CastableParty.Count}");
					ImGui.Text($"小队坦克数量：{PartyHelper.CastableTanks.Count}");
					ImGui.EndChild();
					ImGui.EndTabItem();
				}
			}

			JobWindow.Instance.DrawQtWindow();
			JobWindow.Instance.DrawHotkeyWindow();
			JobWindow.Instance.EndMainStyle();
			ImGui.EndTabBar();
		}
	}

	public void Draw2()
	{
		ImGui.Begin("DA DA DA DA DA");
		ImGui.SetWindowSize(new Vector2(320f, 400f) * Style.OverlayScale);
		PVPHelper.获取权限();
		ImGui.End();
	}
}
