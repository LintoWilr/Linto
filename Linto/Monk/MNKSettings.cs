#region

using System.Numerics;
using CombatRoutine.View.JobView;
using Common.Helper;

#endregion

namespace Linto.Monk;

public class MNKSettings
{
    public static MNKSettings Instance;
    private static string path;
    public float 功力时间 = 4000f;
    public bool AutoReset = true;
    public bool DEV界面 = true;
    public bool 脱战演武 = true;
    public bool 猫男模式 = true;
    public bool AOE = true;
    public bool 日随模式 = true;
    public JobViewSave JobViewSave = new() { MainColor = new Vector4(168 / 255f, 20 / 255f, 20 / 255f, 0.8f) };
    public Dictionary<string, object> StyleSetting = new();
    public bool 爆发 = true;
    public bool 使用说明 = true;
    public bool 自动浴血 = true;
    public bool 自动内丹 = true;
    public int Time = 100;
    public bool TP = false;
    public bool 轻身步法 = true;
    public bool 脱战搓豆 = true;
    public bool 真言队友 = true;
    public bool 吃爆发药 = false;
    public int 阳必杀 = 0;
    public int 阴必杀 = 0;
    public int 阳Step = 0;
    public int 阴Step = 0;
    public int 续过功力了 = 0;
    public int 阳必杀1型 = 0;
    public int 续过破碎了 = 0;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "MNKSettings.json");
        if (!File.Exists(path))
        {
            Instance = new MNKSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<MNKSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new MNKSettings();
            LogHelper.Error(e.ToString());
        }
    }
    public float 浴血阈值设置 = 0.7f; 
    public float 内丹阈值设置 = 0.7f; 
    public float 金刚阈值设置 = 0.7f;
    public float 真言阈值设置 = 0.6f;
    public float 真言队友阈值设置 = 0.6f;
    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
}