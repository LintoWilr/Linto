namespace Linto.LintoPvP.GNB;

public class PvPGNBBattleData
{
    public static PvPGNBBattleData Instance = new();

    public void Reset() => Instance = new PvPGNBBattleData();
}
