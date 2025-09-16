using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.DRG;

public class PvPDRGSettings
{
	public static PvPDRGSettings Instance;
	private static string path;
	public int 药血量 = 70;
	public bool 樱花缭乱龙血;
	public bool 死者之岸樱花;
	public bool 死者之岸天龙;
	public bool 死者之岸苍穹刺;
	public bool 苍穹刺龙血; 
	public bool 高跳龙血;
	public bool 天龙龙血;
	public bool 后跳龙血 = true;
	public bool 后跳面向 = true;
	public bool 后跳解除控制;
	public bool 后跳进入爆发 = true;
	public int 高跳范围  = 20;
	public int 天龙点睛主目标距离  = 15;
	public bool 恐惧咆哮x龙血 = true;
	public int 恐惧咆哮人数  = 1;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(192 / 255f, 128 / 255f, 103 / 255f, 0.8f) };
	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "PvPDRGSettings.json");
		if (!File.Exists(path))
		{
			Instance = new PvPDRGSettings();
			Instance.Save();
			return;
		}
		try
		{
			Instance = JsonHelper.FromJson<PvPDRGSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new PvPDRGSettings();
			LogHelper.Error(e.ToString());
		}
	}
	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson((object)this));
	}
}
