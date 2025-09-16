using Common.Helper;

namespace Linto.Wilr.DarkKnight;

public class PvPDRKSettings
{
	public static PvPDRKSettings Instance;

	private static string path;

	public int MTOpener;

	public int STOpener;

	public bool BNinOpener = true;

	public bool Far4 = true;

	public int OGCDLock = 600;

	public bool NoOgcdFar = true;

	public bool PushInPotion = true;

	public bool AutoMT;

	public bool AutoST;

	public bool AutoGrit;

	public bool AutoReset = true;

	public bool useTTK;

	public Dictionary<string, object> StyleSetting = new Dictionary<string, object>();

	public Dictionary<string, object> JobWindowSetting = new Dictionary<string, object>();

	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPDK.json");
		if (!File.Exists(path))
		{
			Instance = new PvPDRKSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPDRKSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPDRKSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
