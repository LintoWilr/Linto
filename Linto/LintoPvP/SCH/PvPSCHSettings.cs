using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

namespace Linto.LintoPvP.SCH;

public class PvPSCHSettings
{
	public static PvPSCHSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public int 扩散敌人数量 = 5;
	public int 鼓励队友数量 = 3;
	public int 跑快快队友数量 = 2;
	public bool 仅秘策使用 = false;
	public bool 自动扩毒 = true;
	public bool 上毒优先开启= true; 
	public int 枯骨法数量 = 1; 
	public int 自动扩散数量 = 5; 
	public int 扩毒剩余时间 = 10;
	public bool 鼓励优先开启;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(90 / 255f, 127 / 255f, 63 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPSCHSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPSCHSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPSCHSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPSCHSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
