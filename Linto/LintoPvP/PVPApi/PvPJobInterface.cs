using AEAssist;
using AEAssist.Helper;
using Dalamud.Bindings.ImGui;
using Linto.LintoPvP.BLM;
using Linto.LintoPvP.DRG;
using Linto.LintoPvP.GNB;
using Linto.LintoPvP.PCT;
using Linto.LintoPvP.RDM;
using Linto.LintoPvP.SAM;
using Linto.LintoPvP.SMN;

namespace Linto.LintoPvP.PVPApi;
/// <summary>
/// 技能配置切换类型
/// </summary>
public enum SkillSwitchType
{
    /// <summary>
    /// 对应 PVPHelper.技能配置切换:bool开关
    /// </summary>
    Switch1 = 1,
    /// <summary>
    /// 对应 PVPHelper.技能配置切换2:bool开关
    /// </summary>
    Switch2 = 2,
    /// <summary>
    /// 对应 PVPHelper.技能配置切换3:int数值调整
    /// </summary>
    Switch3 = 3,
    /// <summary>
    /// 对应 PVPHelper.技能配置切换4:bool开关+int数值调整
    /// </summary>
    Switch4 = 4,
}
/// <summary>
/// 职业悬浮窗配置接口
/// </summary>
public interface PvPJobInterface
{
    /// <summary>
    /// 获取当前权限配置
    /// </summary>
    void 权限获取();
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
    /// 配置技能界面
    /// </summary>
    /// <param name="技能ID">技能ID</param>
    /// <param name="技能名">技能名</param>
    /// <param name="描述">这是一段话</param>
    /// <param name="设置状态">一般是保存设置里的bool</param>
    /// <param name="标识符">随便填个数字吧</param>
    /// <param name="技能切换类型"></param>
    public virtual void ConfigureSkillBool(uint SkillID, string SkillName, string description, ref bool variable, int ID)
    {
        PVPHelper.技能配置2(SkillID, SkillName, description, ref variable, ID);
    }

    public virtual void ConfigureSkillInt(uint SkillID, string SkillName, string description, ref int value, int step, int quickstep, int id)
    {
        PVPHelper.技能配置3(SkillID, SkillName, description, ref value, step, quickstep, id);
    }

    public virtual void ConfigureSkillBoolInt(uint SkillID, string SkillName, string IntDescription, string description, ref bool status, ref int value, int step, int quickstep, int id)
    {
        PVPHelper.技能配置4(SkillID, SkillName, IntDescription, description, ref status, ref value, step, quickstep, id);
    }
    public virtual void ConfigureSkillSliderFloat(uint SkillID, string SkillName, string IntDescription, ref float value, float min, float max, int id)
    {
        PVPHelper.技能配置5(SkillID, SkillName, IntDescription, ref value, min, max, id);
    }
    public virtual void ConfigureSkilldescription(uint SkillID, string SkillName, string description)
    {
        PVPHelper.技能解释(SkillID, SkillName, description);
    }
}
public class 职业配置 : BaseJobConfig
{
    public class 自定义
    {
        public static void 死者之岸()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(DRGSkillID.死者之岸);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("死者之岸");
            ImGui.Text($"有樱花缭乱时不用");
            ImGui.Checkbox($"##{2}", ref PvPDRGSettings.Instance.死者之岸樱花);
            ImGui.Text($"有天龙点睛时不用");
            ImGui.Checkbox($"##{3}", ref PvPDRGSettings.Instance.死者之岸天龙);
            ImGui.Text($"有苍穹刺时不用");
            ImGui.Checkbox($"##{4}", ref PvPDRGSettings.Instance.死者之岸苍穹刺);
            ImGui.Columns(1);
        }
        public static void 磁暴()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(29657);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("磁暴");
            PvPBLMSettings.Instance.磁暴敌人距离 = Math.Clamp(PvPBLMSettings.Instance.磁暴敌人距离, 5, 10);
            PvPBLMSettings.Instance.磁暴敌人数量 = Math.Clamp(PvPBLMSettings.Instance.磁暴敌人数量, 1, 48);
            ImGui.Text($"多少米范围内");
            ImGui.InputInt($"##{88}", ref PvPBLMSettings.Instance.磁暴敌人距离, 1, 5);
            ImGui.Text($"存在敌人数量");
            ImGui.InputInt($"##{99}", ref PvPBLMSettings.Instance.磁暴敌人数量, 1, 5);
            ImGui.Columns(1);
        }
        public static void 剑身强部()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(41496);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("剑身强部");
            PvPRDMSettings.Instance.护盾距离 = Math.Clamp(PvPRDMSettings.Instance.护盾距离, 0, 30);
            PvPRDMSettings.Instance.护盾人数 = Math.Clamp(PvPRDMSettings.Instance.护盾人数, 1, 48);
            ImGui.Text($"多少米范围内");
            ImGui.InputInt($"##{8787}", ref PvPRDMSettings.Instance.护盾距离, 1, 5);
            ImGui.Text($"存在敌人数量");
            ImGui.InputInt($"##{6565}", ref PvPRDMSettings.Instance.护盾人数, 1, 5);
            ImGui.Columns(1);
        }
        public static void 后跳()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(DRGSkillID.后跳);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("后跳");
            ImGui.Text($"被控时主动使用后跳");
            ImGui.Checkbox($"##{55}", ref PvPDRGSettings.Instance.后跳解除控制);
            ImGui.Text($"往镜头方向后跳");
            ImGui.Checkbox($"##{68}", ref PvPDRGSettings.Instance.后跳面向);
            ImGui.Columns(1);
        }
        public static void 地天()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(29533u);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("地天");
            PvPSAMSettings.Instance.周围敌人数量 = Math.Clamp(PvPSAMSettings.Instance.周围敌人数量, 1, 48);
            ImGui.Text($"10米内人数大于");
            ImGui.InputInt($"##{55}", ref PvPSAMSettings.Instance.周围敌人数量, 1, 100);
            PvPSAMSettings.Instance.地天自身血量 = Math.Clamp(PvPSAMSettings.Instance.地天自身血量, 0.01f, 1.0f);
            ImGui.Text($"自身血量小于");
            ImGui.SliderFloat($"##{58}", ref PvPSAMSettings.Instance.地天自身血量, 0.01f, 1.0f);
            ImGui.Columns(1);
        }
        public static void 斩铁()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(29537u);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 200);
            ImGui.Text("斩铁");
            ImGui.Text("确认不被控再使用");
            ImGui.Checkbox($"##{41}", ref PvPSAMSettings.Instance.斩铁检查状态);
            ImGui.Text("多斩(推荐去用PvPAuto)");
            ImGui.Checkbox($"##{42}", ref PvPSAMSettings.Instance.多斩模式);
            PvPSAMSettings.Instance.多斩人数 = Math.Clamp(PvPSAMSettings.Instance.多斩人数, 2, 48);
            ImGui.Text("多斩人数");
            ImGui.InputInt($"##{43}", ref PvPSAMSettings.Instance.多斩人数, 1, 100);
            ImGui.Columns(1);
        }
        public static void 光阴神()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(29400U);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("光阴神");
            ImGui.Text("解控队友|聊天框打印:");
            PvPBRDSettings.Instance.光阴对象 = Math.Clamp(PvPBRDSettings.Instance.光阴对象, 0, PartyHelper.Party.Count - 1);
            ImGui.Checkbox($"##{1}", ref PvPBRDSettings.Instance.光阴队友);
            ImGui.SameLine();
            ImGui.Checkbox($"##{54}", ref PvPBRDSettings.Instance.光阴播报);
            ImGui.Text("优先玩家名");
            ImGui.InputText($"##{678}", ref PvPBRDSettings.Instance.优先对象, 10);
            var targetMember = PartyHelper.Party
                .FirstOrDefault(x => x?.Name?.TextValue == PvPBRDSettings.Instance.优先对象);
            if (targetMember != null)
            {
                ImGui.Text("优先对象:");
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPBRDSettings.Instance.光阴对象 = targetIndex;
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPBRDSettings.Instance.光阴对象 >= 0 && PvPBRDSettings.Instance.光阴对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
            }
            else
            {
                ImGui.Text("优先对象:");
                ImGui.InputInt($"##{78}", ref PvPBRDSettings.Instance.光阴对象, 1, 100);
                PvPBRDSettings.Instance.光阴对象 = Math.Clamp(PvPBRDSettings.Instance.光阴对象, 0, PartyHelper.Party.Count - 1);
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPBRDSettings.Instance.光阴对象 >= 0 && PvPBRDSettings.Instance.光阴对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
                if (ImGui.Button("优先玩家名设定该对象"))
                    PvPBRDSettings.Instance.优先对象 = PartyHelper.Party[PvPBRDSettings.Instance.光阴对象].Name.TextValue;

            }

            ImGui.Columns(1);
        }
        public static void 守护之光()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(29670U);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("守护之光");
            PvPSMNSettings.Instance.守护之光血量 = Math.Clamp(PvPSMNSettings.Instance.守护之光血量, 1, 101);
            ImGui.InputInt($"血量阈值##{6666}", ref PvPSMNSettings.Instance.守护之光血量, 10, 20);
            ImGui.Text("守护队友|聊天框打印:");
            PvPSMNSettings.Instance.守护对象 = Math.Clamp(PvPSMNSettings.Instance.守护对象, 0, PartyHelper.Party.Count - 1);
            ImGui.Checkbox($"##{45454}", ref PvPSMNSettings.Instance.守护队友);
            ImGui.SameLine();
            ImGui.Checkbox($"##{54}", ref PvPSMNSettings.Instance.守护播报);
            ImGui.Text("优先玩家名");
            ImGui.InputText($"##{678}", ref PvPSMNSettings.Instance.优先对象, 10);
            var targetMember = PartyHelper.Party
                .FirstOrDefault(x => x?.Name?.TextValue == PvPSMNSettings.Instance.优先对象);
            if (targetMember != null)
            {
                ImGui.Text("优先对象:");
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPSMNSettings.Instance.守护对象 = targetIndex;
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPSMNSettings.Instance.守护对象 >= 0 && PvPSMNSettings.Instance.守护对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPSMNSettings.Instance.守护对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
            }
            else
            {
                ImGui.Text("优先对象:");
                ImGui.InputInt($"##{78}", ref PvPSMNSettings.Instance.守护对象, 1, 100);
                PvPSMNSettings.Instance.守护对象 = Math.Clamp(PvPSMNSettings.Instance.守护对象, 0, PartyHelper.Party.Count - 1);
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPSMNSettings.Instance.守护对象 >= 0 && PvPSMNSettings.Instance.守护对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPSMNSettings.Instance.守护对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
                if (ImGui.Button("优先玩家名设定该对象"))
                    PvPSMNSettings.Instance.优先对象 = PartyHelper.Party[PvPSMNSettings.Instance.守护对象].Name.TextValue;

            }

            ImGui.Columns(1);
        }
        public static void 刚玉之心()
        {
            ImGui.Separator();
            ImGui.Columns(2, ImU8String.Empty, false);
            ImGui.SetColumnWidth(0, 70);
            PVPHelper.技能图标(41443U);
            ImGui.NextColumn();
            ImGui.SetColumnWidth(1, 150);
            ImGui.Text("刚玉之心");
            PvPGNBSettings.Instance.刚玉血量 = Math.Clamp(PvPGNBSettings.Instance.刚玉血量, 1, 101);
            ImGui.InputInt($"血量阈值##{6666}", ref PvPGNBSettings.Instance.刚玉血量, 10, 20);
            ImGui.Text("刚玉队友|聊天框打印:");
            PvPGNBSettings.Instance.刚玉对象 = Math.Clamp(PvPGNBSettings.Instance.刚玉对象, 0, PartyHelper.Party.Count - 1);
            ImGui.Checkbox($"##{45454}", ref PvPGNBSettings.Instance.刚玉队友);
            ImGui.SameLine();
            ImGui.Checkbox($"##{54}", ref PvPGNBSettings.Instance.刚玉播报);
            ImGui.Text("优先玩家名");
            ImGui.InputText($"##{678}", ref PvPGNBSettings.Instance.优先对象, 10);
            var targetMember = PartyHelper.Party
                .FirstOrDefault(x => x?.Name?.TextValue == PvPGNBSettings.Instance.优先对象);
            if (targetMember != null)
            {
                ImGui.Text("优先对象:");
                int targetIndex = PartyHelper.Party.IndexOf(targetMember);
                PvPGNBSettings.Instance.刚玉对象 = targetIndex;
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPGNBSettings.Instance.刚玉对象 >= 0 && PvPGNBSettings.Instance.刚玉对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
            }
            else
            {
                ImGui.Text("优先对象:");
                ImGui.InputInt($"##{78}", ref PvPGNBSettings.Instance.刚玉对象, 1, 100);
                PvPGNBSettings.Instance.刚玉对象 = Math.Clamp(PvPGNBSettings.Instance.刚玉对象, 0, PartyHelper.Party.Count - 1);
                ImGui.SameLine();
                if (PartyHelper.Party != null && PartyHelper.Party.Count > 0 && PvPGNBSettings.Instance.刚玉对象 >= 0 && PvPGNBSettings.Instance.刚玉对象 < PartyHelper.Party.Count)
                {
                    var partyMember = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象];
                    ImGui.Text($"{partyMember?.Name ?? "未存在此玩家"}");
                }
                else
                {
                    ImGui.Text("未存在此玩家");
                }
                if (ImGui.Button("优先玩家名设定该对象"))
                    PvPGNBSettings.Instance.优先对象 = PartyHelper.Party[PvPGNBSettings.Instance.刚玉对象].Name.TextValue;

            }

            ImGui.Columns(1);
        }
    }
    public void 配置龙骑技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPDRGSettings.Instance.药血量 = Math.Clamp(PvPDRGSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPDRGSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 樱花缭乱
        ConfigureSkillBool(DRGSkillID.樱花缭乱, "樱花缭乱", "仅龙血时使用", ref PvPDRGSettings.Instance.樱花缭乱龙血, 11);
        //技能配置切换("仅龙血时使用", ref PvPDRGSettings.Instance.樱花缭乱龙血, 1);

        // 技能3: 武神枪
        // 技能配置界面(SkillSwitchType.Switch2, DRGSkillID.武神枪,"武神枪","","这里并没有配置", ref 开关,5, ref 数值,0,0);

        // 技能4: 死者之岸
        自定义.死者之岸();

        // 技能5: 高跳
        ConfigureSkillBoolInt(DRGSkillID.高跳, "高跳", "使用时目标距离多少米以内", "仅龙血才使用", ref PvPDRGSettings.Instance.高跳龙血, ref PvPDRGSettings.Instance.高跳范围, 1, 2, 12);

        // 技能6: 苍穹刺
        ConfigureSkillBool(DRGSkillID.苍穹刺, "苍穹刺", "仅龙血才能使用", ref PvPDRGSettings.Instance.苍穹刺龙血, 15);

        // 技能7: 后跳
        自定义.后跳();

        // 技能8: 天龙点睛
        PvPDRGSettings.Instance.天龙点睛主目标距离 = Math.Clamp(PvPDRGSettings.Instance.天龙点睛主目标距离, 0, 25);
        ConfigureSkillBoolInt(DRGSkillID.天龙点睛, "天龙点睛", "使用时至少距离目标多少米", "仅龙血才使用", ref PvPDRGSettings.Instance.天龙龙血, ref PvPDRGSettings.Instance.天龙点睛主目标距离, 1, 5, 13);

        // 技能9: 恐惧咆哮
        PvPDRGSettings.Instance.恐惧咆哮人数 = Math.Clamp(PvPDRGSettings.Instance.恐惧咆哮人数, 1, 48);
        ConfigureSkillBoolInt(DRGSkillID.恐惧咆哮, "恐惧咆哮", "使用时周围敌人人数", "仅龙血才使用", ref PvPDRGSettings.Instance.恐惧咆哮x龙血, ref PvPDRGSettings.Instance.恐惧咆哮人数, 1, 2, 14);
        PvPDRGSettings.Instance.Save();
    }
    public void 配置机工技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPMCHSettings.Instance.药血量 = Math.Clamp(PvPMCHSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPMCHSettings.Instance.药血量, 5, 10, 77);

        // 技能组: 钻头套装
        ConfigureSkillBool(29405u, "钻头", "仅分析使用", ref PvPMCHSettings.Instance.钻头分析, 1);
        ConfigureSkillBool(29406u, "毒菌冲击", "仅分析使用", ref PvPMCHSettings.Instance.毒菌分析, 2);
        ConfigureSkillBool(29407u, "空气锚", "仅分析使用", ref PvPMCHSettings.Instance.空气锚分析, 3);
        ConfigureSkillBool(29408u, "回转飞锯", "仅分析使用", ref PvPMCHSettings.Instance.回转飞锯分析, 4);

        // 技能3: 分析
        ConfigureSkillBool(29414u, "分析", "钻头套装可用才使用分析", ref PvPMCHSettings.Instance.分析可用, 5);

        // 技能4: 野火
        ConfigureSkillBool(29409u, "野火", "仅过热状态时使用", ref PvPMCHSettings.Instance.过热野火, 6);

        // 技能7: 全金属爆发
        ConfigureSkillBool(41469u, "全金属爆发", "仅野火期间使用", ref PvPMCHSettings.Instance.金属爆发仅野火, 12);

        // 技能6: 整活

        ConfigureSkillBool(41468u, "烈焰弹(整活)", "使用旧版热冲击\n(只会降低2000威力)", ref PvPMCHSettings.Instance.热冲击, 10);

        // 技能5: LB
        ConfigureSkillBool(29415u, "魔弹射手(热键)", "使用智能目标", ref PvPMCHSettings.Instance.智能魔弹, 7);
        PvPMCHSettings.Instance.Save();
    }
    public void 配置画家技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPPCTSettings.Instance.药血量 = Math.Clamp(PvPPCTSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPPCTSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 坦培拉涂层
        PvPPCTSettings.Instance.盾自身血量 = Math.Clamp(PvPPCTSettings.Instance.盾自身血量, 0.01f, 1.0f);
        ConfigureSkillSliderFloat(PCTSkillID.技能坦培拉涂层, "坦培拉涂层", "自身血量", ref PvPPCTSettings.Instance.盾自身血量, 0.01f, 1.0f, 1);

        // 技能3: 减色混合
        ConfigureSkillInt(PCTSkillID.技能减色混合, "减色混合", "最低切换间隔(毫秒)", ref PvPPCTSettings.Instance.减色切换, 100, 1000, 76);
        PvPPCTSettings.Instance.Save();
    }
    public void 配置赤魔技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPRDMSettings.Instance.药血量 = Math.Clamp(PvPRDMSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPRDMSettings.Instance.药血量, 3, 10, 77);
        // 剑身强部
        自定义.剑身强部();
        // 鼓励
        PvPRDMSettings.Instance.鼓励人数 = Math.Clamp(PvPRDMSettings.Instance.鼓励人数, 1, 8);
        ConfigureSkillInt(41494u, "鼓励", "30米内队友人数", ref PvPRDMSettings.Instance.鼓励人数, 1, 2, 556);
        // 光芒四射
        ConfigureSkillBool(41495u, "光芒四射", "仅鼓励期间释放", ref PvPRDMSettings.Instance.鼓励光芒四射, 90);
        // 决断
        ConfigureSkillBool(41492U, "决断", "仅鼓励期间释放", ref PvPRDMSettings.Instance.鼓励决断, 95);
        // 技能3: LB
        ConfigureSkillBool(41498u, "南天十字(热键)", "以自己为目标", ref PvPRDMSettings.Instance.南天自己, 7);
        PvPRDMSettings.Instance.Save();
    }
    public void 配置黑魔技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPBLMSettings.Instance.药血量 = Math.Clamp(PvPBLMSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPBLMSettings.Instance.药血量, 3, 10, 77);

        // 技能2: 磁暴
        自定义.磁暴();

        // 技能3: 昏沉
        PvPBLMSettings.Instance.昏沉 = Math.Clamp(PvPBLMSettings.Instance.昏沉, 0, 1000);
        ConfigureSkillInt(41510, "昏沉(插入时机)", "数值越低越优先\n推荐0", ref PvPBLMSettings.Instance.昏沉, 10, 100, 1414);
        // 技能4: 元素天赋
        ConfigureSkillBool(41475u, "元素天赋(默认烈火环)", "开烈火环时不会排除龟壳\n允许寒冰环", ref PvPBLMSettings.Instance.寒冰环, 1112);
        // PVPHelper.技能图标(29651u); 
        // ImGui.SameLine();  
        // ImGui.Text("核爆"); 
        // ImGui.Checkbox("寻找最佳AOE目标", ref PvPBLMSettings.Instance.最佳AOE); 
        // ImGui.InputInt("AOE人数", ref PvPBLMSettings.Instance.最佳人数, 1, 5); 

        // 技能5: 冰火切换时机
        ConfigureSkillInt(29663u, "冰火切换(冰判定时间)", "数值越低打的冰越少\n推荐50~300", ref PvPBLMSettings.Instance.冰时间, 10, 100, 1412);

        ConfigureSkilldescription(29662u, "灵魂共鸣(技能逻辑说明)", "LB需要手动按\n仅自动耀星(有QT开关)\n仅使用核爆\n移动时仅打冰连击");
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
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPSAMSettings.Instance.药血量, 3, 10, 5);

        // 技能2: 斩浪雪月花
        ConfigureSkillBool(29530u, "斩浪&雪月花", "移动时不读条", ref PvPSAMSettings.Instance.读条检查, 1);

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
        ConfigureSkillInt(29711u, "喝热水", "热水阈值", ref PvPBRDSettings.Instance.药血量, 3, 10, 5);

        // 技能2: 和弦箭
        PvPBRDSettings.Instance.和弦箭 = Math.Clamp(PvPBRDSettings.Instance.和弦箭, 1, 4);
        ConfigureSkillInt(41464U, "和弦箭", "使用层数", ref PvPBRDSettings.Instance.和弦箭, 1, 1, 87);

        // 技能3: 光阴神
        自定义.光阴神();
        PvPBRDSettings.Instance.Save();
    }
    public void 配置召唤技能()
    {
        权限获取();
        // 技能1: 喝热水
        PvPSMNSettings.Instance.药血量 = Math.Clamp(PvPSMNSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "癒しの水", "热水阈值", ref PvPSMNSettings.Instance.药血量, 3, 10, 1);

        // 技能2: 火神冲
        PvPSMNSettings.Instance.火神冲 = Math.Clamp(PvPSMNSettings.Instance.火神冲, 0, 28);
        ConfigureSkillInt(29667u, "猛き炎", "火神冲敌人距离", ref PvPSMNSettings.Instance.火神冲, 1, 5, 2);

        // 技能3: 坏死爆发
        PvPSMNSettings.Instance.溃烂阈值 = Math.Clamp(PvPSMNSettings.Instance.溃烂阈值, 0.1f, 1f);
        ConfigureSkillSliderFloat(41483u, "坏死爆发", "敌人血量阈值", ref PvPSMNSettings.Instance.溃烂阈值, 0.1f, 1f, 3);

        自定义.守护之光();
        PvPSMNSettings.Instance.Save();
    }
    public void 配置绝枪技能()
    {
        权限获取();
        ImGui.Text("当前职业职能技能推荐使用:铁壁");
        // 技能1: 喝热水
        PvPGNBSettings.Instance.药血量 = Math.Clamp(PvPGNBSettings.Instance.药血量, 1, 100);
        ConfigureSkillInt(29711u, "自愈", "自愈阈值", ref PvPGNBSettings.Instance.药血量, 3, 10, 1);

        // 技能2: 粗分斩
        PvPGNBSettings.Instance.粗分斩最大距离 = Math.Clamp(PvPGNBSettings.Instance.粗分斩最大距离, 0, 20);
        ConfigureSkillInt(29123u, "粗分斩", "离目标()米以内使用", ref PvPGNBSettings.Instance.粗分斩最大距离, 1, 5, 2);

        // 技能1: 喝热水
        PvPGNBSettings.Instance.爆破血量 = Math.Clamp(PvPGNBSettings.Instance.爆破血量, 1, 100);
        ConfigureSkillInt(29128u, "爆破领域", "目标血量多少才使用", ref PvPGNBSettings.Instance.爆破血量, 3, 10, 3);

        自定义.刚玉之心();
        PvPGNBSettings.Instance.Save();
    }
}
