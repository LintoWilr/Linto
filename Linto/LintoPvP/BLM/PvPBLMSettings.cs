using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.BLM;

public class PvPBLMSettings
{
	public static PvPBLMSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 昏沉 = 0;
	public int 冰时间 = 300;
	public int 磁暴敌人距离 = 5;
	public int 磁暴敌人数量 = 3;
	public bool 寒冰环 = false;
	public bool 最佳AOE;
	public int 最佳人数 = 3;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(145 / 255f, 75 / 255f, 175 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPBLMSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPBLMSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPBLMSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPBLMSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
