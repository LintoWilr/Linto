namespace Linto.LintoPvP.GNB;

public class PvPGNBBattleData
{
    public static PvPGNBBattleData Instance = new();

    public static void Reset() => Instance = new PvPGNBBattleData();
}
