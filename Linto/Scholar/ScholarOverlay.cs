using System.Numerics;

using CombatRoutine;
using CombatRoutine.Setting;
using CombatRoutine.TriggerModel;
using CombatRoutine.View;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace Linto.Scholar;

public class ScholarOverlay
{
	private bool isHorizontal;

	public void DrawGeneral(JobViewWindow jobViewWindow)
	{
		if (ImGui.CollapsingHeader("起手"))
		{
			ImGui.Text("起手设置");
			string opener = "0";
			if (SCHSettings.Instance.opener == 0)
			{
				opener = "保留即刻";
			}
			if (SCHSettings.Instance.opener == 1)
			{
				opener = "使用即刻";
			}
			if (SCHSettings.Instance.opener == 2)
			{
				opener = "以太起手";
			}
			if (ImGui.BeginCombo("", opener))
			{
				if (ImGui.Selectable("保留即刻"))
				{
					SCHSettings.Instance.opener = 0;
					SCHSettings.Instance.Save();
				}
				if (ImGui.Selectable("使用即刻"))
				{
					SCHSettings.Instance.opener = 1;
					SCHSettings.Instance.Save();
				}
				if (ImGui.Selectable("以太起手"))
				{
					SCHSettings.Instance.opener = 2;
					SCHSettings.Instance.Save();
				}
				ImGui.EndCombo();
			}
		}
		ImGui.Text("当前豆子保留层数：" + SCHBattleData.Instance.AethorflowReserve);
		if (ImGui.Button("清除队列"))
		{
			SCHBattleData.Instance.SpellQueueoGCD.Clear();
			SCHBattleData.Instance.SpellQueueGCD.Clear();
			AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
			AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
		}
		if (ImGui.Button("清一个GCD"))
		{
			SCHBattleData.Instance.SpellQueueGCD.Dequeue();
			AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
		}
		if (ImGui.Button("清一个能力技"))
		{
			SCHBattleData.Instance.SpellQueueoGCD.Dequeue();
			AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
		}
		ImGui.Text("-------能力技-------");
		if (SCHBattleData.Instance.SpellQueueoGCD.Count > 0)
		{
			foreach (Spell spell2 in SCHBattleData.Instance.SpellQueueoGCD)
			{
				ImGui.Text(spell2.Name);
			}
		}
		ImGui.Text("-------GCD-------");
		if (SCHBattleData.Instance.SpellQueueGCD.Count > 0)
		{
			foreach (Spell spell in SCHBattleData.Instance.SpellQueueGCD)
			{
				ImGui.Text(spell.Name);
			}
		}
		ImGui.EndChild();
		ImGui.EndTabItem();
	}
			
	public static class SCHQt
	{
		/// 获取指定名称qt的bool值
		public static bool GetQt(string qtName)
		{
			return MonkRotationEntry.JobViewWindow.GetQt(qtName);
		}

		/// 反转指定qt的值
		/// <returns>成功返回true，否则返回false</returns>
		public static bool ReverseQt(string qtName)
		{
			return MonkRotationEntry.JobViewWindow.ReverseQt(qtName);
		}

		/// 设置指定qt的值
		/// <returns>成功返回true，否则返回false</returns>
		public static bool SetQt(string qtName, bool qtValue)
		{
			return MonkRotationEntry.JobViewWindow.SetQt(qtName, qtValue);
		}

		/// 重置所有qt为默认值
		public static void Reset()
		{
			MonkRotationEntry.JobViewWindow.Reset();
		}

		/// 给指定qt设置新的默认值
		public static void NewDefault(string qtName, bool newDefault)
		{
			MonkRotationEntry.JobViewWindow.NewDefault(qtName, newDefault);
		}

		/// 将当前所有Qt状态记录为新的默认值，
		/// 通常用于战斗重置后qt还原到倒计时时间点的状态
		public static void SetDefaultFromNow()
		{
			MonkRotationEntry.JobViewWindow.SetDefaultFromNow();
		}

		/// 返回包含当前所有qt名字的数组
		public static string[] GetQtArray()
		{
			return MonkRotationEntry.JobViewWindow.GetQtArray();
		}
	}
}
