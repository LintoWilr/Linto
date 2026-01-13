using System.Numerics;
using Common.GUI;
using ImGuiNET;

namespace Linto.JobView;

public class MainWindow
{
	private bool smallWindow;

	private QtWindow qtWindow;

	private HotkeyWindow hotkeyWindow;

	public MainWindow(QtWindow _qtWindow, HotkeyWindow _hotkeyWindow)
	{
		qtWindow = _qtWindow;
		hotkeyWindow = _hotkeyWindow;
	}

	public int MainControlView(ref bool buttonValue, ref bool stopButton, Action save)
	{
		string label = (stopButton ? "停手" : "启动");
		Vector2 size = new Vector2(100f, 45f);
		int ret = 0;
		if (buttonValue)
		{
			if (!stopButton)
			{
				ImGui.PushStyleColor((ImGuiCol)5, new Vector4(1f, 1f, 1f, 0.5f));
				ImGui.PushStyleColor((ImGuiCol)21, Style.MainColor);
				ImGui.PushStyleColor((ImGuiCol)23, Style.ColorActive);
				ImGui.PushStyleColor((ImGuiCol)22, Style.ColorHovered);
			}
			else
			{
				Vector4 colorHovered = Style.ColorHovered;
				colorHovered.W = 1f;
				ImGui.PushStyleColor((ImGuiCol)5, colorHovered);
				colorHovered = Style.ColorActive;
				colorHovered.W = 1f;
				ImGui.PushStyleColor((ImGuiCol)0, colorHovered);
				ImGui.PushStyleColor((ImGuiCol)21, Style.ColorFalse);
				ImGui.PushStyleColor((ImGuiCol)23, Style.ColorFalse);
				ImGui.PushStyleColor((ImGuiCol)22, Style.ColorFalse);
			}
			if (ImGui.Button(label, size))
			{
				ret = 1;
				if (!stopButton)
				{
					buttonValue = !buttonValue;
				}
				else
				{
					stopButton = !stopButton;
					ImGui.PopStyleColor(1);
				}
			}
			if (stopButton)
			{
				ImGui.PopStyleColor(1);
			}
		}
		else
		{
			ImGui.PushStyleColor((ImGuiCol)5, new Vector4(1f, 1f, 1f, 0.5f));
			ImGui.PushStyleColor((ImGuiCol)21, Style.ColorFalse);
			ImGui.PushStyleColor((ImGuiCol)23, Style.ColorFalse);
			ImGui.PushStyleColor((ImGuiCol)22, Style.ColorFalse);
			if (ImGui.Button(label, size))
			{
				ret = 1;
				buttonValue = !buttonValue;
			}
			if (ImGuiHelper.IsRightMouseClicked() & buttonValue)
			{
				stopButton = !stopButton;
			}
		}
		ImGui.PopStyleColor(4);
		if (!buttonValue)
		{
			stopButton = false;
		}
		if (ImGuiHelper.IsRightMouseClicked() & buttonValue)
		{
			stopButton = !stopButton;
		}
		ImGui.SameLine();
		string label_text = (smallWindow ? "展开" : "缩小");
		Vector4 _color = new Vector4(0f, 0f, 0f, 0f);
		ImGui.PushStyleColor((ImGuiCol)21, _color);
		ImGui.PushStyleColor((ImGuiCol)23, _color);
		ImGui.PushStyleColor((ImGuiCol)22, new Vector4(1f, 1f, 1f, 0.3f));
		ImGui.PushStyleColor((ImGuiCol)5, new Vector4(1f, 1f, 1f, 0.1f));
		if (ImGui.Button(label_text, new Vector2(50f, 40f)))
		{
			ret = 2;
			if (!smallWindow)
			{
				ImGui.SetWindowSize(new Vector2(170f, 80f) * Style.OverlayScale);
			}
			else
			{
				ImGui.SetWindowSize(new Vector2(320f, 400f) * Style.OverlayScale);
			}
			smallWindow = !smallWindow;
		}
		ImGui.SameLine(0f, 25f);
		ImGui.BeginChild("##保存设置", new Vector2(-1f, 50f));
		if (ImGui.Button("保存设置"))
		{
			save();
		}
		ImGui.TextDisabled("版本1.24");
		ImGui.PopStyleColor(4);
		ImGui.EndChild();
		return ret;
	}

	public void ChangeStyleView()
	{
		ImGui.Dummy(new Vector2(1f, 3f));
		Vector3 colorV3 = new Vector3(Style.MainColor.X, Style.MainColor.Y, Style.MainColor.Z);
		float alpha = Style.MainColor.W;
		if (ImGui.ColorEdit3("主颜色", ref colorV3))
		{
			Style.MainColor = new Vector4(colorV3.X, colorV3.Y, colorV3.Z, alpha);
		}
		if (ImGui.SliderFloat("主透明度", ref alpha, 0.2f, 1f, "%.1f"))
		{
			Vector4 mainColor = Style.MainColor;
			mainColor.W = alpha;
			Style.MainColor = mainColor;
		}
		ImGui.Dummy(new Vector2(1f, 3f));
		int input = qtWindow.QtLineCount;
		if (ImGui.InputInt("Qt按钮每行个数", ref input))
		{
			if (input < 1)
			{
				qtWindow.QtLineCount = 1;
			}
			else
			{
				qtWindow.QtLineCount = input;
			}
		}
		input = hotkeyWindow.HotkeyLineCount;
		if (ImGui.InputInt("快捷键每行个数", ref input))
		{
			if (input < 1)
			{
				hotkeyWindow.HotkeyLineCount = 1;
			}
			else
			{
				hotkeyWindow.HotkeyLineCount = input;
			}
		}
		float scale = hotkeyWindow.HotkeyScale;
		if (ImGui.SliderFloat("快捷键缩放", ref scale, 0.5f, 1f, "%.2f"))
		{
			hotkeyWindow.HotkeyScale = scale;
		}
		float qtBackGroundAlpha = Style.QtWindowBgAlpha;
		if (ImGui.SliderFloat("背景透明度", ref qtBackGroundAlpha, 0f, 1f, "%.1f"))
		{
			Style.QtWindowBgAlpha = qtBackGroundAlpha;
		}
		if (ImGui.SliderFloat("标签大小", ref scale, 0.5f, 1f, "%.2f"))
		{
			hotkeyWindow.HotkeyScale = scale;
		}
		ImGui.SameLine();
		if (ImGui.SliderFloat("标签页大小", ref scale, 0.5f, 1f, "%.2f"))
		{
			hotkeyWindow.HotkeyScale = scale;
		}
		ImGui.Dummy(new Vector2(1f, 3f));
		bool lockWindow = hotkeyWindow.LockWindow;
		if (ImGui.Checkbox("窗口不可拖动", ref lockWindow))
		{
			hotkeyWindow.LockWindow = lockWindow;
		}
		if (ImGui.Button("重置"))
		{
			Style.MainColor = Style.DefaultMainColor;
			Style.QtWindowBgAlpha = Style.DefaultQtWindowBgAlpha;
			qtWindow.QtLineCount = Style.DefaultQtLineCount;
		}
	}
}
