using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

namespace Linto.Scholar;

public class SCHSettings
{
	public static SCHSettings Instance;

	private static string path;

	public int opener;

	public int AethorflowReserve;

	public bool Aethorflow = true;

	public bool ChainStrategem = true;

	public bool EnergyDrain = true;

	public bool Ruin2;
	public JobViewSave JobViewSave = new() { MainColor = new Vector4(175 / 255f, 120 / 255f, 75 / 255f, 0.8f) };
	public Dictionary<string, object> StyleSetting = new();
	public bool AutoReset = true;

	public static void Build(string settingPath)
	{
		path = Path.Combine(settingPath, "SCHSettings.json");
		if (!File.Exists(path))
		{
			Instance = new SCHSettings();
			Instance.Save();
			return;
		}

		try
		{
			Instance = JsonHelper.FromJson<SCHSettings>(File.ReadAllText(path));
		}
		catch (Exception e)
		{
			Instance = new SCHSettings();
			LogHelper.Error(e.ToString());
		}
	}
	public void Save()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		File.WriteAllText(path, JsonHelper.ToJson(this));
	}
}
