namespace Linto.LintoPvP.PCT;

public class PvPPCTBattleData
{
    public static PvPPCTBattleData Instance = new();

    public void Reset() => Instance = new PvPPCTBattleData();
}
