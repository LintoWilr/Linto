namespace Linto.LintoPvP.VPR;

public class PvPVPRBattleData
{
    public static PvPVPRBattleData Instance = new();

    public static void Reset() => Instance = new PvPVPRBattleData();
}
