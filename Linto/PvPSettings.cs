using AEAssist.Helper;
using AEAssist.IO;
using System.Numerics;

namespace Linto;

public class PvPSettings
{
    public static PvPSettings? Instance;

    private static string? path;
    public bool 监控 = false;
    public bool 警报 = true;
    public bool 播报 = true;
    public bool 鬼叫 = true;
    public bool 自动选中 = true;
    public bool 不选冰 = false;
    public bool 技能自动选中 = true;
    public int 自动选中自定义范围 = 50;
    public bool 最合适目标 = true;
    public int 长臂猿 = 0;
    public int 坐骑cd = 5;
    public bool 无目标坐骑 = true;
    public int 无目标坐骑范围 = 50;
    public int 冲刺 = 5;
    public int Volume = 100;
    public int 语音cd = 1;
    public int 语音选择 = 1;
    public bool 指定坐骑 = false;
    public uint 坐骑名 = 1u;
    public float 宽1 = 500f;
    public float 高1 = 500f;
    public float 图片比例 = 125.0f;
    public float 图标大小 = 32.0f;
    public bool 紧凑 = true;
    public int 紧凑数量 = 3;
    public int 警报数量 = 4;
    public bool 窗口开关 = true;
    public int 监控数量 = 6;
    public float iconWidth = 64.0f; // 图标宽度，默认 64 像素
    public float inputWidth = 100.0f; // InputInt 宽度，默认 100 像素
    public float lineSpacing = 10.0f; // 行距（style.ItemSpacing.Y），默认 4 像素
    public Vector2 监控窗口位置= new Vector2(100, 100); // 默认位置
    public Vector2 监控窗口大小= new Vector2(200, 200); // 默认大小
    public bool 禁止移动窗口  = false; 
    public bool 名字 = true;
    public bool 血量 = true;
    public bool 距离 = true;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "PvPSettings.json");
        if (!File.Exists(path))
        {
            Instance = new PvPSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<PvPSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new PvPSettings();
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