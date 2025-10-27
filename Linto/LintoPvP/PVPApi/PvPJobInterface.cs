using AEAssist;
using AEAssist.Helper;
using Dalamud.Bindings.ImGui;
using FFXIVClientStructs.FFXIV.Common.Math;
using Linto.LintoPvP.BLM;
using Linto.LintoPvP.DRG;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.PCT;
using Linto.LintoPvP.RDM;
using Linto.LintoPvP.SAM;
using Linto.LintoPvP.SMN;
using Linto.LintoPvP.VPR;

namespace Linto.LintoPvP.PVPApi;
/// <summary>
/// 职业悬浮窗配置接口
/// </summary>
public interface PvPJobInterface
{
    /// <summary>
    /// 获取当前权限配置
    /// </summary>
}
public abstract class BaseJobConfig : PvPJobInterface
{
    /// <summary>
    /// 获取当前是否有权限 非通用码可用
    /// </summary>
    public virtual void 权限获取()
    {
        PVPHelper.权限获取();
        
    }

    /// <summary>
    /// 适用于只有单个Bool的技能
    /// </summary>
    public virtual void 配置Bool1(uint 技能id, string 技能名字, string 效果描述, ref bool 效果bool, int ID)
    {
        PVPHelper.技能配置2(技能id, 技能名字, 效果描述, ref 效果bool, ID);
    }
    /// <summary>
    /// 适用于2Bool的技能
    /// </summary>
    public virtual void 配置Bool2(uint 技能id, string 技能名字, string 效果描述,string 效果描述2, ref bool 效果bool,ref bool 效果bool2, int ID)
    {
        PVPHelper.技能配置7(技能id, 技能名字, 效果描述,效果描述2, ref 效果bool,ref 效果bool2,ID);
    }
    /// <summary>
    /// 适用于3Bool的技能
    /// </summary>
    public virtual void 配置Bool3(uint 技能id, string 技能名字, string 效果描述,string 效果描述2,string 效果描述3, ref bool 效果bool,ref bool 效果bool2,ref bool 效果bool3, int ID)
    {
        PVPHelper.技能配置6(技能id, 技能名字, 效果描述,效果描述2,效果描述3, ref 效果bool,ref 效果bool2, ref 效果bool3,ID);
    }
    /// <summary>
    /// 适用于只有单个int的技能
    /// </summary>
    public virtual void 配置Int1(uint 技能id, string 技能名字, string 效果描述, ref int 效果int, int 变化幅度, int 快速变化幅度, int id)
    {
        PVPHelper.技能配置3(技能id, 技能名字, 效果描述, ref 效果int, 变化幅度, 快速变化幅度, id);
    }
    /// <summary>
    /// 适用于bool+int的技能
    /// </summary>
    public virtual void 配置Int1Bool1(uint 技能id, string 技能名字, string 数值描述, string 效果描述, ref bool 效果bool, ref int 效果int, int 变化幅度, int 快速变化幅度, int id)
    {
        PVPHelper.技能配置4(技能id, 技能名字, 数值描述, 效果描述, ref 效果bool, ref 效果int, 变化幅度, 快速变化幅度, id);
    }
    /// <summary>
    /// 适用于单个滑块float的技能
    /// </summary>
    public virtual void 配置Float1(uint 技能id, string 技能名字, string 数值描述, ref float 效果int, float min, float max, int id)
    {
        PVPHelper.技能配置5(技能id, 技能名字, 数值描述, ref 效果int, min, max, id);
    }
    /// <summary>
    /// 只是技能解释，只是解释
    /// </summary>
    public virtual void 配置解释(uint 技能id, string 技能名字, string 效果描述)
    {
        PVPHelper.技能解释(技能id, 技能名字, 效果描述);
    }
}
public class 职业配置 : BaseJobConfig
{
    public class 自定义
    {
        public static void 死者之岸()
        {
            var style = ImGui.GetStyle();

            float lineHeight = ImGui.GetTextLineHeight();
            float checkboxHeight = ImGui.GetFrameHeight();
            float totalHeight =
                (lineHeight * 3) + (checkboxHeight * 3) + (PvPSettings.Instance.lineSpacing * 3); // 三行文字 + 三行 Checkbox
            float iconSize = PvPSettings.Instance.iconWidth;
            float textAreaWidth = ImGui.GetContentRegionAvail().X - PvPSettings.Instance.iconWidth -
                                  style.ItemSpacing.X * 2;

            Vector2 startPos = ImGui.GetCursorPos();

            PVPHelper.技能图标(DRGSkillID.死者之岸, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + PvPSettings.Instance.iconWidth + style.ItemSpacing.X,
                startPos.Y));
            ImGui.Text("死者之岸");

            // 第二行：有樱花缭乱时不用 + Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + PvPSettings.Instance.iconWidth + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("有樱花缭乱时不用:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##死者之岸天龙", ref PvPDRGSettings.Instance.死者之岸樱花);

            // 第三行：有天龙点睛时不用 + Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + PvPSettings.Instance.iconWidth + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + checkboxHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("有天龙点睛时不用:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##死者天龙", ref PvPDRGSettings.Instance.死者之岸天龙);

            // 第四行：有苍穹刺时不用 + Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + PvPSettings.Instance.iconWidth + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 3) + (checkboxHeight * 2) + (PvPSettings.Instance.lineSpacing * 3)));
            ImGui.Text("有苍穹刺时不用:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##死者之岸苍穹刺", ref PvPDRGSettings.Instance.死者之岸苍穹刺);

            // 分隔线
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }

        /// <summary>
        /// 待更新
        /// </summary>
        public static void 磁暴()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); 
            float inputHeight = ImGui.GetFrameHeight(); 
            float totalHeight =
                (lineHeight * 2) + (inputHeight * 2) +
                (PvPSettings.Instance.lineSpacing * 2); // 两行文字 + 两行 InputInt + 两段行间距
            float iconSize = PvPSettings.Instance.iconWidth; 
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; 

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(29657, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("磁暴");

            // 第二行：多少米范围内 + InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("多少米范围内:");
            ImGui.SameLine();
            float descWidth1 = ImGui.CalcTextSize("多少米范围内:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPBLMSettings.Instance.磁暴敌人距离 = Math.Clamp(PvPBLMSettings.Instance.磁暴敌人距离, 5, 10);
            ImGui.InputInt("##MagneticStormDistance", ref PvPBLMSettings.Instance.磁暴敌人距离, 1, 5);

            // 第三行：存在敌人数量 + InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("存在敌人数量:");
            ImGui.SameLine();
            float descWidth2 = ImGui.CalcTextSize("存在敌人数量:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPBLMSettings.Instance.磁暴敌人数量 = Math.Clamp(PvPBLMSettings.Instance.磁暴敌人数量, 1, 48);
            ImGui.InputInt("##MagneticStormCount", ref PvPBLMSettings.Instance.磁暴敌人数量, 1, 5);

            // 分隔线
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }
        
        public static void 剑身强部()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt 高度
            float totalHeight =
                (lineHeight * 2) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 2); // 两行文字 + 两行控件 + 两段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(41496, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("剑身强部");

            // 第二行：多少米范围内 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("多少米范围内:");
            ImGui.SameLine();
            float descWidth1 = ImGui.CalcTextSize("多少米范围内:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPRDMSettings.Instance.护盾距离 = Math.Clamp(PvPRDMSettings.Instance.护盾距离, 0, 30);
            ImGui.InputInt("##SwordShieldDistance", ref PvPRDMSettings.Instance.护盾距离, 1, 5);

            // 第三行：存在敌人数量 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("存在敌人数量:");
            ImGui.SameLine();
            float descWidth2 = ImGui.CalcTextSize("存在敌人数量:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPRDMSettings.Instance.护盾人数 = Math.Clamp(PvPRDMSettings.Instance.护盾人数, 1, 48);
            ImGui.InputInt("##SwordShieldCount", ref PvPRDMSettings.Instance.护盾人数, 1, 5);

            // 分隔线：位于图标或内容下方
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }
        public static void 后跳()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // Checkbox 高度
            float totalHeight = (lineHeight * 2) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 2); // 两行文字 + 两行控件 + 两段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(DRGSkillID.后跳, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("后跳");

            // 第二行：被控时主动使用后跳 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("被控时主动使用后跳:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##BackstepControlBreak", ref PvPDRGSettings.Instance.后跳解除控制);

            // 第三行：往镜头方向后跳 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("往镜头方向后跳:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##BackstepCameraDirection", ref PvPDRGSettings.Instance.后跳面向);

            // 分隔线：位于图标或内容下方
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }

        public static void 地天()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt/SliderFloat 高度
            float totalHeight =
                (lineHeight * 2) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 2); // 两行文字 + 两行控件 + 两段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(29533u, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("地天");

            // 第二行：10米内人数大于 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("10米内人数大于:");
            ImGui.SameLine();
            float descWidth1 = ImGui.CalcTextSize("10米内人数大于:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPSAMSettings.Instance.周围敌人数量 = Math.Clamp(PvPSAMSettings.Instance.周围敌人数量, 1, 48);
            ImGui.InputInt("##EarthSkyEnemyCount", ref PvPSAMSettings.Instance.周围敌人数量, 1, 100);

            // 第三行：自身血量小于 SliderFloat
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("自身血量小于:");
            ImGui.SameLine();
            float descWidth2 = ImGui.CalcTextSize("自身血量小于:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPSAMSettings.Instance.地天自身血量 = Math.Clamp(PvPSAMSettings.Instance.地天自身血量, 0.01f, 1.0f);
            ImGui.SliderFloat("##EarthSkyHealth", ref PvPSAMSettings.Instance.地天自身血量, 0.01f, 1.0f);

            // 分隔线：位于图标或内容下方
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }

        public static void 斩铁()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt/Checkbox 高度
            float totalHeight =
                (lineHeight * 3) + (inputHeight * 3) + (PvPSettings.Instance.lineSpacing * 3); // 三行文字 + 三行控件 + 三段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(29537u, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("合法斩铁");ImGui.SameLine();ImGui.Text("推荐使用PvPAuto进行斩铁");

            // 第二行：确认不被控再使用 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("确认不被控再使用:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##ZantetsuControlCheck", ref PvPSAMSettings.Instance.斩铁检查状态);

            // 第三行：多斩模式 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("多斩(过于合法不一定有用):");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##ZantetsuMultiMode", ref PvPSAMSettings.Instance.多斩模式);

            // 第四行：多斩人数 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 3) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 3)));
            ImGui.Text("多斩人数:");
            ImGui.SameLine();
            float descWidth = ImGui.CalcTextSize("多斩人数:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPSAMSettings.Instance.多斩人数 = Math.Clamp(PvPSAMSettings.Instance.多斩人数, 2, 48);
            ImGui.InputInt("##ZantetsuMultiCount", ref PvPSAMSettings.Instance.多斩人数, 1, 100);

            // 分隔线：位于图标或内容下方
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }

        /// <summary>
        /// 光阴神配置界面
        /// </summary>
        public static void 光阴神()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt/InputText/按钮高度
            float totalHeight =
                (lineHeight * 5) + (inputHeight * 5) + (PvPSettings.Instance.lineSpacing * 5); // 五行文字 + 五行控件 + 五段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸（正方形，宽度 = 高度）
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(29400U, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("光阴神");

            // 第二行：解控队友 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("解控队友:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##LightGodTeammate", ref PvPBRDSettings.Instance.光阴队友);
            ImGui.SameLine();
            ImGui.Text("未勾选则只解控自己");

            // 第三行：聊天框打印 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("聊天框打印:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##LightGodReport", ref PvPBRDSettings.Instance.光阴播报);
            ImGui.SameLine();
            ImGui.Text("例 [AEAssist]光阴对象:XX");

            // 第四行：优先玩家名 InputText
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 3) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 3)));
            ImGui.Text("优先玩家名:");
            ImGui.SameLine();
            float descWidth1 = ImGui.CalcTextSize("优先玩家名:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); 
            ImGui.InputText("##LightGodPriorityName", ref PvPBRDSettings.Instance.优先对象, 10);
            ImGui.SameLine();
            ImGui.Text("优先逻辑:目标玩家＞自己＞其他队友");
            
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 4) + (inputHeight * 3) + (PvPSettings.Instance.lineSpacing * 4)));
            ImGui.Text("优先对象:");
            ImGui.SameLine();
            float descWidth2 = ImGui.CalcTextSize("优先对象:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); 
            PvPBRDSettings.Instance.光阴对象 = Math.Clamp(PvPBRDSettings.Instance.光阴对象, 0, PartyHelper.Party.Count - 1);
            ImGui.InputInt("##光阴神对象", ref PvPBRDSettings.Instance.光阴对象, 1, 100);
            ImGui.SameLine();
            if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPBRDSettings.Instance.光阴对象 >= 0 &&
                PvPBRDSettings.Instance.光阴对象 < PartyHelper.Party.Count)
            {
                var partyMember = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象];
                ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
            }
            else
            {
                ImGui.Text("未存在此玩家");
            }
            
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 5) + (inputHeight * 4) + (PvPSettings.Instance.lineSpacing * 5)));
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); 
            if (ImGui.Button("优先玩家名设定该对象##LightGodSetPriority"))
            {
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPBRDSettings.Instance.光阴对象 >= 0 &&
                    PvPBRDSettings.Instance.光阴对象 < PartyHelper.Party.Count)
                {
                    PvPBRDSettings.Instance.优先对象 = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].Name.TextValue;
                }
            }

            // 处理优先玩家名逻辑
            var targetMember =
                PartyHelper.Party.FirstOrDefault(x => x?.Name?.TextValue == PvPBRDSettings.Instance.优先对象);
            if (targetMember != null)
            {
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPBRDSettings.Instance.光阴对象 = targetIndex;
                // 第五行已显示玩家名，无需重复
            }
            
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }
        public static void 守护之光()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt/InputText/按钮高度
            float totalHeight =
                (lineHeight * 6) + (inputHeight * 6) + (PvPSettings.Instance.lineSpacing * 6); // 六行文字 + 六行控件 + 六段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸（正方形，宽度 = 高度）
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(29670U, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("守护之光");

            // 第二行：血量阈值 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("血量阈值:");
            ImGui.SameLine();
            float bloodDescWidth = ImGui.CalcTextSize("血量阈值:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPSMNSettings.Instance.守护之光血量 = Math.Clamp(PvPSMNSettings.Instance.守护之光血量, 1, 101);
            ImGui.InputInt("##GuardLightBloodThreshold", ref PvPSMNSettings.Instance.守护之光血量, 10, 20);

            // 第三行：守护队友 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("守护队友:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##GuardLightTeammate", ref PvPSMNSettings.Instance.守护队友);

            // 第四行：聊天框打印 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 3) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 3)));
            ImGui.Text("聊天框打印:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##GuardLightReport", ref PvPSMNSettings.Instance.守护播报);

            // 第五行：优先玩家名 InputText
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 4) + (inputHeight * 3) + (PvPSettings.Instance.lineSpacing * 4)));
            ImGui.Text("优先玩家名:");
            ImGui.SameLine();
            float priorityDescWidth = ImGui.CalcTextSize("优先玩家名:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            ImGui.InputText("##GuardLightPriorityName", ref PvPSMNSettings.Instance.优先对象, 10);

            // 第六行：优先对象索引 InputInt 和玩家名显示
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 5) + (inputHeight * 4) + (PvPSettings.Instance.lineSpacing * 5)));
            ImGui.Text("优先对象:");
            ImGui.SameLine();
            float targetDescWidth = ImGui.CalcTextSize("优先对象:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPSMNSettings.Instance.守护对象 = Math.Clamp(PvPSMNSettings.Instance.守护对象, 0, PartyHelper.Party.Count - 1);
            ImGui.InputInt("##GuardLightTargetIndex", ref PvPSMNSettings.Instance.守护对象, 1, 100);
            ImGui.SameLine();
            if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPSMNSettings.Instance.守护对象 >= 0 &&
                PvPSMNSettings.Instance.守护对象 < PartyHelper.Party.Count)
            {
                var partyMember = PartyHelper.Party[PvPSMNSettings.Instance.守护对象];
                ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
            }
            else
            {
                ImGui.Text("未存在此玩家");
            }

            // 第七行：优先玩家名设定按钮
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 6) + (inputHeight * 5) + (PvPSettings.Instance.lineSpacing * 6)));
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            if (ImGui.Button("优先玩家名设定该对象##GuardLightSetPriority"))
            {
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPSMNSettings.Instance.守护对象 >= 0 &&
                    PvPSMNSettings.Instance.守护对象 < PartyHelper.Party.Count)
                {
                    PvPSMNSettings.Instance.优先对象 = PartyHelper.Party[PvPSMNSettings.Instance.守护对象].Name.TextValue;
                }
            }

            // 处理优先玩家名逻辑：如果优先对象存在，自动更新守护对象索引
            var targetMember =
                PartyHelper.Party.FirstOrDefault(x => x?.Name?.TextValue == PvPSMNSettings.Instance.优先对象);
            if (targetMember != null)
            {
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPSMNSettings.Instance.守护对象 = targetIndex;
            }

            // 分隔线：位于图标或内容下方
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }

        /// <summary>
        /// 刚玉
        /// </summary>
        public static void 刚玉之心()
        {
            var style = ImGui.GetStyle();

            // 计算布局参数
            float lineHeight = ImGui.GetTextLineHeight(); // 单行文字高度
            float inputHeight = ImGui.GetFrameHeight(); // InputInt/InputText/按钮高度
            float totalHeight =
                (lineHeight * 6) + (inputHeight * 6) + (PvPSettings.Instance.lineSpacing * 6); // 六行文字 + 六行控件 + 六段行间距
            float iconSize = PvPSettings.Instance.iconWidth; // 图标尺寸（正方形，宽度 = 高度）
            float textAreaWidth = ImGui.GetContentRegionAvail().X - iconSize - style.ItemSpacing.X * 2; // 文字区域宽度

            Vector2 startPos = ImGui.GetCursorPos();

            // 显示技能图标
            PVPHelper.技能图标(41443U, iconSize);

            // 第一行：技能名字
            ImGui.SameLine();
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X, startPos.Y));
            ImGui.Text("刚玉之心");

            // 第二行：血量阈值 InputInt
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + lineHeight + PvPSettings.Instance.lineSpacing));
            ImGui.Text("血量阈值:");
            ImGui.SameLine();
            float bloodDescWidth = ImGui.CalcTextSize("血量阈值:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPGNBSettings.Instance.刚玉血量 = Math.Clamp(PvPGNBSettings.Instance.刚玉血量, 1, 101);
            ImGui.InputInt("##JadeHeartBloodThreshold", ref PvPGNBSettings.Instance.刚玉血量, 10, 20);

            // 第三行：刚玉队友 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 2) + inputHeight + (PvPSettings.Instance.lineSpacing * 2)));
            ImGui.Text("刚玉队友:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##JadeHeartTeammate", ref PvPGNBSettings.Instance.刚玉队友);

            // 第四行：聊天框打印 Checkbox
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 3) + (inputHeight * 2) + (PvPSettings.Instance.lineSpacing * 3)));
            ImGui.Text("聊天框打印:");
            ImGui.SameLine();
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + style.ItemSpacing.X);
            ImGui.Checkbox("##JadeHeartReport", ref PvPGNBSettings.Instance.刚玉播报);

            // 第五行：优先玩家名 InputText
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 4) + (inputHeight * 3) + (PvPSettings.Instance.lineSpacing * 4)));
            ImGui.Text("优先玩家名:");
            ImGui.SameLine();
            float priorityDescWidth = ImGui.CalcTextSize("优先玩家名:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            ImGui.InputText("##JadeHeartPriorityName", ref PvPGNBSettings.Instance.优先对象, 10);

            // 第六行：优先对象索引 InputInt 和玩家名显示
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 5) + (inputHeight * 4) + (PvPSettings.Instance.lineSpacing * 5)));
            ImGui.Text("优先对象:");
            ImGui.SameLine();
            float targetDescWidth = ImGui.CalcTextSize("优先对象:").X;
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            PvPGNBSettings.Instance.刚玉对象 = Math.Clamp(PvPGNBSettings.Instance.刚玉对象, 0, PartyHelper.Party.Count - 1);
            ImGui.InputInt("##JadeHeartTargetIndex", ref PvPGNBSettings.Instance.刚玉对象, 1, 100);
            ImGui.SameLine();
            if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPGNBSettings.Instance.刚玉对象 >= 0 &&
                PvPGNBSettings.Instance.刚玉对象 < PartyHelper.Party.Count)
            {
                var partyMember = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象];
                ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
            }
            else
            {
                ImGui.Text("未存在此玩家");
            }

            // 第七行：优先玩家名设定按钮
            ImGui.SetCursorPos(new Vector2(startPos.X + iconSize + style.ItemSpacing.X,
                startPos.Y + (lineHeight * 6) + (inputHeight * 5) + (PvPSettings.Instance.lineSpacing * 6)));
            ImGui.SetNextItemWidth(PvPSettings.Instance.inputWidth); // 直接使用 inputWidth
            if (ImGui.Button("优先玩家名设定该对象##JadeHeartSetPriority"))
            {
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPGNBSettings.Instance.刚玉对象 >= 0 &&
                    PvPGNBSettings.Instance.刚玉对象 < PartyHelper.Party.Count)
                {
                    PvPGNBSettings.Instance.优先对象 = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].Name.TextValue;
                }
            }

            // 处理优先玩家名逻辑：如果优先对象存在，自动更新刚玉对象索引
            var targetMember =
                PartyHelper.Party.FirstOrDefault(x => x?.Name?.TextValue == PvPGNBSettings.Instance.优先对象);
            if (targetMember != null)
            {
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPGNBSettings.Instance.刚玉对象 = targetIndex;
            }

            // 分隔线
            ImGui.SetCursorPosY(startPos.Y + Math.Max(totalHeight, iconSize) + 5);
            ImGui.Separator();

            // 留出空间
            ImGui.SetCursorPosY(ImGui.GetCursorPosY());
        }
    }
    public void 配置龙骑技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPDRGSettings.Instance.药血量 = Math.Clamp(PvPDRGSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPDRGSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 樱花缭乱
        配置Bool1(DRGSkillID.樱花缭乱, "樱花缭乱", "仅龙血时使用", ref PvPDRGSettings.Instance.樱花缭乱龙血, 11);
        //技能配置切换("仅龙血时使用", ref PvPDRGSettings.Instance.樱花缭乱龙血, 1);

        // 技能3: 武神枪
        // 技能配置界面(SkillSwitchType.Switch2, DRGSkillID.武神枪,"武神枪","","这里并没有配置", ref 开关,5, ref 数值,0,0);

        // 技能4: 死者之岸
        自定义.死者之岸();

        // 技能5: 高跳
        配置Int1Bool1(DRGSkillID.高跳, "高跳", "使用时目标距离多少米以内", "仅龙血才使用", ref PvPDRGSettings.Instance.高跳龙血, ref PvPDRGSettings.Instance.高跳范围, 1, 2, 12);

        // 技能6: 苍穹刺
        配置Bool1(DRGSkillID.苍穹刺, "苍穹刺", "仅龙血才能使用", ref PvPDRGSettings.Instance.苍穹刺龙血, 15);

        // 技能7: 后跳
       // 配置Bool1(DRGSkillID.后跳, "后跳", "被控时主动使用后跳[失效待修复]", ref PvPDRGSettings.Instance.后跳解除控制, 55);
       // 配置Bool1(DRGSkillID.后跳, "后跳", "往镜头方向后跳(技能以及热键)", ref PvPDRGSettings.Instance.后跳面向, 56);

        // 技能8: 天龙点睛
        PvPDRGSettings.Instance.天龙点睛主目标距离 = Math.Clamp(PvPDRGSettings.Instance.天龙点睛主目标距离, 0, 25);
        配置Int1Bool1(DRGSkillID.天龙点睛, "天龙点睛", "使用时至少距离目标多少米", "仅龙血才使用", ref PvPDRGSettings.Instance.天龙龙血, ref PvPDRGSettings.Instance.天龙点睛主目标距离, 1, 5, 13);

        // 技能9: 恐惧咆哮
        PvPDRGSettings.Instance.恐惧咆哮人数 = Math.Clamp(PvPDRGSettings.Instance.恐惧咆哮人数, 1, 48);
        配置Int1Bool1(DRGSkillID.恐惧咆哮, "恐惧咆哮", "使用时周围敌人人数", "仅龙血才使用", ref PvPDRGSettings.Instance.恐惧咆哮x龙血, ref PvPDRGSettings.Instance.恐惧咆哮人数, 1, 2, 14);
        PvPDRGSettings.Instance.Save();
    }
    public void 配置机工技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPMCHSettings.Instance.药血量 = Math.Clamp(PvPMCHSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPMCHSettings.Instance.药血量, 5, 10, 77);

        // 技能组: 钻头套装
        配置Bool1(29405u, "钻头", "仅分析使用", ref PvPMCHSettings.Instance.钻头分析, 1);
        配置Bool1(29406u, "毒菌冲击", "仅分析使用", ref PvPMCHSettings.Instance.毒菌分析, 2);
        配置Bool1(29407u, "空气锚", "仅分析使用", ref PvPMCHSettings.Instance.空气锚分析, 3);
        配置Bool1(29408u, "回转飞锯", "仅分析使用", ref PvPMCHSettings.Instance.回转飞锯分析, 4);

        // 技能3: 分析
        配置Bool1(29414u, "分析", "钻头套装可用才使用分析", ref PvPMCHSettings.Instance.分析可用, 5);

        // 技能4: 野火
        配置Bool1(29409u, "野火", "仅过热状态时使用", ref PvPMCHSettings.Instance.过热野火, 6);

        // 技能7: 全金属爆发
        配置Bool1(41469u, "全金属爆发", "仅野火期间使用", ref PvPMCHSettings.Instance.金属爆发仅野火, 12);

        // 技能6: 整活

        配置Bool1(41468u, "烈焰弹(整活)", "使用旧版热冲击(只会降低2000威力)", ref PvPMCHSettings.Instance.热冲击, 10);

        // 技能5: LB
        配置Bool1(29415u, "魔弹射手(热键)", "使用智能目标", ref PvPMCHSettings.Instance.智能魔弹, 7);
        PvPMCHSettings.Instance.Save();
    }
    public void 配置画家技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPPCTSettings.Instance.药血量 = Math.Clamp(PvPPCTSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPPCTSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 坦培拉涂层
        PvPPCTSettings.Instance.盾自身血量 = Math.Clamp(PvPPCTSettings.Instance.盾自身血量, 0.01f, 1.0f);
        配置Float1(PCTSkillID.技能坦培拉涂层, "坦培拉涂层", "自身血量", ref PvPPCTSettings.Instance.盾自身血量, 0.01f, 1.0f, 1);

        // 技能3: 减色混合
        配置Int1(PCTSkillID.技能减色混合, "减色混合", "最低切换间隔(毫秒)", ref PvPPCTSettings.Instance.减色切换, 100, 1000, 76);
        PvPPCTSettings.Instance.Save();
    }
    public void 配置赤魔技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPRDMSettings.Instance.药血量 = Math.Clamp(PvPRDMSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPRDMSettings.Instance.药血量, 3, 10, 77);
        // 剑身强部
        自定义.剑身强部();
        // 鼓励
        PvPRDMSettings.Instance.鼓励人数 = Math.Clamp(PvPRDMSettings.Instance.鼓励人数, 1, 8);
        配置Int1(41494u, "鼓励", "30米内队友人数", ref PvPRDMSettings.Instance.鼓励人数, 1, 2, 556);
        // 光芒四射
        配置Bool1(41495u, "光芒四射", "仅鼓励期间释放", ref PvPRDMSettings.Instance.鼓励光芒四射, 90);
        // 决断
        配置Bool1(41492U, "决断", "仅鼓励期间释放", ref PvPRDMSettings.Instance.鼓励决断, 95);
        // 技能3: LB
        配置Bool1(41498u, "南天十字(热键)", "以自己为目标", ref PvPRDMSettings.Instance.南天自己, 7);
        PvPRDMSettings.Instance.Save();
    }
    public void 配置黑魔技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPBLMSettings.Instance.药血量 = Math.Clamp(PvPBLMSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPBLMSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 磁暴
        自定义.磁暴();

        // 技能3: 昏沉
        PvPBLMSettings.Instance.昏沉 = Math.Clamp(PvPBLMSettings.Instance.昏沉, 0, 1000);
        配置Int1(41510, "昏沉(插入时机)", "数值越低越优先推荐0", ref PvPBLMSettings.Instance.昏沉, 10, 100, 1414);
        // 技能4: 元素天赋
        配置Bool1(41475u, "元素天赋(默认烈火环)", "(开烈火环时会打龟壳)允许寒冰环", ref PvPBLMSettings.Instance.寒冰环, 1112);
        // PVPHelper.技能图标(29651u); 
        // ImGui.SameLine();  
        // ImGui.Text("核爆"); 
        // ImGui.Checkbox("寻找最佳AOE目标", ref PvPBLMSettings.Instance.最佳AOE); 
        // ImGui.InputInt("AOE人数", ref PvPBLMSettings.Instance.最佳人数, 1, 5); 

        // 技能5: 冰火切换时机
        配置Int1(29663u, "冰火切换(冰判定时间)", "数值越低打的冰越少(推荐50~300)", ref PvPBLMSettings.Instance.冰时间, 10, 100, 1412);

        配置解释(29662u, "灵魂共鸣(技能逻辑说明)", "LB需要手动按 只打火大招和火耀星(有QT开关)");
        PvPBLMSettings.Instance.Save();
    }
    public void 配置武士技能()
    {
        权限获取();
        ImGui.Checkbox("斩铁日志调试模式", ref PvPSAMSettings.Instance.斩铁调试);
        if (PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数) != null && PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数) != Core.Me)
        {
            ImGui.Text($"多斩目标：{PVPTargetHelper.TargetSelector.Get多斩Target(PvPSAMSettings.Instance.多斩人数)}");
        }
        if (PVPTargetHelper.TargetSelector.Get斩铁目标() != null && PVPTargetHelper.TargetSelector.Get斩铁目标() != Core.Me)
        {
            ImGui.Text($"斩铁目标：{PVPTargetHelper.TargetSelector.Get斩铁目标().Name}");
        }
        ImGui.Separator();
        // 技能1: 喝热水
        PvPSAMSettings.Instance.药血量 = Math.Clamp(PvPSAMSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPSAMSettings.Instance.药血量, 3, 10, 5);

        // 技能2: 斩浪雪月花
        配置Bool1(29530u, "斩浪&雪月花", "移动时不读条", ref PvPSAMSettings.Instance.读条检查, 1);

        // 技能3: 地天
        自定义.地天();

        // 技能4: 斩铁
        自定义.斩铁();
        PvPSAMSettings.Instance.Save();
    }
    public void 配置诗人技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPBRDSettings.Instance.药血量 = Math.Clamp(PvPBRDSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "喝热水", "热水阈值", ref PvPBRDSettings.Instance.药血量, 3, 10, 5);

        // 技能2: 和弦箭
        PvPBRDSettings.Instance.和弦箭 = Math.Clamp(PvPBRDSettings.Instance.和弦箭, 1, 4);
        配置Int1(41464U, "和弦箭", "使用层数", ref PvPBRDSettings.Instance.和弦箭, 1, 1, 87);

        // 技能3: 光阴神
        自定义.光阴神();
        PvPBRDSettings.Instance.Save();
    }
    public void 配置召唤技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPSMNSettings.Instance.药血量 = Math.Clamp(PvPSMNSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "癒しの水", "热水阈值", ref PvPSMNSettings.Instance.药血量, 3, 10, 1);

        // 技能2: 火神冲
        PvPSMNSettings.Instance.火神冲 = Math.Clamp(PvPSMNSettings.Instance.火神冲, 0, 28);
        配置Int1(29667u, "猛き炎", "火神冲敌人距离", ref PvPSMNSettings.Instance.火神冲, 1, 5, 2);

        // 技能3: 坏死爆发
        配置Bool1(41483u, "坏死爆发", "有毁绝预备时不使用", ref PvPSMNSettings.Instance.毁绝不重复,41483);

        自定义.守护之光();
        PvPSMNSettings.Instance.Save();
    }
    public void 配置绝枪技能()
    {
        权限获取();
        ImGui.Text("当前职业职能技能推荐使用:铁壁");
        // 技能1: 喝热水
        PvPGNBSettings.Instance.药血量 = Math.Clamp(PvPGNBSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "自愈", "自愈阈值", ref PvPGNBSettings.Instance.药血量, 3, 10, 1);

        // 技能2: 粗分斩
        PvPGNBSettings.Instance.粗分斩最大距离 = Math.Clamp(PvPGNBSettings.Instance.粗分斩最大距离, 0, 20);
        配置Int1(29123u, "粗分斩", "离目标()米以内使用", ref PvPGNBSettings.Instance.粗分斩最大距离, 1, 5, 2);

        // 技能1: 喝热水
        PvPGNBSettings.Instance.爆破血量 = Math.Clamp(PvPGNBSettings.Instance.爆破血量, 1, 100);
        配置Int1(29128u, "爆破领域", "目标血量多少才使用", ref PvPGNBSettings.Instance.爆破血量, 3, 10, 3);

        自定义.刚玉之心();
        PvPGNBSettings.Instance.Save();
    }
    public void 配置蛇蛇技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPVPRSettings.Instance.药血量 = Math.Clamp(PvPVPRSettings.Instance.药血量, 1, 100);
        配置Int1(29711u, "蛇喝水", "热水阈值", ref PvPVPRSettings.Instance.药血量, 3, 10, 1);
        
        PvPVPRSettings.Instance.Save();
    }
    public void 配置测试()
    {
        权限获取();
       
        
        PvPSMNSettings.Instance.火神冲 = Math.Clamp(PvPSMNSettings.Instance.火神冲, 0, 28);
        配置Int1(29667u, "猛き炎", "火神冲敌人距离", ref PvPSMNSettings.Instance.火神冲, 1, 5, 2);
        PvPSMNSettings.Instance.Save();
    }
}
