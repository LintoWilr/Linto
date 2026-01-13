namespace Linto.LintoPvP.SAM;

public class PvPSAMBattleData
{
	public static PvPSAMBattleData Instance = new PvPSAMBattleData();

	public void Reset()
	{
		Instance = new PvPSAMBattleData();
	}
}
