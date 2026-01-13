namespace Linto.LintoPvP.DRG;

public class PvPDRGBattleData
{
	public static PvPDRGBattleData Instance = new PvPDRGBattleData();

	public void Reset()
	{
		Instance = new PvPDRGBattleData();
	}
}
