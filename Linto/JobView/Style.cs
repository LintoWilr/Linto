using System.Numerics;
using CombatRoutine.Setting;
using ImGuiNET;

namespace Linto.JobView;

public static class Style
{

	public static readonly Vector4 DefaultMainColor = new Vector4(0.6313726f, 0.18431373f, 38f / 85f, 0.8f);

	public static readonly float DefaultQtWindowBgAlpha = 0.2f;

	public static readonly int DefaultQtLineCount = 3;

	public static readonly int DefaultHotkeyLineCount = 4;

	public static readonly float DefaultHotkeyScale = 0.7f;

	public static Dictionary<string, object> StyleSetting => JobViewWindow.GetStyleSetting();

	public static float OverlayScale => SettingMgr.GetSetting<GeneralSettings>().OverlayScale;

	public static Vector2 QtButtonSize => new Vector2(95f, 40f) * OverlayScale;

	public static Vector4 ColorFalse => new Vector4(5f / 51f, 5f / 51f, 5f / 51f, MainColor.W);

	public static Vector4 ColorDark => new Vector4(0.18431373f, 0.18431373f, 0.18431373f, MainColor.W);

	public static Vector4 ColorDark2 => new Vector4(27f / 85f, 27f / 85f, 27f / 85f, MainColor.W);

	public static Vector4 ColorHovered => new Vector4((MainColor.X * 255f + 20f) / 255f, (MainColor.Y * 255f + 20f) / 255f, (MainColor.Z * 255f + 20f) / 255f, MainColor.W);

	public static Vector4 ColorActive => new Vector4((MainColor.X * 255f + 50f) / 255f, (MainColor.Y * 255f + 50f) / 255f, (MainColor.Z * 255f + 50f) / 255f, MainColor.W);

	public static Vector4 MainColor
	{
		get
		{
			StyleSetting.TryAdd("MainColor", DefaultMainColor);
			return (Vector4)StyleSetting["MainColor"];
		}
		set
		{
			StyleSetting["MainColor"] = value;
		}
	}

	public static float QtWindowBgAlpha
	{
		get
		{
			StyleSetting.TryAdd("QtWindowBgAlpha", DefaultQtWindowBgAlpha);
			return Convert.ToSingle(StyleSetting["QtWindowBgAlpha"]);
		}
		set
		{
			if (StyleSetting.ContainsKey("QtWindowBgAlpha"))
			{
				StyleSetting["QtWindowBgAlpha"] = value;
			}
			else
			{
				StyleSetting.TryAdd("QtWindowBgAlpha", value);
			}
		}
	}

	public static void SetMainStyle()
	{
		ImGui.PushStyleColor((ImGuiCol)5, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor((ImGuiCol)7, ColorDark);
		Vector4 defaultMainColor = DefaultMainColor;
		defaultMainColor.X = 0f;
		defaultMainColor.Y = 0f;
		defaultMainColor.Z = 0f;
		ImGui.PushStyleColor((ImGuiCol)4, defaultMainColor);
		ImGui.PushStyleColor((ImGuiCol)30, ColorFalse);
		ImGui.PushStyleColor((ImGuiCol)32, MainColor);
		ImGui.PushStyleColor((ImGuiCol)31, MainColor);
		ImGui.PushStyleColor((ImGuiCol)18, ColorActive);
		ImGui.PushStyleColor((ImGuiCol)19, MainColor);
		ImGui.PushStyleColor((ImGuiCol)20, ColorActive);
		ImGui.PushStyleColor((ImGuiCol)21, ColorDark2);
		ImGui.PushStyleColor((ImGuiCol)23, ColorActive);
		ImGui.PushStyleColor((ImGuiCol)22, ColorHovered);
		ImGui.PushStyleColor((ImGuiCol)24, MainColor);
		ImGui.PushStyleColor((ImGuiCol)26, ColorActive);
		ImGui.PushStyleColor((ImGuiCol)25, ColorHovered);
		ImGui.PushStyleColor((ImGuiCol)33, ColorDark);
		ImGui.PushStyleColor((ImGuiCol)35, MainColor);
		ImGui.PushStyleColor((ImGuiCol)34, ColorHovered);
	}

	public static void EndMainStyle()
	{
		ImGui.PopStyleColor(18);
	}

	public static void SetQtStyle()
	{
		ImGui.SetNextWindowSize(new Vector2(1000f, 1000f), (ImGuiCond)4);
		ImGui.PushStyleVar((ImGuiStyleVar)11, 4f);
		ImGui.PushStyleVar((ImGuiStyleVar)1, new Vector2(8f, 8f));
		ImGui.PushStyleVar((ImGuiStyleVar)13, new Vector2(6f, 6f));
		ImGui.PushStyleColor((ImGuiCol)2, new Vector4(0f, 0f, 0f, QtWindowBgAlpha));
	}

	public static void EndQtStyle()
	{
		ImGui.PopStyleVar(3);
		ImGui.PopStyleColor(1);
	}
}
