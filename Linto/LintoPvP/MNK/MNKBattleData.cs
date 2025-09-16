namespace Linto.LintoPvP.MNK;

public class PvPMNKBattleData
{
	public static PvPMNKBattleData Instance = new PvPMNKBattleData();

	public void Reset()
	{
		Instance = new PvPMNKBattleData();
	}
}
