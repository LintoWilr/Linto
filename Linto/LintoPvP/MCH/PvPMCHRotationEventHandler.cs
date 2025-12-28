using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.Command;
using Linto.LintoPvP.PVPApi;
using Linto.LintoPvP.PVPApi.PVPApi.Target;

namespace Linto.LintoPvP.MCH;

public class PvPMCHRotationEventHandler : IRotationEventHandler
{
    public void OnTerritoryChanged()
    {

    }
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {

    }
    public void OnResetBattle()
    {
        PvPMCHBattleData.Instance.Reset();
    }
    public async Task OnPreCombat()
    {
        PVPTargetHelper.自动选中();
        if (PvPSettings.Instance.无目标坐骑)
        {
            MountHandler.无目标坐骑();
        }
        await Task.CompletedTask;
    }
    public async Task OnNoTarget()
    {
        var slot = new Slot();
        PVPTargetHelper.自动选中();
        if (PvPSettings.Instance.无目标坐骑)
        {
            MountHandler.无目标坐骑();
        }
        await Task.CompletedTask;
    }

	public void AfterSpell(Slot slot, Spell spell)
	{
		uint id = spell.Id;
	}
    public void BeforeSpell(Slot slot, Spell spell)
    {
        if(Core.Me.GetCurrTarget()==null)return;
        var 距离 = Core.Me.GetCurrTarget().DistanceToPlayer();
        if (PvPSettings.Instance.诊断模式)
            LogHelper.Print(AI.Instance.BattleData.CurrBattleTimeInSec + ",释放技能:" + spell.Name + ":" + spell.Id + 
                $"目标：{Core.Me.GetCurrTarget().Name},血量：{Core.Me.GetCurrTarget().CurrentHp}，距离：{距离}");
    }
    public void OnBattleUpdate(int currTime)
	{
		PVPHelper.战斗状态();
		PVPTargetHelper.自动选中();
	}
    #region 宏支持相关

    public Dictionary<string, string>? qtKeyDictionary;

    public Dictionary<string, string?> hotkeyDictionary = new(StringComparer.OrdinalIgnoreCase)
    {
        { "疾跑", "疾跑"},{ "冲刺", "疾跑"},{ "龟壳", "龟壳"},
        { "热水", "热水" },{ "LB", "LB" },
        { "霰弹枪", "霰弹枪" }
    };
    public void BulidQtKeyDictionary() // 创建QT对应命令
    {
        qtKeyDictionary = new(StringComparer.OrdinalIgnoreCase);
        foreach (var fi in typeof(QTKey).GetFields(
                 System.Reflection.BindingFlags.Public |
                 System.Reflection.BindingFlags.Static |
                 System.Reflection.BindingFlags.FlattenHierarchy))
        {
            if (fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            {
                string value = fi.GetValue(null).ToString();
                string key = value.ToLower();
                string fieldName = fi.Name.ToLower();

                if (!qtKeyDictionary.ContainsKey(key))
                    qtKeyDictionary.Add(key, value);
                if (!qtKeyDictionary.ContainsKey(fieldName))
                    qtKeyDictionary.Add(fieldName, value);
            }
        }
    }
    private void PvPMCHCommandHandler(string command, string args)
    {
        //LogHelper.Print($"1: 收到命令 -> 主命令：{command}，参数：{args}"); // 原日志保留，新增下面的日志
        if (string.IsNullOrWhiteSpace(args))
        {
            LogHelper.Print("Linto_MCH 命令无效，请提供参数。");
            return;
        }

        var lowerArgs = args.Trim().ToLower();
        //LogHelper.Print($"2: 参数小写处理后：{lowerArgs}"); // 新增：打印处理后的参数
        if (lowerArgs.EndsWith("_qt"))
        {
            var keyPart = lowerArgs[..^3];
            var matchedKey = GetMatchingQtKey(keyPart);
            ToggleQtSetting(matchedKey);
            return;
        }

        if (lowerArgs.EndsWith("_hk"))
        {
            var keyPart = lowerArgs[..^3];

            //LogHelper.Print($"3: {keyPart}");
            if (hotkeyDictionary.TryGetValue(keyPart, out var canonical))
            {
                ExecuteHotkey(GetHotkeyResolver(canonical));

                return;
            }
            ChatHelper.SendMessage($"未知 Hotkey 参数: {keyPart}");
            return;
        }
        // 新增：处理 _gt 宏命令
        if (lowerArgs.EndsWith("_gt"))
        {
            var keyPart = lowerArgs[..^3];
            ExecuteCustomCommand(keyPart);
            return;
        }
    }
    // 新增：自定义命令执行方法
    private void ExecuteCustomCommand(string commandKey)
    {
        // 根据需要添加你的自定义命令逻辑
        switch (commandKey.ToLower())
        {
            case "自动选中":
                PvPSettings.Instance.自动选中 = !PvPSettings.Instance.自动选中;
                LogHelper.Print($"共通配置 \"{commandKey}\" 已设置为 {PvPSettings.Instance.自动选中}。");
                break;
            default:
                LogHelper.Print($"未知自定义命令: {commandKey}");
                break;
        }
    }
    private void ToggleQtSetting(string qtKey)
    {
        bool current = PvPMCHRotationEntry.JobViewWindow.GetQt(qtKey);
        PvPMCHRotationEntry.JobViewWindow.SetQt(qtKey, !current);
        LogHelper.Print($"QT \"{qtKey}\" 已设置为 {(!current).ToString().ToLower()}。");
    }

    private void ExecuteHotkey(IHotkeyResolver? resolver)
    {
        if (resolver == null) { LogHelper.Print("快捷键解析器未正确初始化。"); return; }
        if (resolver.Check() >= 0) resolver.Run();
        else LogHelper.Print("无法执行该快捷键命令，可能条件不满足或技能不可用。");
    }
    private IHotkeyResolver? GetHotkeyResolver(string? skillName)
    {
        // 先判空，避免传入null导致switch报错
        if (string.IsNullOrEmpty(skillName))
        {
            LogHelper.PrintError("GetHotkeyResolver：传入的技能名为null/空");
            return null;
        }

        // 使用传统switch语句替代switch表达式，避免语法错误
        switch (skillName)
        {
            case "疾跑":
                return new HotKeyResolver_NormalSpell(29057U, SpellTargetType.Self, false);
            case "龟壳":
                return new HotKeyResolver_NormalSpell(29054U, SpellTargetType.Self, false);
            case "热水":
                return new HotKeyResolver_NormalSpell(29711U, SpellTargetType.Self, false);
            case "LB":
                return new HotkeyData.机工LB();
            case "霰弹枪":
                return new HotkeyData.霰弹枪();
            default:
                LogHelper.PrintError($"GetHotkeyResolver：无匹配技能，传入的技能名：{skillName}");
                return null;
        }
    }
    private string? GetMatchingQtKey(string keyPart)
    {
        var lower = keyPart.ToLower();
        return qtKeyDictionary.TryGetValue(lower, out var matchedKey) ? matchedKey : null;
    }
    #endregion
    public void OnEnterRotation()
    {
        try
        {
            LogHelper.Print("欢迎使用Linto的机工PVPACR。");
            ECHelper.Commands.RemoveHandler("/Linto_MCH");//先移除旧的再添加新的，以免本地重载的时候命令没有注销
            ECHelper.Commands.AddHandler("/Linto_MCH", new CommandInfo(PvPMCHCommandHandler));
            LogHelper.Print("宏命令已注册，示例：/Linto_MCH 冲刺_qt,/Linto_MCH LB_hk,/Linto_MCH 自动选中_gt");
            BulidQtKeyDictionary(); // 生成 qtKeyDictionary
            PVPHelper.进入ACR();
            Share.Pull = true;
        }
        catch (MissingFieldException ex)
        {
            LogHelper.PrintError($"初始化失败: {ex.Message}");
        }

    }
    public void OnExitRotation()
    {
        Share.Pull = false;
    }
}
