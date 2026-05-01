namespace Linto.LintoPvP.RDM;

public class PvPRDMBattleData
{
    public static PvPRDMBattleData Instance = new();

    public static void Reset() => Instance = new PvPRDMBattleData();
}
