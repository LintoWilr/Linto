using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.SMN;

public class PvPSMNSettings
{
	public static PvPSMNSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 火神冲 = 5;
	public int 守护之光血量 = 50;
	public bool 守护队友 = true;
	public int 守护对象 = 50;
	public string 优先对象 = "梅友仁";
	public bool 守护播报 = false;
	public float 溃烂阈值 = 0.5f;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(230 / 255f, 129 / 255f, 129 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPSMNSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPSMNSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPSMNSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPSMNSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
