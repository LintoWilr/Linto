using System.Numerics;
using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Define.HotKey;
using Common.GUI;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;
using ImGuiScene;

namespace Linto.JobView;

public class HotkeyWindow
{
	private Dictionary<string, object> Setting;

	private Dictionary<string, HotkeyControl> HotkeyDict = new Dictionary<string, HotkeyControl>();

	public List<string> ActiveList = new List<string>();

	private Keys keyInput;

	private ModifierKey keyModifierInput;

	private string[] comboKeyStr = new string[4] { "None", "Ctrl", "Shift", "Alt" };

	private List<string> HotkeyNameList
	{
		get
		{
			Setting.TryAdd("HotkeyNameList", new List<string>());
			return (List<string>)Setting["HotkeyNameList"];
		}
	}

	private Dictionary<string, int> Hotkey
	{
		get
		{
			Setting.TryAdd("Hotkey", new Dictionary<string, int>());
			return (Dictionary<string, int>)Setting["Hotkey"];
		}
	}

	private Dictionary<string, int> HotkeyModifier
	{
		get
		{
			Setting.TryAdd("HotkeyModifier", new Dictionary<string, int>());
			return (Dictionary<string, int>)Setting["HotkeyModifier"];
		}
	}

	private List<string> HotkeyUnVisibleList
	{
		get
		{
			Setting.TryAdd("HotkeyUnVisibleList", new List<string>());
			return (List<string>)Setting["HotkeyUnVisibleList"];
		}
	}

	public int HotkeyLineCount
	{
		get
		{
			Setting.TryAdd("HotkeyLineCount", Style.DefaultHotkeyLineCount);
			return Convert.ToInt32(Setting["HotkeyLineCount"]);
		}
		set
		{
			if (Setting.ContainsKey("HotkeyLineCount"))
			{
				Setting["HotkeyLineCount"] = value;
			}
			else
			{
				Setting.TryAdd("HotkeyLineCount", value);
			}
		}
	}

	public float HotkeyScale
	{
		get
		{
			Setting.TryAdd("HotkeyScale", Style.DefaultHotkeyScale);
			return Convert.ToSingle(Setting["HotkeyScale"]);
		}
		set
		{
			if (Setting.ContainsKey("HotkeyScale"))
			{
				Setting["HotkeyScale"] = value;
			}
			else
			{
				Setting.TryAdd("HotkeyScale", value);
			}
		}
	}

	public bool LockWindow
	{
		get
		{
			Setting.TryAdd("LockWindow", false);
			return Convert.ToBoolean(Setting["LockWindow"]);
		}
		set
		{
			if (Setting.ContainsKey("LockWindow"))
			{
				Setting["LockWindow"] = value;
			}
			else
			{
				Setting.TryAdd("LockWindow", false);
			}
		}
	}

	public HotkeyWindow(ref Dictionary<string, object> setting)
	{
		Setting = setting;
	}

	public void AddHotkey(string name, IHotkeySlotResolver slot)
	{
		if (!HotkeyDict.ContainsKey(name))
		{
			HotkeyControl hotkey = new HotkeyControl(name);
			HotkeyDict.Add(name, hotkey);
			HotkeyDict[name].Slot = slot;
			HotkeyDict[name].img = slot.ImgPath;
			HotkeyDict[name].useMo = slot.UseMoTarget;
			if (!HotkeyNameList.Contains(name))
			{
				HotkeyNameList.Add(name);
			}
		}
	}

	public void SetHotkeyToolTip(string toolTip)
	{
		HotkeyDict[HotkeyDict.Keys.ToArray()[^1]].ToolTip = toolTip;
	}

	public string[] GetHotkeyArray()
	{
		return HotkeyDict.Keys.ToArray();
	}

	public void SetHotkey(string name)
	{
		if (!ActiveList.Contains(name) && HotkeyDict.TryGetValue(name, out HotkeyControl value) && SpellHelper.IsReady(value.spell))
		{
			ActiveList.Add(name);
		}
	}

	public void DrawHotkeyWindow()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ActiveList.Count; i++)
		{
			string hotkeyName = ActiveList[i];
			if (HotkeyDict.ContainsKey(hotkeyName))
			{
				Spell spell = HotkeyDict[hotkeyName].spell;
				if (SpellHistoryHelper.RecentlyUsed(spell, 100))
				{
					ActiveList.Remove(hotkeyName);
					HotkeyDict[hotkeyName].Reset();
				}
			}
		}
		Style.SetQtStyle();
		ImGuiWindowFlags flag = (ImGuiWindowFlags)(LockWindow ? 47 : 43);
		ImGui.Begin("###Hotkey_Window", flag);
		int num = 0;
		float scale = HotkeyScale * Style.OverlayScale;
		Vector2 childSize = new Vector2(80f, 80f) * scale;
		IDalamudTextureWrap textureWrap_skill2 ;
		IDalamudTextureWrap textureWrap_skill;
		IDalamudTextureWrap textureWrap_active2 ;
		IDalamudTextureWrap textureWrap_normal3 ;
		IDalamudTextureWrap textureWrap_black ;
		IDalamudTextureWrap textureWrap_active ;
		IDalamudTextureWrap textureWrap_normal2 ;
		IDalamudTextureWrap textureWrap_normal;
		for (int j = 0; j < HotkeyNameList.Count; j++)
		{
			string hotkeyName2 = HotkeyNameList[j];
			if (!HotkeyDict.ContainsKey(hotkeyName2) || HotkeyUnVisibleList.Contains(hotkeyName2))
			{
				continue;
			}
			HotkeyControl hotkey = HotkeyDict[hotkeyName2];
			keyInput = (Keys)(Hotkey.TryGetValue(hotkey.Name, out var value) ? value : 0);
			keyModifierInput = (ModifierKey)(HotkeyModifier.TryGetValue(hotkey.Name, out var value2) ? value2 : 0);
			ImGui.BeginChild($"###Hotkey_HotkeyControl{j}", childSize, false, (ImGuiWindowFlags)43);
			ImGui.SetCursorPos(new Vector2(6f, 6f) * scale);
			if (hotkey.img != "")
			{
				if (Core.Get<IMemApiIcon>().TryGetTexture(hotkey.img, out textureWrap_skill2, true))
				{
					ImGui.Image(textureWrap_skill2.ImGuiHandle, new Vector2(67f, 67f) * scale);
				}
			}
			else if (Core.Get<IMemApiIcon>().GetActionTexture(hotkey.spell.Id, out textureWrap_skill, true))
			{
				ImGui.Image(textureWrap_skill.ImGuiHandle, new Vector2(67f, 67f) * scale);
			}
			if (ImGui.IsItemHovered((ImGuiHoveredFlags)128))
			{
				ImGui.BeginTooltip();
				string mo = (hotkey.useMo ? "/mo" : "");
				ImGui.Text(hotkeyName2 + "  " + mo);
				if (hotkey.ToolTip != "")
				{
					ImGui.Text(hotkey.ToolTip ?? "");
				}
				ImGui.EndTooltip();
				if (ImGui.IsMouseClicked((ImGuiMouseButton)0))
				{
					if (!ActiveList.Contains(hotkey.Name))
					{
						if (SpellHelper.IsReady(hotkey.spell) || !hotkey.spell.IsAbility())
						{
							ActiveList.Add(hotkey.Name);
						}
					}
					else
					{
						ActiveList.Remove(hotkey.Name);
					}
				}
			}
			if (hotkey.spell.IsAbility())
			{
				if (SpellHelper.IsReady(hotkey.spell))
				{
					if (ActiveList.Contains(hotkeyName2))
					{
						ImGui.SetCursorPos(new Vector2(0f, 0f));
						if (Core.Get<IMemApiIcon>().TryGetTexture("Resources\\Spells\\Icon\\activeaction.png", out textureWrap_active2, true))
						{
							ImGui.Image(textureWrap_active2.ImGuiHandle, new Vector2(80f, 80f) * scale);
						}
					}
					else
					{
						ImGui.SetCursorPos(new Vector2(-1f, -1f) * scale);
						if (Core.Get<IMemApiIcon>().TryGetTexture("Resources\\Spells\\Icon\\iconframe.png", out textureWrap_normal3, true))
						{
							ImGui.Image(textureWrap_normal3.ImGuiHandle, new Vector2(82f, 82f) * scale);
						}
					}
				}
				else
				{
					ImGui.SetCursorPos(new Vector2(-1f, -1f) * scale);
					if (Core.Get<IMemApiIcon>().TryGetTexture("Resources\\Spells\\Icon\\icona_frame_disabled.png", out textureWrap_black, true))
					{
						ImGui.Image(textureWrap_black.ImGuiHandle, new Vector2(82f, 82f) * scale);
					}
				}
			}
			else if (ActiveList.Contains(hotkeyName2))
			{
				ImGui.SetCursorPos(new Vector2(0f, 0f));
				if (Core.Get<IMemApiIcon>().TryGetTexture("Resources\\Spells\\Icon\\activeaction.png", out textureWrap_active, true))
				{
					ImGui.Image(textureWrap_active.ImGuiHandle, new Vector2(80f, 80f) * scale);
				}
			}
			else
			{
				ImGui.SetCursorPos(new Vector2(-1f, -1f) * scale);
				if (Core.Get<IMemApiIcon>().TryGetTexture("Resources\\Spells\\Icon\\iconframe.png", out textureWrap_normal2, true))
				{
					ImGui.Image(textureWrap_normal2.ImGuiHandle, new Vector2(82f, 82f) * scale);
				}
			}
			ImGui.SetCursorPos(new Vector2(0f, 0f));
			Spell spell2 = hotkey.spell;
			double cd = spell2.Cooldown.TotalSeconds;
			if (hotkey.Slot.SpellId == 0)
			{
				PotionSetting setting = SettingMgr.GetSetting<PotionSetting>();
				CharacterAgent me = Core.Me;
				uint potionRawId = setting.GetPotionId(me.CurrentJob);
				cd = Core.Get<IMemApiInventory>().GetItemCoolDown(potionRawId).TotalSeconds;
			}
			if (cd > 0.0 && (int)(cd * 1000.0) + 1 != Core.Get<IMemApiSpell>().GetGCDDuration(false) - Core.Get<IMemApiSpell>().GetElapsedGCD())
			{
				cd = (int)cd;
				if (hotkey.Slot.SpellId != 0)
				{
					cd %= (double)((int)spell2.RecastTime.TotalSeconds / spell2.MaxCharges);
				}
				ImGui.SetCursorPos(new Vector2(4f, 82f * scale - 17f));
				ImGui.Text($"{cd + 1.0}");
			}
			if (spell2.MaxCharges > 1)
			{
				int charge = (int)spell2.Charges;
				ImGui.SetCursorPos(new Vector2(48f * scale, 51f * scale));
				if (Core.Get<IMemApiIcon>().TryGetTexture($"Resources\\Spells\\Icon\\Charge{charge}.png", out textureWrap_normal, true))
				{
					ImGui.Image(textureWrap_normal.ImGuiHandle, new Vector2(29f, 26f) * scale);
				}
			}
			ImGui.EndChild();
			if (num % HotkeyLineCount != HotkeyLineCount - 1)
			{
				ImGui.SameLine();
			}
			num++;
			RunSlot();
		}
		double line = Math.Ceiling((float)num / (float)HotkeyLineCount);
		int row = ((num < HotkeyLineCount) ? num : HotkeyLineCount);
		float width = (float)((row - 1) * 6 + 16) + childSize.X * (float)row;
		double height = (line - 1.0) * 6.0 + 16.0 + (double)childSize.Y * line;
		ImGui.SetWindowSize(new Vector2(width, (int)height));
		ImGui.End();
		Style.EndQtStyle();
	}

	public void HotkeySettingView()
	{
		ImGui.TextDisabled("   *左键拖动改变hotkey顺序，右键点击显示更多");
		int num = default(int);
		ModifierKey val2 = default(ModifierKey);
		for (int i = 0; i < HotkeyNameList.Count; i++)
		{
			string item = HotkeyNameList[i];
			string visible = ((!HotkeyUnVisibleList.Contains(item)) ? "显示" : "隐藏");
			if (visible == "隐藏")
			{
				ImGui.PushStyleColor((ImGuiCol)0, new Vector4(0.6f, 0f, 0f, 1f));
			}
			ImGui.Selectable("   " + visible + "        " + item);
			if (visible == "隐藏")
			{
				ImGui.PopStyleColor(1);
			}
			keyInput = (Keys)(Hotkey.TryGetValue(item, out var value) ? value : 0);
			keyModifierInput = (ModifierKey)(HotkeyModifier.TryGetValue(item, out var value2) ? value2 : 0);
			if (ImGui.IsItemActive() && !ImGui.IsItemHovered())
			{
				int n_next = i + ((!(ImGui.GetMouseDragDelta((ImGuiMouseButton)0).Y < 0f)) ? 1 : (-1));
				if (n_next < 0 || n_next >= HotkeyNameList.Count)
				{
					continue;
				}
				HotkeyNameList[i] = HotkeyNameList[n_next];
				HotkeyNameList[n_next] = item;
				ImGui.ResetMouseDragDelta();
			}
			ImGui.PushStyleVar((ImGuiStyleVar)9, 1f);
			ImGui.PushStyleColor((ImGuiCol)5, new Vector4(0.2f, 0.2f, 0.2f, 1f));
			if (ImGuiHelper.IsRightMouseClicked())
			{
				ImGui.OpenPopup($"###hotkeyPopup{i}");
			}
			if (ImGui.BeginPopup($"###hotkeyPopup{i}"))
			{
				bool vis = !HotkeyUnVisibleList.Contains(item);
				if (ImGui.Checkbox("显示", ref vis))
				{
					if (!vis)
					{
						HotkeyUnVisibleList.Add(item);
					}
					else
					{
						HotkeyUnVisibleList.Remove(item);
					}
				}
				ImGui.EndPopup();
			}
			ImGui.PopStyleColor(1);
			ImGui.PopStyleVar(1);
		}
	}

	private void RunSlot()
	{
		if (ActiveList.Count == 0)
		{
			return;
		}
		foreach (string hotkeyName in HotkeyDict.Keys)
		{
			if (!ActiveList.Contains(hotkeyName))
			{
				continue;
			}
			Spell spell = HotkeyDict[hotkeyName].spell;
			if (SpellHistoryHelper.RecentlyUsed(spell, 500))
			{
				continue;
			}
			HotkeyControl hotkey = HotkeyDict[hotkeyName];
			if (hotkey.IsNextSlot)
			{
				continue;
			}
			int ret = hotkey.Slot.Check(hotkey);
			if (ret >= 0)
			{
				Slot slot = new Slot(2500);
				hotkey.Slot.Build(slot, hotkey);
				if (SpellHelper.CanCast(spell) == 0)
				{
					AI.Instance.BattleData.NextSlot = slot;
					hotkey.IsNextSlot = true;
				}
			}
		}
	}

	public void RunHotkey()
	{
		foreach (HotkeyControl hotkey in HotkeyDict.Values)
		{
			int value;
			Keys keyInput = (Keys)(Hotkey.TryGetValue(hotkey.Name, out value) ? value : 0);
			int value2;
			ModifierKey keyModifierInput = (ModifierKey)(HotkeyModifier.TryGetValue(hotkey.Name, out value2) ? value2 : 0);
			if ((int)keyInput == 0)
			{
				continue;
			}
			IMemApiHotKey a = Core.Get<IMemApiHotKey>();
			if ((a.CheckModifierClicked() && (int)keyModifierInput == 0) || !a.CheckState(keyModifierInput, keyInput))
			{
				continue;
			}
			if (!ActiveList.Contains(hotkey.Name))
			{
				if (SpellHelper.IsReady(hotkey.spell) || !hotkey.spell.IsAbility())
				{
					ActiveList.Add(hotkey.Name);
					CharacterAgent? moTarget = Core.Get<IMemApiParty>().Mo();
					if (moTarget.HasValue)
					{
						hotkey.MoTarget = moTarget.Value;
					}
				}
			}
			else
			{
				ActiveList.Remove(hotkey.Name);
			}
		}
	}
}
