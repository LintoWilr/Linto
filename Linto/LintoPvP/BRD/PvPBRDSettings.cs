using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP;

public class PvPBRDSettings
{
	public static PvPBRDSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 和弦箭 = 4;
	public bool 光阴播报 = true;
	public bool 光阴队友 = true;
	public int 光阴对象 = 0;
	public string 优先对象 = "梅友仁";
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(122 / 255f, 184 / 255f, 156 / 255f, 0.8f)};
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPBRDSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPBRDSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPBRDSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPBRDSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
