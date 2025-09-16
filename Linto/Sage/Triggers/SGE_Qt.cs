using System.Numerics;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using ImGuiNET;

namespace Linto.Sage.Triggers;

public class SGE_Qt : ITriggerAction, ITriggerBase
{
    public List<TriggerQTSetting> QTList = new List<TriggerQTSetting>();

    public string DisplayName { get; } = "贤者/QT设置";


    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.BeginChild("###TriggerWhm", new Vector2(0f, 0f));
        ImGuiHelper.DrawSplitList<TriggerQTSetting>("QT开关", (IList<TriggerQTSetting>)QTList, (Func<TriggerQTSetting, string>)DrawHeader, (Func<TriggerQTSetting>)AddCallBack, (Func<TriggerQTSetting, TriggerQTSetting>)DrawCallback, 0.3f, true, (Func<TriggerQTSetting, Vector4?>)null);
        ImGui.EndChild();
        return true;
    }

    public bool Handle()
    {
        foreach (TriggerQTSetting qtSetting in QTList)
        {
            qtSetting.action();
        }
        return true;
    }

    private TriggerQTSetting DrawCallback(TriggerQTSetting arg)
    {
        arg.draw();
        return arg;
    }

    private string DrawHeader(TriggerQTSetting arg)
    {
        string v = (arg.Value ? "开" : "关");
        return v + "-" + arg.Key;
    }

    private TriggerQTSetting AddCallBack()
    {
        return new TriggerQTSetting();
    }
}