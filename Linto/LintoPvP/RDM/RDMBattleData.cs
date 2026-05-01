namespace Linto.LintoPvP.RDM;

public class PvPRDMBattleData
{
    public static PvPRDMBattleData Instance = new();

    public void Reset() => Instance = new PvPRDMBattleData();
}
