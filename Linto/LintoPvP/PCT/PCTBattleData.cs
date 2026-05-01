namespace Linto.LintoPvP.PCT;

public class PvPPCTBattleData
{
    public static PvPPCTBattleData Instance = new();

    public static void Reset() => Instance = new PvPPCTBattleData();
}
