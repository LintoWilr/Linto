using System.Numerics;
using System.Runtime.CompilerServices;
using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP;

public static class 监控窗口
{
    // 定义窗口大小限制：最小/最大尺寸
    public const float MinWindowSize = 10f;
    public const float MaxWindowSize = 1300f; // 增大最大尺寸以支持更大窗口
    private static Vector2 _savedWindowPos = new Vector2(100, 100); // 窗口位置（临时静态存储）
    private static bool _isWindowOpen = true;

    public static void Draw(NewWindow JobViewWindow)
    {
        if (!PvPSettings.Instance.监控)
        {
            _isWindowOpen = false;
            return;
        }
        _isWindowOpen = true;

        // 配置窗口参数：设置最小/最大尺寸
        ImGui.SetNextWindowSizeConstraints(
            new Vector2(MinWindowSize, MinWindowSize),  // 最小尺寸
            new Vector2(MaxWindowSize, MaxWindowSize)   // 最大尺寸
        );

        // 设置窗口初始位置和大小
        ImGui.SetNextWindowPos(_savedWindowPos, ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSize(new Vector2(PvPSettings.Instance.宽1, PvPSettings.Instance.高1), ImGuiCond.FirstUseEver);

        // 主视口是游戏整个窗口，主窗口折叠不会影响它
        var mainViewport = ImGui.GetMainViewport();
        ImGui.SetNextWindowViewport(mainViewport.ID);
        
        // 动态设置窗口标志
        var windowFlags = ImGuiWindowFlags.NoTitleBar
                          | ImGuiWindowFlags.NoScrollbar
                          | ImGuiWindowFlags.NoDocking // 禁止停靠到主窗口
                          | ImGuiWindowFlags.NoCollapse
                          | ImGuiWindowFlags.NoBackground
                          | ImGuiWindowFlags.AlwaysAutoResize; // 可选：自动适应内容（可根据需要移除）
        // 根据 PvPSettings 动态添加 NoMove 和鼠标穿透标志
        if (PvPSettings.Instance.禁止移动窗口)
            windowFlags |= ImGuiWindowFlags.NoMove;
        if (PvPSettings.Instance.鼠标穿透)
            windowFlags |= ImGuiWindowFlags.NoInputs
                           | ImGuiWindowFlags.NoNav
                           | ImGuiWindowFlags.NoNavInputs
                           | ImGuiWindowFlags.NoMouseInputs;

        // 启动窗口
        if (ImGui.Begin("###GCD窗口", ref _isWindowOpen, windowFlags))
        {
            // 实时保存窗口位置和大小
            _savedWindowPos = ImGui.GetWindowPos();
            PvPSettings.Instance.宽1 = ImGui.GetWindowSize().X;
            PvPSettings.Instance.高1 = ImGui.GetWindowSize().Y;
            PvPSettings.Instance.Save(); // 保存设置

            // 获取当前窗口的实际内部尺寸
            Vector2 windowContentSize = ImGui.GetContentRegionAvail();

            // 绘制图标和文本
            画图标和文本(windowContentSize);
        }
        ImGui.End();
    }

    public static void 画图标和文本(Vector2 windowContentSize)
    {
        // 获取看着玩家的敌方目标
        List<IBattleChara> targetMe = PVPTargetHelper.Get看着目标的人(Group.敌人, Core.Me);

        // 创建一个默认的字符串格式化处理器
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler =
            new DefaultInterpolatedStringHandler(28, 1);

        // 生成一个图片路径，根据目标数量（最多显示4个敌人）
        defaultInterpolatedStringHandler.AppendLiteral("Resources\\Images\\Number\\");
        defaultInterpolatedStringHandler.AppendFormatted((targetMe.Count <= 4) ? ((object)targetMe.Count) : "4+");
        defaultInterpolatedStringHandler.AppendLiteral(".png");

        // 转换格式化后的路径为字符串
        string imgPath = defaultInterpolatedStringHandler.ToStringAndClear();

        // 尝试获取目标数量图标
        IDalamudTextureWrap? texture;
        if (Core.Resolve<MemApiIcon>().TryGetTexture(imgPath, out texture))
        {
            // 如果成功获取到图标，显示它
            ImGui.Text("    ");
            ImGui.SameLine();
            if (texture != null)
                ImGui.Image(texture.Handle, new Vector2(PvPSettings.Instance.图片宽1, PvPSettings.Instance.图片高1));
            ImGui.Columns();
        }

        // 如果开启了警报设置并且目标数量大于0，则显示警报窗口
        if (PvPSettings.Instance.警报 && targetMe.Count >= PvPSettings.Instance.警报数量)
        {
            Core.Resolve<MemApiChatMessage>().Toast2("好像有很多人在看你耶!", 1, 3000); // 显示警报文本
        }

        // 如果有敌人目标（目标数量大于0）
        if (targetMe.Count > 0)
        {
            int i = 1;
            IDalamudTextureWrap? textureJob;

            // 遍历每一个敌方目标
            foreach (IBattleChara v in targetMe)
            {
                if (i > PvPSettings.Instance.监控数量)
                {
                    break;
                }

                // 获取当前敌人的职业
                uint job = (uint)v.CurrentJob();
                // 根据职业获取对应的图标
                if (Core.Resolve<MemApiIcon>().TryGetTexture($"Resources\\jobs\\{job}.png", out textureJob))
                {
                    if (PvPSettings.Instance.紧凑 && i % PvPSettings.Instance.紧凑数量 == 0 && i != 1)
                    {
                        ImGui.NewLine();
                    }

                    // 如果成功获取到职业图标，显示该图标
                    if (textureJob != null) ImGui.Image(textureJob.Handle, new Vector2(50f, 50f));
                    if (PvPSettings.Instance.名字)
                    {
                        ImGui.SameLine();
                        ImGui.Text($"{v.Name}");
                    }

                    if (PvPSettings.Instance.血量)
                    {
                        ImGui.SameLine();
                        ImGui.Text($"HP百分比:{v.CurrentHpPercent() * 100f}");
                    }

                    if (PvPSettings.Instance.距离)
                    {
                        ImGui.SameLine();
                        ImGui.Text($"距离:{v.DistanceToPlayer():F1}m");
                    }
                }

                if (PvPSettings.Instance.紧凑 && i % PvPSettings.Instance.紧凑数量 != 0)
                {
                    ImGui.SameLine(0f, 5f); // 设置图标间隔
                }

                i++; // 增加计数器
            }
        }
    }
}