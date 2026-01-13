using System.Numerics;
using Common.GUI;
using Common.Helper;
using ImGuiNET;

namespace Linto.JobView;

public class QtWindow
{
	private class QtControl
	{
		public readonly string Name;

		public bool QtValue;

		public bool QtValueDefault;

		public string ToolTip = "";

		public QtControl(string name, bool qtValueDefault)
		{
			Name = name;
			QtValueDefault = qtValueDefault;
			Reset();
		}

		public void Reset()
		{
			QtValue = QtValueDefault;
		}
	}

	private Dictionary<string, object> Setting;

	private Dictionary<string, QtControl> QtDict = new Dictionary<string, QtControl>();

	public List<string> QtNameList
	{
		get
		{
			Setting.TryAdd("QtNameList", new List<string>());
			return (List<string>)Setting["QtNameList"];
		}
		set
		{
			if (!Setting.ContainsKey("QtNameList"))
			{
				Setting.TryAdd("QtNameList", new List<string>());
			}
			Setting["QtNameList"] = value;
		}
	}

	private List<string> QtUnVisibleList
	{
		get
		{
			Setting.TryAdd("QtUnVisibleList", new List<string>());
			return (List<string>)Setting["QtUnVisibleList"];
		}
		set
		{
			if (!Setting.ContainsKey("QtUnVisibleList"))
			{
				Setting.TryAdd("QtUnVisibleList", new List<string>());
			}
			Setting["QtUnVisibleList"] = value;
		}
	}

	public int QtLineCount
	{
		get
		{
			Setting.TryAdd("QtLineCount", Style.DefaultQtLineCount);
			return Convert.ToInt32(Setting["QtLineCount"]);
		}
		set
		{
			if (Setting.ContainsKey("QtLineCount"))
			{
				Setting["QtLineCount"] = value;
			}
			else
			{
				Setting.TryAdd("QtLineCount", value);
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

	public QtWindow(ref Dictionary<string, object> setting)
	{
		Setting = setting;
	}

	public void AddQt(string name, bool qtValueDefault)
	{
		if (!QtDict.ContainsKey(name))
		{
			QtControl qt = new QtControl(name, qtValueDefault);
			QtDict.Add(name, qt);
			if (!QtNameList.Contains(name))
			{
				QtNameList.Add(name);
			}
		}
	}

	public void SetQtToolTip(string toolTip)
	{
		QtDict[QtDict.Keys.ToArray()[^1]].ToolTip = toolTip;
	}

	public bool GetQt(string qtName)
	{
		if (!QtDict.ContainsKey(qtName))
		{
			return false;
		}
		return QtDict[qtName].QtValue;
	}

	public bool SetQt(string qtName, bool qtValue)
	{
		if (!QtDict.ContainsKey(qtName))
		{
			return false;
		}
		QtDict[qtName].QtValue = qtValue;
		return true;
	}

	public bool ReverseQt(string qtName)
	{
		if (!QtDict.ContainsKey(qtName))
		{
			return false;
		}
		QtDict[qtName].QtValue = !QtDict[qtName].QtValue;
		return true;
	}

	public void Reset()
	{
		foreach (QtControl qt in QtDict.Select<KeyValuePair<string, QtControl>, QtControl>((KeyValuePair<string, QtControl> _qt) => _qt.Value))
		{
			qt.Reset();
			LogHelper.Info("重置所有qt为默认值");
		}
	}

	public void NewDefault(string qtName, bool newDefault)
	{
		if (QtDict.ContainsKey(qtName))
		{
			QtControl qt = QtDict[qtName];
			qt.QtValueDefault = newDefault;
			LogHelper.Info($"改变qt \"{qt.Name}\" 默认值为 {qt.QtValueDefault}");
		}
	}

	public void SetDefaultFromNow()
	{
		foreach (QtControl qt in QtDict.Select<KeyValuePair<string, QtControl>, QtControl>((KeyValuePair<string, QtControl> _qt) => _qt.Value))
		{
			if (qt.QtValueDefault != qt.QtValue)
			{
				qt.QtValueDefault = qt.QtValue;
				LogHelper.Info($"改变qt \"{qt.Name}\" 默认值为 {qt.QtValueDefault}");
			}
		}
	}

	public string[] GetQtArray()
	{
		return QtDict.Keys.ToArray();
	}

	public void DrawQtWindow()
	{
		
		Style.SetQtStyle();
		ImGuiWindowFlags flag = (ImGuiWindowFlags)(LockWindow ? 47 : 43);
		ImGui.Begin("###Qt_Window{Yoyokity}", flag);
		int i = 0;
		foreach (string qtName in QtNameList)
		{
			if (QtDict.ContainsKey(qtName) && !QtUnVisibleList.Contains(qtName))
			{
				QtControl qt = QtDict[qtName];
				if (QtSwitchButton(qtName, ref qt.QtValue))
				{
					LogHelper.Info($"改变qt \"{qt.Name}\" 状态为 {qt.QtValue}");
				}
				if (qt.ToolTip != "")
				{
					ImGuiHelper.SetHoverTooltip(qt.ToolTip);
				}
				if (i % QtLineCount != QtLineCount - 1)
				{
					ImGui.SameLine();
				}
				i++;
			}
		}
		double line = Math.Ceiling((float)i / (float)QtLineCount);
		int row = ((i < QtLineCount) ? i : QtLineCount);
		float width = (float)((row - 1) * 6 + 16) + Style.QtButtonSize.X * (float)row;
		double height = (line - 1.0) * 6.0 + 16.0 + (double)Style.QtButtonSize.Y * line;
		ImGui.SetWindowSize(new Vector2(width, (int)height));
		ImGui.End();
		Style.EndQtStyle();
	}

	public void QtSettingView()
	{
		ImGui.TextDisabled("   *左键拖动改变qt顺序，右键点击qt隐藏");
		for (int i = 0; i < QtNameList.Count; i++)
		{
			string item = QtNameList[i];
			string visible = ((!QtUnVisibleList.Contains(item)) ? "显示" : "隐藏");
			if (visible == "隐藏")
			{
				ImGui.PushStyleColor((ImGuiCol)0, new Vector4(0.6f, 0f, 0f, 1f));
			}
			ImGui.Selectable("   " + visible + "        " + item);
			if (visible == "隐藏")
			{
				ImGui.PopStyleColor(1);
			}
			if (ImGui.IsItemActive() && !ImGui.IsItemHovered())
			{
				int n_next = i + ((!(ImGui.GetMouseDragDelta((ImGuiMouseButton)0).Y < 0f)) ? 1 : (-1));
				if (n_next < 0 || n_next >= QtNameList.Count)
				{
					continue;
				}
				QtNameList[i] = QtNameList[n_next];
				QtNameList[n_next] = item;
				ImGui.ResetMouseDragDelta();
			}
			if (ImGuiHelper.IsRightMouseClicked())
			{
				if (!QtUnVisibleList.Contains(item))
				{
					QtUnVisibleList.Add(item);
				}
				else
				{
					QtUnVisibleList.Remove(item);
				}
			}
		}
	}

	private static bool QtSwitchButton(string label, ref bool buttonValue)
	{
		Vector2 size = Style.QtButtonSize;
		bool ret = false;
		if (buttonValue)
		{
			ImGui.PushStyleColor((ImGuiCol)21, Style.MainColor);
			ImGui.PushStyleColor((ImGuiCol)23, Style.MainColor);
			ImGui.PushStyleColor((ImGuiCol)22, Style.MainColor);
			if (ImGui.Button(label, size))
			{
				ret = true;
				buttonValue = !buttonValue;
			}
			ImGui.PopStyleColor(3);
		}
		else
		{
			ImGui.PushStyleColor((ImGuiCol)21, Style.ColorFalse);
			ImGui.PushStyleColor((ImGuiCol)23, Style.ColorFalse);
			ImGui.PushStyleColor((ImGuiCol)22, Style.ColorFalse);
			if (ImGui.Button(label, size))
			{
				ret = true;
				buttonValue = !buttonValue;
			}
			ImGui.PopStyleColor(3);
		}
		return ret;
	}
}
