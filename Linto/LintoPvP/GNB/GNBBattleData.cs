namespace Linto.LintoPvP.GNB;

public class PvPGNBBattleData
{
	public static PvPGNBBattleData Instance = new PvPGNBBattleData();

	public void Reset()
	{
		Instance = new PvPGNBBattleData();
	}
}
