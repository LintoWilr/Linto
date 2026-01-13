namespace Linto.Wilr.DarkKnight;

public static class Qt
{
	public static bool GetQt(string qtName)
	{
		return JobWindow.Instance.GetQt(qtName);
	}

	public static bool ReverseQt(string qtName)
	{
		return JobWindow.Instance.ReverseQt(qtName);
	}

	public static bool SetQt(string qtName, bool qtValue)
	{
		return JobWindow.Instance.SetQt(qtName, qtValue);
	}

	public static void Reset()
	{
		JobWindow.Instance.Reset();
	}

	public static void NewDefault(string qtName, bool newDefault)
	{
		JobWindow.Instance.NewDefault(qtName, newDefault);
	}

	public static void SetDefaultFromNow()
	{
		JobWindow.Instance.SetDefaultFromNow();
	}

	public static string[] GetQtArray()
	{
		return JobWindow.Instance.GetQtArray();
	}
}
