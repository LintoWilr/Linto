namespace Linto.LintoPvP.SCH;

public class PvPBLMBattleData
{
	public static PvPBLMBattleData Instance = new PvPBLMBattleData();

	public void Reset()
	{
		Instance = new PvPBLMBattleData();
	}
}
