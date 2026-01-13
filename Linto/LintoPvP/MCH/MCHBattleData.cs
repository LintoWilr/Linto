namespace Linto.LintoPvP.MCH;

public class PvPMCHBattleData
{
	public static PvPMCHBattleData Instance = new PvPMCHBattleData();

	public void Reset()
	{
		Instance = new PvPMCHBattleData();
	}
}
