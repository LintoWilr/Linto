using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;

namespace Linto.Sage.Triggers;

public class 贤者时间轴蓝豆状态 : ITriggerCond, ITriggerBase
{
	[LabelName("蓝豆数量")]
	public int Blue { get; set; }

	public string DisplayName => LocalizationExtensions.Loc("SGE/检测量谱-蓝豆");

	public string Remark { get; set; }

	public bool Draw()
	{
		return false;
	}
	public bool Handle(ITriggerCondParams triggerCondParams)
	{
		return Core.Resolve<JobApi_Sage>().Addersgall >= Blue;
	}
}
