using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP;

public class PvPMCHSettings
{
	public static PvPMCHSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 减色切换 = 4000;
	public float 盾自身血量 = 0.6f;
	public bool 钻头分析= true; 
	public bool 毒菌分析= true;
	public bool 回转飞锯分析= true;
	public bool 空气锚分析= true;
	public bool 智能魔弹 = true;
	public bool 分析可用= true;
	public bool 过热野火 = true;
	public bool 热冲击= false; 
	public bool 禁止野火炮台= false; 
	public bool 金属爆发仅野火= true; 
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(183 / 255f, 122 / 255f, 65 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPMCHSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPMCHSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPMCHSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPMCHSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
