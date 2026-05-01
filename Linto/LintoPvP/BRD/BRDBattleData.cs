namespace Linto.LintoPvP.BRD;

public class PvPBRDBattleData
{
    public static PvPBRDBattleData Instance = new();

    public static void Reset() => Instance = new PvPBRDBattleData();
}
