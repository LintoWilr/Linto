using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;


namespace Linto.LintoPvP.GNB;

public class PvPGNBSettings
{
    public static PvPGNBSettings? Instance;

    private static string? path;

    public int 爆破血量 = 70;
    public int 药血量 = 70;
    public int 粗分斩最大距离 = 5;
    public int 刚玉对象 = 50;
    public bool 刚玉队友 = true;
    public int 刚玉血量 = 50;
    public string 优先对象 = "梅友仁";
    public bool 刚玉播报 = false;
    public JobViewSave JobViewSave = new() { MainColor = new Vector4(83 / 255f, 150 / 255f, 157 / 255f, 0.8f) };
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "PvPGNBSettings.json");
        if (!File.Exists(path))
        {
            Instance = new PvPGNBSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<PvPGNBSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new PvPGNBSettings();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        if (!string.IsNullOrEmpty(path))
        {
            var directory = Path.GetDirectoryName(path);
            if (directory != null)
                Directory.CreateDirectory(directory);
            File.WriteAllText(path, JsonHelper.ToJson((object)this));
        }
    }
}
