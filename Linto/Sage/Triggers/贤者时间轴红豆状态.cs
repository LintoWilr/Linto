using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;

namespace Linto.Sage.Triggers;

public class 贤者时间轴红豆状态 : ITriggerCond, ITriggerBase
{
	[LabelName("红豆数量")]
	public int Red { get; set; }

	public string DisplayName => LocalizationExtensions.Loc("SGE/检测量谱-红豆");

	public string Remark { get; set; }

	public bool Draw()
	{
		return false;
	}
	public bool Handle(ITriggerCondParams triggerCondParams)
	{
		return Core.Resolve<JobApi_Sage>().Addersting >= Red;
	}
}
