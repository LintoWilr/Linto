using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;

namespace Linto.Sage.Triggers;

public class SGE_HotKey : ITriggerAction, ITriggerBase
{
	public string Key = "";

	public bool Value;

	private int _selectIndex;

	private string[] _HotKeyArray;

	public string DisplayName { get; } = "贤者/热键";


	public string Remark { get; set; }

	public SGE_HotKey()
	{
		_HotKeyArray = SageRotationEntry.JobViewWindow.GetHotkeyArray();
	}

	public bool Draw()
	{
		_selectIndex = Array.IndexOf(_HotKeyArray, Key);
		if (_selectIndex == -1)
		{
			_selectIndex = 0;
		}
		ImGuiHelper.LeftCombo("选择Key", ref _selectIndex, _HotKeyArray, 200);
		Key = _HotKeyArray[_selectIndex];
		return true;
	}

	public bool Handle()
	{
		SageRotationEntry.JobViewWindow.SetHotkey(Key);
		return true;
	}
}
