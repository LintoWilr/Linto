using Linto.JobView;

namespace Linto.Wilr.DarkKnight;

public static class JobWindow
{
	public static JobViewWindow Instance;

	static JobWindow()
	{
		Instance = new JobViewWindow(ref PvPDRKSettings.Instance.JobWindowSetting, ref PvPDRKSettings.Instance.StyleSetting, PvPDRKSettings.Instance.Save);
		Instance.AddQt("基础连击", qtValueDefault: true);
		Instance.SetQtToolTip("噬魂斩连击");
		Instance.AddQt("喝热水", qtValueDefault: true);
		Instance.AddQt("自动净化", qtValueDefault: true);
		// Instance.AddHotkey("LB", new LB());
	}
}
