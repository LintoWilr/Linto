namespace Linto.LintoPvP.VPR;

public class PvPVPRBattleData
{
    public static PvPVPRBattleData Instance = new PvPVPRBattleData();

    public void Reset()
    {
        Instance = new PvPVPRBattleData();
    }
}
