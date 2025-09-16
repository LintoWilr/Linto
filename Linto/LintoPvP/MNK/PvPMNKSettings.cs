using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

namespace Linto.LintoPvP.MNK;

public class PvPMNKSettings
{
	public static PvPMNKSettings Instance;

	private static string path;

	public int 药血量 = 70;
	public bool 武僧小连招 = false;
	public bool se6 = true;
	public float 金刚阈值设置 = 0.7f;
	public float 金刚回血阈值 = 0.3f;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(175 / 255f, 120 / 255f, 75 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPMNKSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPMNKSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPMNKSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPMNKSettings();
			LogHelper.Error(e.ToString());
		}
	}

	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
