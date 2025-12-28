namespace Linto.LintoPvP.SMN;

public class PvPSMNBattleData
{
    public static PvPSMNBattleData Instance = new PvPSMNBattleData();

    public void Reset()
    {
        Instance = new PvPSMNBattleData();
    }
}
