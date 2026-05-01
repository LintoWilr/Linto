namespace Linto.LintoPvP.SMN;

public class PvPSMNBattleData
{
    public static PvPSMNBattleData Instance = new();

    public static void Reset() => Instance = new PvPSMNBattleData();
}
