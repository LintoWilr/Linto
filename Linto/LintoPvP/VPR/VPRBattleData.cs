namespace Linto.LintoPvP.VPR;

public class PvPVPRBattleData
{
    public static PvPVPRBattleData Instance = new();

    public void Reset() => Instance = new PvPVPRBattleData();
}
