namespace Linto.LintoPvP.BRD;

public class PvPBRDBattleData
{
    public static PvPBRDBattleData Instance = new();

    public void Reset() => Instance = new PvPBRDBattleData();
}
