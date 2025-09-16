using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.TriggerModel;
using CombatRoutine.View;
using Common.Define;
using Common.Language;
using Linto.LintoPvP.PVPApi;
using Linto.Wilr.DarkKnight;
using Linto.Wilr.DarkKnight.GCD;

namespace Linto.Wilr;

public class DarkKnightRotationEntry : IRotationEntry
{
	private DarkKnightOverlay _overlay = new DarkKnightOverlay();

	public List<ISlotResolver> SlotResolvers = new List<ISlotResolver>
	{
		(ISlotResolver)(object)new DRKGCD_Unmend(),
	};
	

	public string OverlayTitle { get; } = "抽水马桶";


	public string AuthorName { get; } = "Linto PvP";


	public Jobs TargetJob { get; } = (Jobs)32;


	public AcrType AcrType { get; } = (AcrType)1;


	public int MinLevel { get; } = 1;
	public int MaxLevel { get; } = 90;
	public string Description { get; } = "需要获取权限才能使用\n适用场景:PvP";

	public void DrawOverlay()
	{
		if (PVPHelper.通用权限())
		{
			_overlay.Draw();
		}
		else
		{
			_overlay.Draw2();
		}
	}

	public Rotation Build(string settingFolder)
	{
		PvPSettings.Build(settingFolder);
		PvPDRKSettings.Build(settingFolder);
		return 
			new Rotation(this, () => SlotResolvers).AddOpener
				((Func<uint, IOpener>)GetOpener).SetRotationEventHandler(
				new DRKRotationEventHandler()).AddSettingUIs(new TankDRKSettingView())
			.AddSlotSequences()
			.AddTriggerAction();
	}

	private IOpener? GetOpener(uint level)
	{
		return null;
	}

	public void OnLanguageChanged(LanguageType languageType)
	{
	}
}
