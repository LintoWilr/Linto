namespace Linto.JobView;

public class JobViewWindow
{
	private static Dictionary<string, object> jobViewSetting;

	private static Dictionary<string, object> styleSetting;

	private Action saveSetting;

	private QtWindow qtWindow;

	private HotkeyWindow hotkeyWindow;

	private MainWindow mainWindow;

	public JobViewWindow(ref Dictionary<string, object> _jobViewSetting, ref Dictionary<string, object> _styleSetting, Action save)
	{
		jobViewSetting = _jobViewSetting;
		styleSetting = _styleSetting;
		saveSetting = save;
		qtWindow = new QtWindow(ref _jobViewSetting);
		hotkeyWindow = new HotkeyWindow(ref _jobViewSetting);
		mainWindow = new MainWindow(qtWindow, hotkeyWindow);
	}
	public void SetMainStyle()
	{
		Style.SetMainStyle();
	}

	public void EndMainStyle()
	{
		Style.EndMainStyle();
	}

	public void AddQt(string name, bool qtValueDefault)
	{
		qtWindow.AddQt(name, qtValueDefault);
	}

	public void SetQtToolTip(string toolTip)
	{
		qtWindow.SetQtToolTip(toolTip);
	}

	public void DrawQtWindow()
	{
		qtWindow.DrawQtWindow();
	}

	public void QtSettingView()
	{
		qtWindow.QtSettingView();
	}

	public bool GetQt(string qtName)
	{
		return qtWindow.GetQt(qtName);
	}

	public bool SetQt(string qtName, bool qtValue)
	{
		return qtWindow.SetQt(qtName, qtValue);
	}

	public bool ReverseQt(string qtName)
	{
		return qtWindow.ReverseQt(qtName);
	}

	public void Reset()
	{
		qtWindow.Reset();
	}

	public void NewDefault(string qtName, bool newDefault)
	{
		qtWindow.NewDefault(qtName, newDefault);
	}

	public void SetDefaultFromNow()
	{
		qtWindow.SetDefaultFromNow();
	}

	public string[] GetQtArray()
	{
		return qtWindow.GetQtArray();
	}

	public void DrawHotkeyWindow()
	{
		hotkeyWindow.DrawHotkeyWindow();
	}

	public void AddHotkey(string name, IHotkeySlotResolver slot)
	{
		hotkeyWindow.AddHotkey(name, slot);
	}

	public List<string> GetActiveList()
	{
		return hotkeyWindow.ActiveList;
	}

	public void SetHotkeyToolTip(string toolTip)
	{
		hotkeyWindow.SetHotkeyToolTip(toolTip);
	}

	public void SetHotkey(string name)
	{
		hotkeyWindow.SetHotkey(name);
	}

	public void CancelHotkey(string name)
	{
		GetActiveList().Remove(name);
	}

	public string[] GetHotkeyArray()
	{
		return hotkeyWindow.GetHotkeyArray();
	}

	public void HotkeySettingView()
	{
		hotkeyWindow.HotkeySettingView();
	}

	public void RunHotkey()
	{
		hotkeyWindow.RunHotkey();
	}

	public int MainControlView(ref bool buttonValue, ref bool stopButton)
	{
		return mainWindow.MainControlView(ref buttonValue, ref stopButton, saveSetting);
	}

	public void ChangeStyleView()
	{
		mainWindow.ChangeStyleView();
	}

	public static Dictionary<string, object> GetStyleSetting()
	{
		return styleSetting;
	}
}
