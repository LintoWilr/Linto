using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.RDM;

public class PvPRDMSettings
{
	public static PvPRDMSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 鼓励人数 = 0;
	public bool 南天自己 = false;
	public bool 鼓励光芒四射 = false;
	public bool 鼓励决断 = false;
	public int 护盾距离 = 10;
	public int 护盾人数 = 4;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(220 / 255f, 74 / 255f, 74 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPRDMSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPRDMSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPRDMSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPRDMSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
