using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.VPR;

public class PvPVPRSettings
{
	public static PvPVPRSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int AOE数量 = 1;
	public int 周围敌人数量 = 4;
	public float 地天自身血量 = 0.99f;
	public bool 斩铁检查状态  = false;
	public bool 多斩模式  = false;
	public int 多斩人数  = 2;
	public bool 读条检查  = false;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(72 / 255f, 126 / 255f, 142 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPVPRSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPVPRSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPVPRSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPVPRSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
