using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Define;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;
using ImGuiNET;

namespace Linto.Sage.Triggers
{
    public class CheckAddersgall : ITriggerCond
    {
        public string Remark { get; set; }

        public CompareType CompareType = CompareType.LessEqual;

        public int Count;
        public string DisplayName => "贤/检测蛇胆".Loc();

        public void Check()
        {

        }

        public bool Draw()
        {
            ImGui.Text("蛇胆数量");
            ImGui.SameLine();
            ImGuiHelper.DrawEnum("", ref CompareType, size: 50);
            ImGui.SameLine();
            ImGuiHelper.LeftInputInt("", ref Count);
            return true;
        }

        public bool Handle(ITriggerCondParams triggerCondParams)
        {
            var currValue = Core.Resolve<JobApi_Sage>().Addersgall;
            switch (CompareType)
            {
                case CompareType.Equal:
                    return currValue == Count;
                case CompareType.NotEqual:
                    return currValue != Count;
                case CompareType.Greater:
                    return currValue > Count;
                case CompareType.GreaterEqual:
                    return currValue >= Count;
                case CompareType.Less:
                    return currValue < Count;
                case CompareType.LessEqual:
                    return currValue <= Count;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}