using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Linto.LintoPvP.PCT;

public class PvPPCTSettings
{
    public static PvPPCTSettings? Instance;

    private static string? path;

    public int 药血量 = 70;
    public int 减色切换 = 4000;
    public float 盾自身血量 = 0.6f;
    public bool 脱战嗑药 = true;
    public JobViewSave JobViewSave = new() { MainColor = new Vector4(186 / 255f, 140 / 255f, 98 / 255f, 0.8f) };
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "PvPPCTSettings.json");
        if (!File.Exists(path))
        {
            Instance = new PvPPCTSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<PvPPCTSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new PvPPCTSettings();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        if (path == null)
            return;
            
        var dirPath = Path.GetDirectoryName(path);
        if (dirPath != null)
            Directory.CreateDirectory(dirPath);
        File.WriteAllText(path, JsonHelper.ToJson((object)this));
    }
}
