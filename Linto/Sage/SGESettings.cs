using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
namespace Linto.Sage;

public class SGESettings
{
    public static SGESettings Instance;
    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "SGESettings.json");
        if (!File.Exists(path))
        {
            Instance = new SGESettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<SGESettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new SGESettings();
            LogHelper.Error(e.ToString());
        }
    }

    public int opener = 0;
    public bool H1 = false;
    public bool useDyskrasia;
    public bool AutoHeal;
    public bool 使用说明 = true;
    public bool 康复tea = true;
    public bool 防根素溢出 = true;
    public bool 关心最低血量T = false;
    public float 最低血量T = 0.3f;
    public int time = 1500;
    public int Esuna = 3;
    public int stack = 3;
    public JobViewSave JobViewSave = new(){MainColor = new Vector4(31 / 255f, 157 / 255f, 175 / 255f, 0.8f)};
    public Dictionary<string, object> StyleSetting = new();
    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
}