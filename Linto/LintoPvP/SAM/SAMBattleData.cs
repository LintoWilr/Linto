namespace Linto.LintoPvP.SAM;

public class PvPSAMBattleData
{
    public static PvPSAMBattleData Instance = new();

    public static void Reset() => Instance = new PvPSAMBattleData();
}
